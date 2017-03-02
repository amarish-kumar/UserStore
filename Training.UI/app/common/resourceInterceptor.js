(function() {
    "use strict";

    app.factory('recourceInterceptor', ['$rootScope', '$cookies', 'appSettings', recourceInterceptor]);

    function recourceInterceptor($rootScope, $cookies, appSettings) {
        var service = this;

        service.request = function(config) {
            if (config.url.includes(appSettings.serverPath + 'api/')) {
                config.headers.Authorization = 'Bearer ' + $cookies.get('token');
                return config;
            } else
                return config;
        };
        return service;
    }
}());