
var lowdApp = angular.module('lowdApp', ['ngRoute']);

lowdApp.config(['$routeProvider', function ($routeProvider) {
    $routeProvider
    .when('/', {
        controller: 'homeController',
        templateUrl: '/Content/Views/Index.html'
    })
    .when('/New', {
        controller: 'homeController',
        templateUrl: '/Content/Views/New.html',
        caseInsensitiveMatch: true
    })
    .when('/Games/:id?', {
        controller: 'homeController',
        templateUrl: '/Content/Views/Show.html',
        caseInsensitiveMatch: true
    })
    .otherwise({
        controller: 'homeController',
        templateUrl: '/Content/Views/Index.html'
    })
}])