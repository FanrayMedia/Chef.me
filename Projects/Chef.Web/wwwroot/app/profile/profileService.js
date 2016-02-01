(function () {
    'use strict';

    /**
     * Retrieves and updates profile, for collection related operations see
     * profileCollectionService.js.
     */

    var serviceId = 'profileService';
    angular.module('app').factory(serviceId, ['$http', '$q', 'common', profileService]);

    function profileService($http, $q, common) {
        return { 
            getProfile: getProfile,
            getData: getData,
            updateCoordinates: updateCoordinates,
            updateBgImg: updateBgImg,
            updateProfile: updateProfile,
            updateDesign: updateDesign
        };

        /**
         * Retrieves the profile with styles.
         */
        function getProfile(userName) {
            var d = $q.defer();
            var url = "/api/profile/" + userName;
            $http.get(url).success(d.resolve).error(d.reject);
            return d.promise;
        }

        /**
         * Updates the coordinates of the profile box.
         */
        function updateCoordinates(x, y) {
            console.log(x); console.log(y);
            var d = $q.defer();
            var url = '/api/profile/updateCoordinates';
            $http({
                method: "POST", url: url, 
                data: {
                    x: x,
                    y: y
                }
            }).success(d.resolve).error(d.reject);
            return d.promise;
        }

        /**
         * Updates the background image.
         * @param f: "A01.jpg" with extension.
         */
        function updateBgImg(f) {
            console.log(f);
            var d = $q.defer();
            var url = '/api/profile/updateBgImg';
            $http({
                method: "POST", url: url, 
                data: {
                    imgFileName: f
                }
            }).success(d.resolve).error(d.reject);
            return d.promise;
        }
        /** 
         * Updates anything on Design tab.
         */
        function updateDesign(p) {
            console.log(p);
            var d = $q.defer();
            var url = '/api/profile/updateDesign';
            $http({
                method: "POST", url: url, 
                data: {
                    designField: JSON.stringify(p)
                }
            }).success(d.resolve).error(d.reject);
            return d.promise;
        }
        /**
         * Updates anything on Profile tab 
         */
        function updateProfile(p) {
            console.log(p);
            var d = $q.defer();
            var url = '/api/profile/update';
            $http({
                method: "POST", url: url, 
                data: {
                    profileField: JSON.stringify(p)
                }
            }).success(d.resolve).error(d.reject);
            return d.promise;
        }

        /**
         * static profile data helps debug and get rid exceptions with draggable and slider.
         */
        function getData() {
            return {
                userName: 'ray',
                firstName: 'Ray',
                lastName: 'Fan',
                headline: 'Add a headline',
                bio: 'Add a bio',
                locations: 'USA',
                /* I broke img and ext because profileController needs to select the chosen
                   img by id which is set only by the img file name without extension */
                bgImg: "", // can put a pixel img holder here
                bgImgName: 'A02',
                bgImgExt: '.jpg',
                styles: {
                    // --------------- profbox (binding happens inside draggable directive)
                    profbox_opacity: 40,//'0.2',
                    profbox_bgcolor: '#000', // must be hex, no black use #000
                    profbox_x: 0,
                    profbox_y: 0,
                    // --------------- editbox
                    // design tab
                    name_color: '#eee',
                    name_font: 'Arial', // case sensitive
                    name_size: 66, //px
                    headline_color: '#eee',
                    headline_font: 'Arial',
                    headline_size: 24,
                    bio_color: '#eee',
                    bio_font: 'Arial',
                    bio_size: 18,
                    links_color: '#eee',
                    links_font: 'Arial',
                    links_size: 15,
                },
            };
        }

    }
})();