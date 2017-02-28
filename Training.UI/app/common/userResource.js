(function() {
    "use strict";

    angular.module('common.services')
        .factory('userResource', ['$resource', '$cookies', 'appSettings', 'currentUser', userResource]);

    function userResource($resource, $cookies, appSettings, currentUser) {
        return {
            getUser: $resource(appSettings.serverPath + 'get/:id', null, {
                'getUser': {
                    method: 'GET',
                    isArray: false,
                    headers: {
                        'Authorization': 'Bearer ' + currentUser.getProfile().token,
                    }
                }
            }),
            getUsers: $resource(appSettings.serverPath + 'get/', null, {
                'getUsers': {
                    method: 'GET',
                    isArray: true,
                    headers: {
                        'Authorization': 'Bearer ' + $cookies.get('token'),
                    }
                }
            }),
            addUser: $resource(appSettings.serverPath + '/create', null, {
                'addUser': {
                    method: 'POST',
                    headers: {
                        'Authorization': 'Bearer ' + currentUser.getProfile().token,
                    }
                }
            }),
            updateUser: $resource(appSettings.serverPath + '/update', null, {
                'updateUser': {
                    method: 'PUT',
                    headers: {
                        'Authorization': 'Bearer ' + currentUser.getProfile().token,
                    }
                }
            }),
        };
    }
}());