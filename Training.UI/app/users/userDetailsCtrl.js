app.controller('userDetailsCtrl', ['$scope', '$window', '$location', '$cookies', 'userResource', 'currentUser', userDetailsCtrl]);

function userDetailsCtrl($scope, $window, $location, $cookies, userResource, currentUser) {
    $scope.user = {};
    $scope.isAdmin = function() {
        if (currentUser.getProfile().role == "admin")
            return true;
        else
            return false;
    };

    userResource.getUser.getUser({ id: currentUser.getProfile().id },
        function(data) {
            $scope.user = data;
        });

    $scope.edit = function() {
        $location.path('/editDetails');
    };

    $scope.logout = function() {
        currentUser.setProfile("", "", "", "");
        $location.path('/login');
    };

    $scope.showUsers = function() {
        $location.path('/users');
    };
}