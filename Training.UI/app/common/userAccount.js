(function() {
    "use strict";

    angular.module('common.services')
        .factory('userAccount', ['$resource', 'appSettings', userAccount]);

    function userAccount($resource, appSettings) {
        return {
            registration: $resource(appSettings.serverPath + "api/v1/create", null, {
                'registerUser': { method: 'POST' }
            }),
            login: $resource(appSettings.serverPath + "/token", null, {
                'loginUser': {
                    method: 'POST',
                    headers: { 'Content-Type': 'application/x-www-form-urlencoded' },
                    transformRequest: function(data, headersGetter) {
                        var str = [];
                        for (var d in data)
                            str.push(encodeURIComponent(d) + "=" + encodeURIComponent(data[d]));
                        return str.join("&");
                    }
                }
            })
        };
    }
}());