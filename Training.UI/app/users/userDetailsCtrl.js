app.controller('userDetailsCtrl', ['$scope', '$window', '$location', 'userResource', 'currentUser', userDetailsCtrl]);

function userDetailsCtrl($scope, $window, $location, userResource, currentUser) {
    $scope.user = {};

    userResource.getUser.getUser({ id: currentUser.getProfile().id },
        function(data) {
            $scope.user = data;
        });

    $scope.edit = function() {
        $location.path('/editDetails');
    };

    $scope.cancel = function() {
        $window.history.back();
    };
}