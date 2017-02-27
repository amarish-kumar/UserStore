(function() {
    "use strict";

    angular.module('common.services')
        .factory('currentUser', currentUser);

    function currentUser() {
        var profile = {
            username: "",
            token: "",
            isLoggedIn: false,
            id: "",
        };

        var setProfile = function(username, token, id) {
            profile.username = username;
            profile.token = token;
            profile.isLoggedIn = true;
            profile.id = id;
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