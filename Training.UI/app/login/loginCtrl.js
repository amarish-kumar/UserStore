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

    $scope.login = function() {
        $scope.userData.grant_type = "password";
        // $scope.userData.userName = $scope.userData.email;
        $scope.userData.userName = $scope.username;
        $scope.userData.password = $scope.password;
        userAccount.login.loginUser($scope.userData,
            function(data) {
                $scope.password = "";
                $scope.message = "";
                currentUser.setProfile($scope.userData.username, data.access_token, data.id);
                $location.path('/userDetails');
            },
            function(response) {
                $scope.password = "";
                $scope.message = response.statusText + "\r\n";
                if (response.data.exceptionMessage)
                    $scope.message += data.error_description;

                if (response.data.modelState)
                    for (var key in response.data.modelState) {
                        $scope.message += response.data.modelState[key] + "\r\n";
                    }
            });

        // if ($scope.isLoggedId) {
        //     $location.path('/userDetails');
        // }
    };

    $scope.register = function() {
        $location.path('/register');
        userAccount.registration.registerUser($scope.userData,
            function(data) {
                $scope.message = "... Registration successfull";
                $scope.login();
            },
            function(response) {
                $scope.isLoggedId = false;
                $scope.message = response.statusText + "\r\n";
            });
    };
}