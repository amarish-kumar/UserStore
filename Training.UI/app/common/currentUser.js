(function() {
    "use strict";

    angular.module('common.services')
        .factory('currentUser', ['$cookies', currentUser]);

    function currentUser($cookies) {
        var profile = {
            username: "",
            token: "",
            isLoggedIn: false,
            id: "",
        };

        var setProfile = function(username, token, id, role) {
            profile.username = username;
            profile.token = token;
            profile.isLoggedIn = true;
            profile.id = id;
            profile.role = role;
            $cookies.put('token', token);
        };

        var getProfile = function() {
            return profile;
        };

        return {
            setProfile: setProfile,
            getProfile: getProfile
        };
    }
}());