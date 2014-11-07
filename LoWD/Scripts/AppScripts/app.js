
var lowdApp = angular.module('lowdApp', ['ngRoute', 'ui.bootstrap']);

lowdApp.config(['$routeProvider', function ($routeProvider) {
    $routeProvider
    .when('/New', {
        controller: 'newController',
        templateUrl: '/Content/Views/New.html',
        caseInsensitiveMatch: true
    })
    .when('/Games/:id?', {
        controller: 'showController',
        templateUrl: '/Content/Views/Show.html',
        caseInsensitiveMatch: true
    })
    .otherwise({
        controller: 'homeController',
        templateUrl: '/Content/Views/Index.html'
    })
}])