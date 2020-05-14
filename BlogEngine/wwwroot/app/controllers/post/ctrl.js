app.controller('postCtrl',
    ['$scope', '$rootScope', 'baseFactory', '$timeout', function ($scope, $rootScope, baseFactory, $timeout) {

        $scope.MVCController = 'Post';
        $scope.Path = '../app/controllers/post/';
        $scope.View = '';
        
        this.$onInit = function () {
            $scope.View = $scope.Path + 'form.html';
        }

    }
    ]);