(function () {
    'use strict';

    var controllerId = 'profileController';
    angular.module('app').controller(controllerId,
        ['$scope', '$timeout', '$http', 'common', 'commonService', 'activeProfile', 'profileService', profileController]);

    /**
     * covers both ProfileBox and EditBox on profile page.
     */
    function profileController($scope, $timeout, $http, common, commonService, activeProfile, profileService) {
        var getLogFn = common.logger.getLogFn;
        var log = getLogFn(controllerId);
        var _profileUserName = window.location.pathname.split('/')[1];


        $scope.profileOrig = null;
        $scope.profile = null;
        $scope.rgb = null;
        $scope.opa = null;
        $scope.imgList = ['A01', 'A02', 'A03', 'A04', 'A05', 'A06', 'A07', 'A08', 'A09'];

        // show/hide
        // if req is not authenticated then clicking on AddMe/EmailMe will redirect to login
        $scope.isRequestAuthenticated = !!activeProfile;
        $scope.showEditPage = $scope.isRequestAuthenticated && (activeProfile.userName === _profileUserName);

        // -------------------------------------------------------------------- Init

        init();
        function init() {
            // becuase draggable and slider the profile page uses throw exceptions
            // with the profile object delayed, so I initialize here first.
            $scope.profile = profileService.getData();

            profileService.getProfile(_profileUserName).then(function (data) {
                $scope.profile = data;//profileService.getData();
                $scope.profileOrig = angular.copy($scope.profile);

                // init here since we are checking newValue again oldValue
                // the initialization in the watcher won't run
                $scope.rgb = common.hexToRgb($scope.profile.styles.profbox_bgcolor);
                $scope.opa = $scope.profile.styles.profbox_opacity / 100;

                // init user bg img selection
                // the id is on <img id="{{img}} />
                $(function () { $('#' + $scope.profile.bgImgName).addClass('bgimg-selected'); });
            });
        }

        // -------------------------------------------------------------------- UI

        // watchers on opacity and bgcolor since they have calculation on them
        $scope.$watch('profile.styles.profbox_opacity', function (newValue, oldValue) {
            if (newValue === oldValue) return;
            $scope.opa = newValue / 100;
        });
        $scope.$watch('profile.styles.profbox_bgcolor', function (newValue, oldValue) {
            if (newValue === oldValue) return;
            $scope.rgb = common.hexToRgb(newValue);
        });        

        /**
         * The one-liner makes profile edit modal draggable. It's based on SO question 
         * http://stackoverflow.com/questions/22201539/angularui-modal-to-be-draggable-and-resizable
         *
         * @requires jqueryui
         */
        $timeout(function () {
            $("#profileModalBox").draggable();
            //var resizeOpts = {
            //    handles: "all", autoHide: true
            //};
            //$(".modal-dialog").resizable(resizeOpts);
        }, 0);

        /**
         * Helper makes sure required form fields are valid. Based on 
         * http://plnkr.co/edit/DHwOl881L6AhFue9EjGA?p=preview 
         * @param field is the name attribute on the form field.
         */
        $scope.isInvalid = function (field) {
            return $scope.profileEditForm[field].$invalid && $scope.profileEditForm[field].$dirty;
        };

        $scope.getLocation = function (val) {
            return commonService.getLocation(val);
        };

        /**
         * My impl of editable, toggle hidden.
         */
        $scope.editClick = function (item) {
            $('a#' + item + 'Link').toggleClass("hidden");
            $('div#' + item + 'Div').toggleClass("hidden");
        }

        $scope.cancelClick = function (item) {
            $scope.profile[item] = $scope.profileOrig[item];
            $('a#' + item + 'Link').toggleClass("hidden");
            $('div#' + item + 'Div').toggleClass("hidden");
        }

        // -------------------------------------------------------------------- update

        /**
         * When user drags the profilebox, it saves the new coordinates to server.
         * The "coordinates" event comes from "draggable" directive.
         */
        $scope.$on("updateCoordinates", function (event, args) {
            profileService.updateCoordinates(args.pageX, args.pageY);
        });

        /**
         * change bg img and save directly to server.
         * @param fn "A01", no ext, but when passed to service we attach ext so
         * server can just save it without worry about ext.
         */
        $scope.updateBgImg = function (fn) {
            // set the selected style on the UI
            $('.bgimg-list .bgimg-selected').removeClass('bgimg-selected');
            $('#' + fn).addClass('bgimg-selected');

            // updated the bgImg to new value
            $scope.profile.bgImg = fn + $scope.profile.bgImgExt;
            profileService.updateBgImg($scope.profile.bgImg);
        }

        /**
         * Saves profile data back to datasource.  
         */
        $scope.updateProfile = function (item) {
            var f = $scope.profileEditForm;
            var data = {};
            if (item === 'name') {
                data['firstName'] = $scope.profile['firstName'];
                data['lastName'] = $scope.profile['lastName'];
            }
            else {
                // have to use form value, because Deisgn tab values are with styels obj
                data[item] = f[item].$modelValue;// $scope.profile[item];
            }
            profileService.updateProfile(data);
            $scope.editClick(item);
        }

        $scope.updateDesign = function (item) {
            var f = $scope.profileEditForm;
            var data = {};
            data[item] = f[item].$modelValue;// $scope.profile[item];
            profileService.updateDesign(data);
        }

        /**
         * ------------------------------------------ slider optioins
         * this is based this particular plugin, if we can find better, the code
         * below will be removed.
         * http://darul75.github.io/ng-slider/
         */
        $scope.name_size_slider_options = {
            from: 50,
            to: 100,
            step: 1
        };
        $scope.headline_size_slider_options = {
            from: 14,
            to: 50,
            step: 1
        };
        $scope.bio_size_slider_options = {
            from: 10,
            to: 24,
            step: 1
        };
        $scope.links_size_slider_options = {
            from: 10,
            to: 24,
            step: 1
        };
        $scope.profbox_opacity_slider_options = {
            from: 0,
            to: 100,
            step: 1,
            dimension: "%"
        };
    }
})();