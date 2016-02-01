(function () {
    'use strict';

    /**
     * Service for addMeDialog.html, emailMeDialog.html, CollectionsUsers.cshtml
     */

    var serviceId = 'profileCollectionService';
    angular.module('app').factory(serviceId, ['$http', '$q', 'common', profileCollectionService]);

    function profileCollectionService($http, $q, common) {

        return {
            // addMe
            getGroupListWithTarget: getGroupListWithTarget,
            createGroupWithTarget: createGroupWithTarget,
            addRemoveUserToGroup: addRemoveUserToGroup,
            // collectionsUsers
            getGroup: getGroup,
            getGroupList: getGroupList,
            createGroup: createGroup,
            updateGroupName: updateGroupName,
            destroyGroup: destroyGroup,
            // emailMe
            emailMe: emailMe,
        };

        // -------------------------------------------------------------------- addMe

        /**
        * Returns group list the specified user has and mark TargetUserExists to true
        * if the target user exists in a group, for the AddMeDialog.
        */
        function getGroupListWithTarget(profileId) {
            var d = $q.defer();
            var url = '/api/profile/grouplist_with_target/' + profileId;
            $http({ method: "GET", url: url, isArray: true, }).success(d.resolve).error(d.reject);
            return d.promise;
        }

        /**
         * Creates a new group for the authenticated user and adds the user to 
         * that group.
         * @returns the new group created.
         */
        function createGroupWithTarget(newGroupName, profileId) {
            var d = $q.defer();
            var url = '/api/profile/create_group_with_target';
            $http({ url: url, method: "POST", 
                data: {
                    targetId: profileId,
                    newGroupName: newGroupName,
                } // CreateGroupWithTargetIM.cs
            }).success(d.resolve).error(d.reject);
            return d.promise;
        }

        /**
         * Adds or removes a user to a group.
         */
        function addRemoveUserToGroup(profileId, groupId) {
            var d = $q.defer();
            var url = '/api/profile/add_remove_user_to_group';
            $http({ url: url, method: "POST", 
                data: {
                    targetId: profileId,
                    groupId: groupId,
                } // AddRemoveUserToGroupIM.cs
            }).success(d.resolve).error(d.reject);
            return d.promise;
        }

        // -------------------------------------------------------------------- collectionsUsers

        /**
        * Returns the specified group and its users.
        * @param groupSlug: "" will return all contacts.
        */
        function getGroup(userName, groupSlug) {
            var d = $q.defer();
            if (!groupSlug) groupSlug = "";
            var url = "/api/profile/" + userName + "/group/" + groupSlug;
            $http({ method: "GET", url: url}).success(d.resolve).error(d.reject);
            return d.promise;
        }

        /**
         * Returns a list of groups, just group data not users inside the groups.
         * It's used to display on the AddMe dialog modal when an authenticated
         * user tries to add another user.  
         */
        function getGroupList(userName) {
            var d = $q.defer();
            var url = "/api/profile/" + userName + "/grouplist";
            $http.get(url).success(d.resolve).error(d.reject);
            return d.promise;
        }

        /**
        * Creates a new group for the authenticated user.
        * @returns The new group created.
        */
        function createGroup(newGroupName) {
            var d = $q.defer();
            var url = '/api/profile/create_group';
            $http({
                url: url, method: "POST", 
                data: { newGroupName: newGroupName } // CreateGroupIM.cs
            }).success(d.resolve).error(d.reject);
            return d.promise;
        }

        /**
         */
        function updateGroupName(groupId, groupName) {
            var d = $q.defer();
            var url = '/api/profile/update_group';
            $http({
                url: url, method: "POST", 
                data: { groupId: groupId, newGroupName: groupName } // UpdateGroupIM.cs
            }).success(d.resolve).error(d.reject);
            return d.promise;
        }

        /**
         * Deletes a group and all its collected users.
         */
        function destroyGroup(groupId) {
            var d = $q.defer();
            var url = '/api/profile/destroy_group';
            $http({
                url: url, method: "POST", 
                data: { groupId: groupId } // DestroyGroupIM.cs
            }).success(d.resolve).error(d.reject);
            return d.promise;
        }


        // -------------------------------------------------------------------- emailMe

        /**
         * Email the user by the authenticated user.
         */
        function emailMe(userName, message) {
            var d = $q.defer();
            var url = '/api/profile/emailme';
            $http({
                url: url, method: "POST", 
                data: {
                    userName: userName,
                    message: message
                } // EmailMeBindingModel.cs
            }).success(d.resolve).error(d.reject);
            return d.promise;
        }

    }
})();