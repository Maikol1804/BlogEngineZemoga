app.controller('authenticateCtrl',
    ['$scope', '$rootScope', 'baseFactory', '$timeout', function ($scope, $rootScope, baseFactory, $timeout) {

        $scope.MVCController = 'Authenticate';
        $scope.Path = '../app/controllers/authenticate/';
        $scope.View = '';
        
        this.$onInit = function () {
            $scope.View = $scope.Path + 'form.html';
        }

    }
    ]);