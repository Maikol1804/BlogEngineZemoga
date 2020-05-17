app.controller('homeCtrl',
    ['$scope', '$rootScope', 'baseFactory', '$timeout', function ($scope, $rootScope, baseFactory, $timeout) {

        $scope.MVCController = 'Home';
        $scope.Path = '../app/controllers/home/';
        $scope.View = '';

        $scope.ApprovedPosts = [];

        this.$onInit = function () {
            $scope.Methods.GetAllApprovedPosts();
            $scope.View = $scope.Path + 'form.html';
        }

        $scope.Methods = {

            GetAllApprovedPosts: function () {

                baseFactory.request(
                    $scope.MVCController,
                    'GetAllApprovedPosts',
                    {}
                ).then(function successCallback(response) {

                    if (response != null && response.data.code == "1") {

                        if (response.data.data.length == 0) {
                            $scope.Alert = 'alert alert-success';
                        }

                        $scope.ApprovedPosts = response.data.data;
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