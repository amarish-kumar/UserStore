app.controller('registerCtrl', ['$scope', '$window', '$location', 'userAccount', registerCtrl]);

function registerCtrl($scope, $window, $location, userAccount) {
    $scope.message = "";
    $scope.user = {};
    $scope.user.firstName = "15";
    $scope.user.surname = "w";
    $scope.user.email = "15@15.com";
    $scope.user.dob = "01/20/2017";
    $scope.user.password = "15";

    $scope.register = function() {
        userAccount.registration.registerUser({}, $scope.user,
            function(data) {
                $scope.message = "... user registered";
                $location.path('/login');
            },
            function(data) {
                $scope.message = data;
            });
    };

    $scope.cancel = function() {
        $location.path('/login');
    };
}