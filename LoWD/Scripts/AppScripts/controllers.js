
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
            url: '/lowd/Home/getLeaderboard',
            method: 'GET'
        }).success(function (data) {
            $scope.leaderboard = data;
            $scope.undermountain = function (type) {
                if (type === "1") {
                    return "Undermountain";
                }
            }
            $scope.skullport = function (type) {
                if (type === "1") {
                    return "Skullport";
                }
            }
            $scope.extended = function (type) {
                if (type === "1") {
                    return "+1";
                }
            }
        });

    };

    var lordStatsController = function ($scope, $http) {

        $http({
            url: '/lowd/Home/getLordLeaderboard',
            method: 'GET'
        }).success(function (data) {
            $scope.leaderboard = data;
            $scope.undermountain = function (type) {
                if (type === "1") {
                    return "Undermountain";
                }
            }
            $scope.skullport = function (type) {
                if (type === "1") {
                    return "Skullport";
                }
            }
            $scope.extended = function (type) {
                if (type === "1") {
                    return "+1";
                }
            }
        });

    };

    var tournamentsController = function ($scope, $http, $routeParams, $sce) {
        if ($routeParams.id) {
            $scope.tourney = $routeParams.id;
        }
        else {
            $scope.tourney = 3;
        };

        $scope.getChallongeDesc = function () {
            $http({
                url: '/tournaments/getChallongeDesc',
                method: 'GET',
                params: {tourney: $routeParams.id}
            }).success(function (data) {
                console.log(data);
                $scope.tournament = data.tournament;
            });
        }
        $scope.tourneyDesc = function () {
            if($scope.tournament){
                return $sce.trustAsHtml($scope.tournament.description);
            }
        }        
    };

    var adminTourneyController = function ($scope, $http) {
        $http({
            url: '/lowd/home/getInfo',
            method: 'GET'
        }).success(function (data) {
            $scope.info = data;
            console.log($scope.info);
            $scope.tournament = {};
            $scope.tournament.users = angular.copy(data.users);
            $scope.unSelected = [];
        });

        $scope.addUser = function (index) {
            //if nothing exists, create tournament object, users array, and then push
            if (!$scope.tournament) {
                $scope.tournament = {};
                $scope.tournament.users = [];
                $scope.tournament.users.push($scope.selectedUser);
                $scope.unSelected.splice(index, 1);
            }
            //if users doesn't exist, create array then push
            else if (!$scope.tournament.users) {
                $scope.tournament.users = [];
                $scope.tournament.users.push($scope.selectedUser);;
                $scope.unSelected.splice(index, 1);
            }
            //if tournament and users already exists, just push
            else {
                $scope.tournament.users.push($scope.selectedUser);
                $scope.unSelected.splice(index, 1);
            }
        }
        $scope.removeUser = function (index) {
            $scope.unSelected.push($scope.tournament.users[index]);
            $scope.tournament.users.splice(index, 1);
        }

        $scope.getChallongeTournies = function () {
            $http({
                url: '/lowd/tournaments/createTourney',
                method: 'POST',
                params: { tournament: $scope.tournament }
            }).success(function(data){
                console.log(data);
            });
        };
    }

    homeController.$inject = ['$scope', '$http']
    lowdApp.controller('homeController', homeController)

    lordStatsController.$inject = ['$scope', '$http']
    lowdApp.controller('lordStatsController', lordStatsController)

    showController.$inject = ['$scope', '$http', '$routeParams']
    lowdApp.controller('showController', showController)

    newController.$inject = ['$scope', '$http']
    lowdApp.controller('newController', newController)

    adminTourneyController.$inject = ['$scope', '$http']
    lowdApp.controller('adminTourneyController', adminTourneyController)

    tournamentsController.$inject = ['$scope', '$http', '$routeParams', '$sce']
    lowdApp.controller('tournamentsController', tournamentsController)

}())

lowdApp.filter('trustUrl', function ($sce) {
    return function (url) {
        return $sce.trustAsResourceUrl(url);
    };
});
