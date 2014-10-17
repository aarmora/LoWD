
var lowdApp = angular.module('lowdApp', ['ngRoute']);

lowdApp.config(['$routeProvider', function ($routeProvider) {
    $routeProvider
    .otherwise({
        controller: 'homeController',
        templateUrl: '/Content/Views/Index.html'
    })
}])