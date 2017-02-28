app.controller('userDetailsCtrl', ['$scope', '$window', '$location', '$cookies', 'userResource', 'currentUser', userDetailsCtrl]);

function userDetailsCtrl($scope, $window, $location, $cookies, userResource, currentUser) {
    $scope.user = {};
    $scope.isAdmin = function() {
        return Boolean(parseInt(currentUser.getProfile().role));
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