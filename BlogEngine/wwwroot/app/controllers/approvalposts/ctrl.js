app.controller('approvalpostsCtrl',
    ['$scope', '$rootScope', 'baseFactory', '$timeout', function ($scope, $rootScope, baseFactory, $timeout) {

        $scope.MVCController = 'ApprovalPosts';
        $scope.Path = '../app/controllers/approvalposts/';
        $scope.View = '';

        $scope.WrittenPosts = [];

        this.$onInit = function () {
            $scope.Methods.GetWrittenPosts();
            $scope.View = $scope.Path + 'form.html';
        }

        $scope.Methods = {

            GetWrittenPosts: function () {

                baseFactory.request(
                    $scope.MVCController,
                    'GetAllWrittenPosts',
                    {}
                ).then(function successCallback(response) {

                    if (response != null && response.data.code == "1") {

                        if (response.data.data.length == 0) {
                            $scope.Alert = 'alert alert-success';
                        }

                        $scope.WrittenPosts = response.data.data;
                    } else {
                        $scope.Alert = 'alert alert-danger';
                    }

                    $scope.Message = response.data.message;
                    $scope.DeleteMessage(5000);

                }, function errorCallback(response) {
                });
            },

            Approve: function (Id) {
                baseFactory.request(
                    $scope.MVCController,
                    'ApprovePost',
                    Id
                ).then(function successCallback(response) {

                    if (response != null && response.data.code == "1") {

                        toastr.success(response.data.message);
                        $scope.Methods.GetWrittenPosts();

                    } else {
                        $scope.Alert = 'alert alert-danger';
                    }

                    $scope.Message = response.data.message;
                    $scope.DeleteMessage(5000);

                }, function errorCallback(response) {
                });
            },

            Reject: function (Id) {
                baseFactory.request(
                    $scope.MVCController,
                    'RejectPost',
                    Id
                ).then(function successCallback(response) {

                    if (response != null && response.data.code == "1") {

                        toastr.success(response.data.message);
                        $scope.Methods.GetWrittenPosts();

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