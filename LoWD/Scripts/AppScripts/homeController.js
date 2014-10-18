
(function(){
    
    var homeController = function($scope, $http){
        $scope.users = [];

        $scope.addUser = function () {
            if ($scope.users.length < 6) {
                $scope.users.push(
                    { "user_id": "", "lord_id": "", "lord_pts": "", "corruption_pts": "", "gold_pts": "", "adv_pts": "", "quest_pts": "", "quest_qty": "" }
                );
            }
        }

        $scope.removeUser = function (index) {
            $scope.users.splice(index, 1)
        }

        $scope.submitGame = function () {
            $http({
                url: 'newGame',
                method: 'POST',
                params: {users:[$scope.users]}
            })
        }

    }

    homeController.$inject = ['$scope', '$http']

    lowdApp.controller('homeController', homeController)

}())