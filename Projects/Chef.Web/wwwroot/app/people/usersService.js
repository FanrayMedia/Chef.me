(function () {
    'use strict';

    var serviceId = 'usersService';
    angular.module('app').factory(serviceId, ['$http', '$q', usersService]);

    function usersService($http, $q) {
        /**
         * Returns a list of @see UserVM by calling PeopleController.All().
         */
        function getPeople() {
            var d = $q.defer();
            var url = '/people/all';
            $http.get(url).success(d.resolve).error(d.reject);
            return d.promise;
        }

        return {
            getPeople: getPeople
        };
    }
})();