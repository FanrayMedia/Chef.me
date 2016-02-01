(function () {
    'use strict';

    var controllerId = 'usersController';
    angular.module('app').controller(controllerId,
        ['common', 'usersService', usersController]);

    /**
     * 
     */
    function usersController(common, usersService) {
        var getLogFn = common.logger.getLogFn;
        var log = getLogFn(controllerId);

        // https://github.com/johnpapa/angular-styleguide#controlleras-with-vm
        var vm = this;
        /**
         * @see 
         */
        vm.users = [];

        init();
        function init() {
            usersService.getPeople().then(function (data) {
                vm.users = data;
            });
        }
    }
})();