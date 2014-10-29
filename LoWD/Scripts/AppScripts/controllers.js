
﻿
(function(){
    
    var newController = function ($scope, $http) {
        $http({
            url: 'getInfo',
            method: 'GET'
        }).success(function (data) {
            $scope.info = data;
            console.log($scope.info);
        });
        
        $scope.users = [];
        $scope.game = {'undermountain_flag':'0', 'skullport_flag':'0', 'plus_one_flag':'0'}

        $scope.addUser = function () {
            if ($scope.users.length < 6) {
                $scope.users.push(
                    { "user_id": "", "lord_id": "", "lord_pts": "0", "corruption_pts": "0", "gold_pts": "0", "adv_pts": "0", "quest_pts": "0", "quest_qty": "0" }
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
                params: {users:[$scope.users], gameInfo:$scope.game}
            })
        }

    }

    var showController = function ($scope, $http, $routeParams) {
        $scope.gameId = $routeParams.id;
        if ($routeParams.id) {
            $scope.Filter = { game_id: $scope.gameId };
            $scope.showAll = true;
        }
        else {
            $scope.Filter = {};
        }


        $http({
            url: '/Home/getGames',
            method: 'GET'
        }).success(function (data) {
            $scope.games = data;
            console.log(data);
        });

    };

    showController.$inject = ['$scope', '$http', '$routeParams']
    lowdApp.controller('showController', showController)

    newController.$inject = ['$scope', '$http']
    lowdApp.controller('newController', newController)

}())