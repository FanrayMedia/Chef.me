(function () {
    'use strict';

    /**
     * Provides AddMe dialog to add/remove a user to a contact collection.
     * 
     * @requires
     * emailMeDialog.html and ui.bootstrap module
     */

    var controllerId1 = 'emailMeController';
    var controllerId2 = 'emailMeModalController';
    angular.module('app').controller(controllerId1, ['$scope', '$uibModal', 'common', emailMeController]);
    angular.module('app').controller(controllerId2, ['$rootScope', '$uibModalInstance', 'profile',
        'common', 'config', 'profileCollectionService',emailMeModalController]);

    /**
     * Parent controller, it passes profile to the child controller.
     *
     * @example
     * To use this <a class="btn"
     *                ng-controller="emailMeController" 
     *                ng-click="openEmailMeModal(vm.profile)">
     */
    function emailMeController($scope, $uibModal, common) {
        var getLogFn = common.logger.getLogFn;
        var log = getLogFn(controllerId1);

        /**
         * Opens the AddMe modal dialog for the authenticated user in attempt
         * to add/remove the target user. 
         * The button that has this method attached to should NOT show up if 
         * the request is not authenticated.
         *
         * @param
         * profile: the user profile who is being added or removed by the
         * authenticated user.
         */
        $scope.openEmailMeModal = function (profile) {
            var modalInstance = $uibModal.open({
                templateUrl: '/wwwroot/app/profile/emailMe/emailMeDialog.html',
                controller: 'emailMeModalController as vm',
                resolve: { // passing to child controller below
                    profile: function () {
                        return profile;
                    },
                }
            });
        };
    }

    /**
     * Child controller used by the AddMeDialog.html, if a user is not authenticated
     * the user should not see this modal.
     *
     * @param
     * $rootScope: needed for $broadcast
     * $modalInstance: needed for close and dismiss of the dialog
     * profile: the target user profile
     * common: for logging
     * config: for events names
     */
    function emailMeModalController($rootScope, $uibModalInstance, profile,
                                     common, config, profileCollectionService) {
        var getLogFn = common.logger.getLogFn;
        var log = getLogFn(controllerId2);
        var logError = getLogFn(controllerId2, 'error');
        var logSuccess = getLogFn(controllerId2, 'success');

        /* ---------------------------------------------------------- view model */

        var vm = this;
        vm.message = "";
        vm.profile = profile;
        vm.disableButton = false;
        vm.send = send; 
        vm.cancel = cancel;
        vm.btnText = "Send";

        /* ---------------------------------------------------------- public methods */

        // todo: disable send button once clicked
        function send() {
            vm.disableButton = true;
            vm.btnText = "Sending...";
            profileCollectionService.emailMe(profile.userName, vm.message).then(success, failed);
            function success() {
                $uibModalInstance.dismiss('cancel');
            }
            function failed(err) {
                logError(err);
                vm.disableButton = false;
                vm.btnText = "Send";
            }
        }

        function cancel() {
            $uibModalInstance.dismiss('cancel');
        };
    }
})();