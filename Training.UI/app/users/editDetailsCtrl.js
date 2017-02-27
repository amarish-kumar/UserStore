app.controller('editDetailsCtrl', ['$scope', '$window', 'userResource', 'currentUser', editDetailsCtrl]);

function editDetailsCtrl($scope, $window, userResource, currentUser) {
    $scope.user = {};

    userResource.getUser.getUser({ id: currentUser.getProfile().id },
        function(data) {
            $scope.user = data;
        });

    $scope.update = function() {

        userResource.updateUser.updateUser({}, $scope.user,
            function(data) {
                $scope.message = "... update successfull";
            });
    };

    $scope.cancel = function() {
        $window.history.back();
    };
}