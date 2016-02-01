(function() {
    'use strict';
    var app = angular.module('app');

    /**
     * Makes the Profile Box draggable, usage "profile-draggable".
     * 
     * Doing #draggable  in #angularjs  is not hard, you can still reply on #jqueryui  
     * 1. add reference to #jquery  and #jqueryui  
     * 2. create an #angularjs   #directive  and set draggable on the element, $(element).draggable()
     * 3. put "draggable" on your html tag, the communication between your controller and the directive is all taken care of by #scope  
     */
    app.directive('profileDraggable', ['$rootScope', 'common', 'profileService', function ($rootScope, common, profileService) {
        /**
         * scope is the $scope from profileController
         * ele is div profilebox
         */
        return function (scope, ele, attr) {
            var _profileUserName = window.location.pathname.split('/')[1];
            profileService.getProfile(_profileUserName).then(function (data) {
                var startX = $(ele).position().left; //387
                var startY = $(ele).position().top; //72

                // jqueryui draggable set on the ele which is profilebox
                $(ele).draggable({
                    //containment: "parent",//"#profile-constrain", // see _Layout.cshtml next too container
                    scroll: false,
                    stop: function () {
                        var newPos = $(this).position();
                        var x = newPos.left - startX;//387
                        var y = newPos.top - startY; //72
                        x = (x >= 0) ? x : 0;
                        y = (y >= 0) ? y : 0;
                        x = (x > 474) ? 474 : x;

                        // prepare data to broadcast
                        var eventArgs = {
                            pageX: x,
                            pageY: y,
                        };
                        $rootScope.$broadcast('updateCoordinates', eventArgs);
                    }
                });

                /**
                 * profilebox styles, all the dynamic ones, the static ones are 
                 * on the index.cshtml page. Have to use rgba instead of hex for
                 * the box background, or opacity penetrates even text on the box.
             
                 scope.profile.styles.profbox_opacity
                 todo: move this css back to index.cshtml so binding could work
                 */
                ele.css({
                    cursor: 'move',
                    left: data.styles.profbox_x,
                    top: data.styles.profbox_y,
                });

            }); // then

        }
    }]);
})();