(function () {
    'use strict';

    angular.module('app').controller("registerController",
        ['$scope', '$http', 'commonService', registerController]);

    function registerController($scope, $http, commonService) {
        $scope.getLocation = function (val) {
            return commonService.getLocation(val);
        };
    }
})();
