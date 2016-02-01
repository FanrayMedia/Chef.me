(function () {
    'use strict';

    /**
     * Controller for CollectionsUsers.cshtml "/{userName}/collections/users".
     * Note: the "All contacts" tab is added here in js not from server.
     */

    var controllerId = 'collectionsUsersController';
    angular.module('app').controller(controllerId,
        ['$scope', 'common', 'config', 'confirmDialog',
            'profileService', 'profileCollectionService', 'activeProfile', collectionsUsersController]);

    function collectionsUsersController($scope, common, config, confirmDialog,
        profileService, profileCollectionService, activeProfile) {
        var getLogFn = common.logger.getLogFn;
        var log = getLogFn(controllerId);
        var logError = getLogFn(controllerId, 'error');
        var logSuccess = getLogFn(controllerId, 'success');
        var _activeProfile = activeProfile;
        /**
         * The user whose profile page is being visited.
         */
        var _profileUserName = window.location.pathname.split('/')[1];
        /**
         * The currently active group whose contacts are shown on the page.
         */
        var _activeGroupSlug = window.location.pathname.split('/')[3];
        //var _targetProfile = null;

        /* ---------------------------------------------------------- view model */

        var vm = this;
        vm.groupList = []; // the group list on sidebar
        vm.activeGroup = null; // contacts array inside
        vm.showContactEditButtons = false;
        vm.profile = null; // so we can get data like totalUserCount
        vm.createGroup = createGroup;
        vm.destroyGroup = destroyGroup;
        vm.updateGroupName = updateGroupName;
        vm.newGroupName = "";

        /* ---------------------------------------------------------- constructor */

        /**
         * @description
         * Constructor initializes group list and contacts in current group.
         */
        init();
        function init() {
            if (_activeProfile &&
                _activeProfile.userName.toUpperCase() === _profileUserName.toUpperCase()) {
                vm.showContactEditButtons = true;
            }

            // we can consolidate 4 calls into one with a composite endpoint
            var promises = [
                _getGroupList(),
                _getActiveGroup(),
            ];
            common.activateController(promises, controllerId)
                .then(function () {
                    log('Activated ' + controllerId);
                });
        }

      
        /* ---------------------------------------------------------- private methods */

        //#region private methods

        /**
         * Sets the current group with contacts.  
         * If it is 'all' group, the following will be returned, the groupId is 0.
         * Object {contacts: Array[...], groupId: 0, groupName: null, slug: null, isPublic: true…}
         */
        function _getActiveGroup() {
            profileCollectionService.getGroup(_profileUserName, _activeGroupSlug).then(function (data) {
                vm.activeGroup = data;
                vm.profile = data.owner;
            }).catch(function (err) {
                // err.status: 404, err.data.message
                //log(err);
            });
        }

        /**
         * Sets group list for the sidebar.
         */
        function _getGroupList() {
            profileCollectionService.getGroupList(_profileUserName).then(function (data) {
                vm.groupList = data;
                vm.groupList.unshift({
                    absoluteLink: "/" + _profileUserName + "/collections",
                    name: "All contacts",
                    id:0
                });
            });
        }
       
        //#endregion

        /* ---------------------------------------------------------- public methods */

        function createGroup() {
            if (!vm.newGroupName) {
                logError("Collection name cannot be empty!");
                return;
            }
            else if (_groupNameExists(vm.newGroupName)) {
                logError("Collection name exists, try a different one!");
                return;
            }

            profileCollectionService.createGroup(vm.newGroupName).then(function (data) {
                vm.groupList.push(data);
                vm.newGroupName = "";
            });
        }

        function destroyGroup(group) {
            return confirmDialog.deleteDialog('Collection "' + group.name + '"')
                .then(confirmDelete);

            function confirmDelete() {
                profileCollectionService.destroyGroup(group.id).then(success, failed);

                function success() {
                    window.location = "/" + _profileUserName + "/collections";
                }

                function failed(err) {
                    logError(err);
                }
            }
        }

        function updateGroupName(groupName) {
            if (!groupName) {
                logError("Collection name CANNOT be empty!");
                return;
            }
            else if (groupName === vm.activeGroup.name) {
                return;
            }

            profileCollectionService.updateGroupName(vm.activeGroup.id, groupName).then(success, failed);
            function success(updatedGroup) {
                // update groupList
                var g = _findGroupById(vm.activeGroup.id); // find the g from list
                if (g) {
                    g.name = groupName;
                    g.absoluteLink = updatedGroup.absoluteLink;
                    // update url too without reloading the page
                    //http://stackoverflow.com/questions/12832317/window-history-replacestate-example
                    window.history.pushState({}, "Title", updatedGroup.absoluteLink);
                }
            }
            function failed(err) { logError(err); }
        }
        
        /* ---------------------------------------------------------- event handlers */

        /**
         * When active user is making changes to a contact through the AddMeDialog
         * an event will broadcast and arrive here.  This handler will then
         * do analysis and may add a contact or remove a contact on the ui.
         *
         * @param
         * event: for the event obj internals see the $on doc.
         * args: @see addMeDialog.js addRemoveUser method.
         *
         * @see 
         * $on doc on http://docs.angularjs.org/api/ng/type/$rootScope.Scope

           - have to figure out which contact and if its group count <= 1 remove
           - find the group in list and dec its user count
           - dec total user count
         */
        $scope.$on(config.events.contactToGroupUpdated, function (event, args) {
            // if the active user is not on his own profile page
            // there is nothing to update on the page
            if (_profileUserName.toUpperCase() !== _activeProfile.userName.toUpperCase())
                return;

            //log("handling user..." + args.profile.userName + "...group..." + args.group.name);
            var _targetProfile = args.profile;

            var group = _findGroupById(args.group.id);
            var userIndex = _findContactIndexByName(_targetProfile.userName);

            if (group === null) { // just created
                //debugger;
                group = args.group;
                vm.groupList.push(group);
                //vm.profile.groupCount++;
            }
            else if (args.added) // added to existing
            {
                // if contact (profile) had a group count 0, and it's being added
                // we update the ui, this happens only when active user removed
                // the contact and then without closing the modal and adds the contact back
                if (userIndex === -1){ // didn't find it
                    vm.activeGroup.users.push(_targetProfile);
                    vm.activeGroup.userCount++;
                }

                //find the group and inc its contact count, and total count
                group.userCount++;

                /** todo figure out how to update people count */
                //vm.profile.contactCount++;
            }
            else // removed
            {
                /*
                if active was removing a user,
                - we need to find the contact and see if its group count was 1, if it is
                  we remove the contact from the ui
                - we find the group and dec its count and total count
                */
                if (userIndex !== -1 /*&& _targetProfile.groupCount <= 0*/) { // groupCount already dec by 1  args.profile.groupCount <= 0
                    vm.activeGroup.users.splice(userIndex, 1);
                    vm.activeGroup.userCount--;
                }

                group.userCount--;

                /** todo figure out how to update people count */
                //if (args.profile.groupCount)
                //vm.profile.contactCount--;
            }

        });


        /**
         * Returns the index of the userName in the vm.activeGroup.users array.
         * This is used by the event handler to update the UI.
         * 
         * @param
         */
        function _findContactIndexByName(targetUserName) {
            for (var i = 0; i < vm.activeGroup.users.length; i++) {
                if (vm.activeGroup.users[i].userName.toUpperCase() === targetUserName.toUpperCase())
                    return i;
            }
            return -1;
        }

        function _findGroupById(groupId) {
            for (var i = 0; i < vm.groupList.length; i++) {
                if (vm.groupList[i].id === groupId)
                    return vm.groupList[i];
            }
            return null;
        }

        function _groupNameExists(groupName) {
            for (var i = 0; i < vm.groupList.length; i++) {
                if (vm.groupList[i].name.toUpperCase() === groupName.toUpperCase())
                    return true;
            }
            return false;
        }
    }
})();