app.controller('registerCtrl', ['$scope', '$window', 'userAccount', registerCtrl]);

function registerCtrl($scope, $window, userAccount) {
    $scope.message = "";
    $scope.user = {};
    $scope.user.firstName = "11";
    $scope.user.surname = "q";
    $scope.user.email = "11@11.com";
    $scope.user.dob = "27/02/2017";
    $scope.user.password = "11";

    $scope.register = function() {
        userAccount.registration.registerUser({}, $scope.user,
            function(data) {
                // $scope.message = "... user registered";
                alert("user registered");
                $location.path('/userDetails');

            },
            function(data) {
                $scope.message = data;
            });
    };

    $scope.cancel = function() {
        $window.history.back();
    };
}