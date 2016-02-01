(function () {
    'use strict';

    /**
     * Provides AddMe dialog to add/remove a user to a contact collection.
     * 
     * @requires
     * addMeDialog.html and ui.bootstrap module
     */

    var controllerId1 = 'addMeController';
    var controllerId2 = 'addMeInstanceController';
    angular.module('app').controller(controllerId1,['$scope', '$uibModal', 'common', addMeController]);
    angular.module('app').controller(controllerId2,['$rootScope', '$uibModalInstance', 'profile', 'common', 'config', 'profileCollectionService',
            addMeInstanceController]);

    /**
     * Parent controller, it passes profile to the child controller.
     *
     * @example
     * To use this <a class="btn"
     *                ng-controller="addMeController" 
     *                ng-click="openAddMeModal(vm.profile)">
     */
    function addMeController($scope, $uibModal, common) {
        /**
         * Opens the AddMe modal dialog for the authenticated user in attempt
         * to add/remove the target user. 
         * The button that has this method attached to should NOT show up if 
         * the request is not authenticated.
         *
         * @param
         * profile: the user profile who is being added or removed by the
         * authenticated user.
         * 
         * @example
         * ng-click="openAddMeModal(contact.userName)"
         */
        $scope.openAddMeModal = function (profile) {
            var modalInstance = $uibModal.open({
                templateUrl: '/wwwroot/app/profile/addMe/addMeDialog.html',
                controller: 'addMeInstanceController as vm',
                resolve: { // passing to child controller below
                    profile: function () {
                        return profile;
                    },
                }
            });
        };
    }   

    /**
     * Child controller used by the addMeDialog.html, if a user is not authenticated
     * the user should not see this modal.
     *
     * @param
     * $rootScope: needed for $broadcast
     * $modalInstance: needed for close and dismiss of the dialog
     * profile: the target user profile
     * common: for logging
     * config: for events names
     */
    function addMeInstanceController($rootScope, $uibModalInstance, profile,
                                     common, config, profileCollectionService) {
        var getLogFn = common.logger.getLogFn;
        var log = getLogFn(controllerId2);
        var logError = getLogFn(controllerId2, 'error');
        var logSuccess = getLogFn(controllerId2, 'success');

        /* ---------------------------------------------------------- view model */

        var vm = this;
        /**
         * A list of groups shown up on the dialog, no contacts data.
         *
         * @see
         * GroupViewModel class for the properties in each group of the list.
         */
        vm.groupList = [];
        // the value from the textbox on the modal dialog
        vm.ff = { newGroup: "" };
        // the target
        vm.profile = profile;
        // methods
        vm.createGroup = createGroup; // when user clicks create btn to add a new group
        vm.addRemoveUser = addRemoveUser; // when user add/remove a user on a group
        vm.cancel = cancel; // dismisses the dialog

        /* ---------------------------------------------------------- constructor */

        /**
         * initilaize vm.groupList
         */
        init(); 
        function init() {
            profileCollectionService.getGroupListWithTarget(profile.id).then(function (data) {
                vm.groupList = data;
            });
        }

        /* ---------------------------------------------------------- public methods */

        /**
         * Creates a new group and adds the targetUserName to the new group.
         * Then it broadcasts event so listeners.
         *
         * @event
         * broadcast '' after creating group
         */
        function createGroup() {
            // make sure new group new doesn't already exist in the list
            var group = _findGroupByName(vm.ff.newGroup);
            if (group) {
                log('Group "' + vm.ff.newGroup + '" exists already!');
                return;
            }

            // create it and gets back the new group
            profileCollectionService.createGroupWithTarget(vm.ff.newGroup, profile.id)
                                            .then(function (group) {
                                                success(group);
                                            });

            // update ui and broadcast event
            function success(group) {
                vm.groupList.push(group); // update ui
                vm.ff.newGroup = ''; // clear textbox

                // prepare event data
                var eventArgs = {
                    added: group.targetExists, // true if added, false if removed
                    profile: profile, // the target user
                    group: group // the group the target user was added or removed
                };

                // broadcast with new group telling a new group was created
                $rootScope.$broadcast(config.events.contactToGroupUpdated, eventArgs);
            }
        }

        /**
         * Toggle a contact to a group when user click on the group on the modal
         * dialog, if the target existed on the group, remove it, else add it.
         *
         * @param
         * group: the group to add/remove the target user
         *
         * @event
         * broadcast
         */
        function addRemoveUser(group) {
            if (!group) {
                console.log('group cannot be null');
                return;
            }

            // should I pass targetExists to server side to tell it whether it was an add or remove???
            profileCollectionService.addRemoveUserToGroup(profile.id, group.id).then(success());

            // update ui
            function success() {
                group.targetExists = !group.targetExists;

                // prepare event data
                var eventArgs = {
                    added : group.targetExists, // true if added, false if removed
                    profile: profile, // the target user
                    group: group // the group the target user was added or removed
                };

                // the receiver should decide if to carry out any actions
                $rootScope.$broadcast(config.events.contactToGroupUpdated, eventArgs);
            }
        }

        function cancel() {
            $uibModalInstance.dismiss('cancel');
        };

        /* ---------------------------------------------------------- private methods */

        function _findGroupByName(groupName) {
            for (var i = 0; i < vm.groupList.length; i++) {
                if (vm.groupList[i].name.toUpperCase() === groupName.toUpperCase())
                    return vm.groupList[i];
            }
            return null;
        }
    }
})();