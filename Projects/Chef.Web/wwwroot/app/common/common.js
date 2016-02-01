(function () {
    'use strict';

    /**
     * A common module that collects commonly used things.
     */
    angular.module('common', []).factory('common',
        ['$q', '$rootScope', '$timeout', 'config', 'logger', common]);

    function common($q, $rootScope, $timeout, config, logger) {
        var service = {
            $broadcast: $broadcast,
            $q: $q,
            $timeout: $timeout,
            activateController: activateController,
            logger: logger,
            hexToRgb: hexToRgb,
        };

        return service;

        function activateController(promises, controllerId) {
            return $q.all(promises).then(function (eventArgs) {
                var data = { controllerId: controllerId };
                $broadcast(config.controllerActivateSuccessEvent, data);
            });
        }

        function $broadcast() {
            return $rootScope.$broadcast.apply($rootScope, arguments);
        }

        /**
         * http://stackoverflow.com/questions/5623838/rgb-to-hex-and-hex-to-rgb
         * alert( hexToRgb("#0033ff").g ); // "51";
         * alert( hexToRgb("#03f").g ); // "51";
         */
        function hexToRgb(hex) {
            // Expand shorthand form (e.g. "03F") to full form (e.g. "0033FF")
            var shorthandRegex = /^#?([a-f\d])([a-f\d])([a-f\d])$/i;
            hex = hex.replace(shorthandRegex, function (m, r, g, b) {
                return r + r + g + g + b + b;
            });

            var result = /^#?([a-f\d]{2})([a-f\d]{2})([a-f\d]{2})$/i.exec(hex);
            return result ? {
                r: parseInt(result[1], 16),
                g: parseInt(result[2], 16),
                b: parseInt(result[3], 16)
            } : null;
        }
    }
})();