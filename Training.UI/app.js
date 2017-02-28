var app = angular.module('userStore', ['ui.router', 'ngCookies', 'common.services']);

app.config(function($stateProvider) {

    var usersListState = {
        name: 'usersList',
        controller: 'usersListCtrl',
        url: '/users',
        templateUrl: 'app/users/usersListView.html'
    };

    var loginState = {
        name: 'login',
        controller: 'loginCtrl',
        url: '/login',
        templateUrl: 'app/login/loginView.html'
    };

    var registerState = {
        name: 'register',
        controller: 'registerCtrl',
        url: '/register',
        templateUrl: 'app/registration/registerView.html'
    };

    var userDetails = {
        name: 'userDetails',
        controller: 'userDetailsCtrl',
        url: '/userDetails',
        templateUrl: 'app/users/userDetailsView.html'
    };

    var editDetails = {
        name: 'editDetails',
        controller: 'editDetailsCtrl',
        url: '/editDetails',
        templateUrl: 'app/users/editDetailsView.html'
    };

    $stateProvider.state(usersListState);
    $stateProvider.state(loginState);
    $stateProvider.state(registerState);
    $stateProvider.state(userDetails);
    $stateProvider.state(editDetails);
});