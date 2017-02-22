(function() {
    "use strict";

    angular.module('userStore')
        .controller('usersListCtrl', ['userResource', function(userResource) {
            var vm = this;
            userResource.query(function(data) {
                vm.users = data;
            });
        }]);
}());