(function () {
    'use strict';

    /**
     * calling 3rd part APIs.
     */

    var serviceId = 'commonService';
    angular.module('app').factory(serviceId, ['$http', 'common', commonService]);

    function commonService($http, common) {
        return {
            getLocation: getLocation
        };

        /**
         * Any function returning a promise object can be used to load values asynchronously
         * https://developers.google.com/maps/documentation/geocoding/
         * http://maps.googleapis.com/maps/api/geocode/json
         */
        function getLocation(val) {
            return $http.get('//maps.googleapis.com/maps/api/geocode/json', {
                params: {
                    address: val,
                    sensor: false
                }
            }).then(function (res) {
                var addresses = [];
                angular.forEach(res.data.results, function (item) {
                    addresses.push(item.formatted_address);
                });
                return addresses;
            });
        };
    }
})();