(function() {
    "use strict";

    angular.module('common.services')
        .factory('userResource', ['$resource', '$cookies', 'appSettings', 'currentUser', userResource]);

    function userResource($resource, $cookies, appSettings, currentUser) {
        return {
            getUser: $resource(appSettings.serverPath + 'api/v1/get/:id', null, {
                'getUser': {
                    method: 'GET',
                    isArray: false,
                }
            }),
            getUsers: $resource(appSettings.serverPath + 'api/v1/get', null, {
                'getUsers': {
                    method: 'GET',
                    isArray: true,
                }
            }),
            updateUser: $resource(appSettings.serverPath + 'api/v1/update', null, {
                'updateUser': {
                    method: 'PUT',
                }
            }),
        };
    }
}());