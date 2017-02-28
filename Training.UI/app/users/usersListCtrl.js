app.controller('usersListCtrl', ['userResource', 'currentUser', '$cookies', function(userResource, currentUser, $cookies) {
    var vm = this;

    userResource.getUsers.getUsers(function(data) {
        vm.users = data;
    });
}]);