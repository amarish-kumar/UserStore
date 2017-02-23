app.controller('loginCtrl', ['$scope', '$location', 'userAccount', loginCtrl]);

function loginCtrl($scope, $location, userAccount) {
    $scope.message = "errors will be shown here";
    $scope.isLoggedId = false;
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
                $scope.isLoggedId = true;
                $scope.token = data.access_token;
            },
            function(response) {
                $scope.password = "";
                $scope.isLoggedId = true;
                $scope.message = response.statusText + "\r\n";
                if (response.data.exceptionMessage)
                    $scope.message += response.data.exceptionMessage;

                if (response.data.modelState)
                    for (var key in response.data.modelState) {
                        $scope.message += response.data.modelState[key] + "\r\n";
                    }
            });

        if ($scope.isLoggedId) {
            $location.path('/register');
        }
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