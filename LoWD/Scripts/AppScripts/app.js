
var lowdApp = angular.module('lowdApp', ['ngRoute', 'ui.bootstrap', 'tableSort']);

lowdApp.config(['$routeProvider', function ($routeProvider) {
    $routeProvider
    .when('/', {
        controller: 'homeController',
        templateUrl: '/lowd/Content/Views/Landing.html',
        caseInsensitiveMatch: true
    })
    .when('/New', {
        controller: 'newController',
        templateUrl: '/lowd/Content/Views/New.html',
        caseInsensitiveMatch: true
    })
    .when('/Games/:id?', {
        controller: 'showController',
        templateUrl: '/lowd/Content/Views/Show.html',
        caseInsensitiveMatch: true
    })
    .when('/Tournaments/:id?', {
        controller: 'tournamentsController',
        templateUrl: '/lowd/Content/Views/Tournaments.html',
        caseInsensitiveMatch: true
    })
    .when('/Stats', {
        controller: 'homeController',
        templateUrl: '/lowd/Content/Views/Index.html',
        caseInsensitiveMatch: true
    })
    .when('/LordStats', {
        controller: 'lordStatsController',
        templateUrl: '/lowd/Content/Views/LordStats.html',
        caseInsensitiveMatch: true
    })
    .otherwise({
        controller: 'homeController',
        templateUrl: '/lowd/Content/Views/Landing.html',
        caseInsensitiveMatch: true
    })
}])