
﻿
(function(){
    
    var newController = function ($scope, $http) {
        $http({
            url: '/lowd/home/getInfo',
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
            $scope.disableSubmit = true;
            $http({
                url: '/lowd/home/newGame',
                method: 'POST',
                params: { users: [$scope.users], gameInfo: $scope.game }
            }).success(function (id) {
                window.location.replace('#/Games/' + id);
            });
        }

        $scope.verify = function (verifyUser) {
            if (verifyUser === 'welord4U') {
                $scope.verified = true;
            }
        };

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
            url: '/lowd/Home/getGames',
            method: 'GET'
        }).success(function (data) {
            $scope.games = data;
            console.log(data);
        });

    };

    var homeController = function ($scope, $http) {


        $http({
            url: '/Home/getLeaderboard',
            method: 'GET'
        }).success(function (data) {
            $scope.leaderboard = data;
            console.log(data);
        });

    };

    var tournamentsController = function ($scope, $http) {



    };

    homeController.$inject = ['$scope', '$http']
    lowdApp.controller('homeController', homeController)

    showController.$inject = ['$scope', '$http', '$routeParams']
    lowdApp.controller('showController', showController)

    newController.$inject = ['$scope', '$http']
    lowdApp.controller('newController', newController)

    tournamentsController.$inject = ['$scope', '$http']
    lowdApp.controller('tournamentsController', tournamentsController)

}())