app.controller('usersListCtrl', ['userResource', function(userResource) {
    var vm = this;
    userResource.getUsers.getUsers(function(data) {
        vm.users = data;
    });
}]);