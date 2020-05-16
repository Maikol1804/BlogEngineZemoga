app.controller('pendingpostsCtrl',
    ['$scope', '$rootScope', 'baseFactory', '$timeout', function ($scope, $rootScope, baseFactory, $timeout) {

        $scope.MVCController = 'PendingPosts';
        $scope.Path = '../app/controllers/pendingposts/';
        $scope.View = '';
        
        this.$onInit = function () {
            $scope.View = $scope.Path + 'form.html';
        }



    }
    ]);