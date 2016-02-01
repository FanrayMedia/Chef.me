(function () {
    'use strict';

    var app = angular.module('app');

    // when log toaster shows up
    toastr.options.timeOut = 4000;
    toastr.options.positionClass = 'toast-top-right';

    // event names used
    var events = {
        controllerActivateSuccess: 'controller.activateSuccess',
        contactToGroupUpdated: 'contactToGroupUpdated',
    };

    // the config obj
    var config = {
        appErrorPrefix: '[Chef Error] ', 
        events: events,
        version: '1.0.0'
    };

    // inject config
    app.value('config', config);
    
    app.config(['$logProvider', function ($logProvider) {
        // turn debugging off/on (no info or warn)
        if ($logProvider.debugEnabled) {
            $logProvider.debugEnabled(true);
        }
    }]);
    
    /** when you bootstrap activeProfile from server, you init it here */
    app.config(['$provide', function ($provide) {
        var profile = angular.copy(window.activeProfile);
        $provide.constant('activeProfile', profile);
    }]);

})();