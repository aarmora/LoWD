
(function(){
    
    var homeController = function($scope, $http){
        $scope.pizza = 'this';

    }

    homeController.$inject = ['$scope', '$http']

    lowdApp.controller('homeController', homeController)

}())