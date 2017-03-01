app.controller('loginCtrl', ['$scope', '$location', 'userAccount', 'currentUser',
    loginCtrl
]);

function loginCtrl($scope, $location, userAccount, currentUser) {
    $scope.message = "errors will be shown here";
    $scope.isLoggedId = function() {
        return currentUser.getProfile().isLoggedId;
    };
    $scope.userData = {
        userName: '',
        email: '',
        password: ''
    };
    currentUser.setProfile('', '', '', '');

    $scope.login = function() {
        $scope.userData.grant_type = "password";
        $scope.userData.userName = $scope.username;
        $scope.userData.password = $scope.password;
        userAccount.login.loginUser($scope.userData,
            function(data) {
                $scope.password = "";
                $scope.message = "";
                currentUser.setProfile($scope.userData.username, data.access_token, data.id, data.role);
                $location.path('/userDetails');
            },
            function(response) {
                $scope.password = "";
                $scope.message = response;
            });
    };

    $scope.register = function() {
        $location.path('/register');
    };
}