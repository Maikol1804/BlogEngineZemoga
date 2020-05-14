app.controller('approvalpostsCtrl',
    ['$scope', '$rootScope', 'baseFactory', '$timeout', function ($scope, $rootScope, baseFactory, $timeout) {

        $scope.MVCController = 'ApprovalPosts';
        $scope.Path = '../app/controllers/approvalposts/';
        $scope.View = '';
        
        this.$onInit = function () {
            $scope.View = $scope.Path + 'form.html';
        }

    }
    ]);