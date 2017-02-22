var myApp = angular.module('userStore', ['ui.router', 'common.services']);

myApp.controller('oneController', ['$scope', function($scope) {
    $scope.greeting = "welcome to angular from controller";
}]);

myApp.config(function($stateProvider) {

    var aboutState = {
        name: 'about',
        url: '/about',
        template: '<h3>Its the UI-Router hello world app!</h3>'
    };

    var usersListState = {
        name: 'usersList',
        controller: 'usersListCtrl',
        url: '/users',
        templateUrl: 'app/users/usersListView.html'
    };

    $stateProvider.state(aboutState);
    $stateProvider.state(usersListState);
});