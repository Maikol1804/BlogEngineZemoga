app.controller('pendingpostsCtrl',
    ['$scope', '$rootScope', 'baseFactory', '$timeout', function ($scope, $rootScope, baseFactory, $timeout) {

        $scope.MVCController = 'PendingPosts';
        $scope.Path = '../app/controllers/pendingposts/';
        $scope.View = '';

        $scope.PendigPosts = [];

        this.$onInit = function () {
            $scope.Methods.GetPendingPosts();
            $scope.View = $scope.Path + 'form.html';
        }

        $scope.Methods = {

            GetPendingPosts: function () {

                baseFactory.request(
                    $scope.MVCController,
                    'GetAllPendingPosts',
                    {}
                ).then(function successCallback(response) {

                    if (response != null && response.data.code == "1") {

                        if (response.data.data.length == 0) {
                            $scope.Alert = 'alert alert-success';
                        }
                        
                        $scope.PendigPosts = response.data.data; 
                    } else {
                        $scope.Alert = 'alert alert-danger';
                    }

                    $scope.Message = response.data.message;
                    $scope.DeleteMessage(5000);

                }, function errorCallback(response) {
                });
                
            }
        }

        $scope.DeleteMessage = function (time) {
            $timeout(function () {
                $scope.Message = '';
            }, time);
        }

    }
    ]);