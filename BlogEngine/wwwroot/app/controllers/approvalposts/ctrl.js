app.controller('approvalpostsCtrl',
    ['$scope', '$rootScope', 'baseFactory', '$timeout', '$http', function ($scope, $rootScope, baseFactory, $timeout, $http) {

        $scope.MVCController = 'ApprovalPosts';
        $scope.Path = '../app/controllers/approvalposts/';
        $scope.View = '';
        $scope.UseAPI = true;

        $scope.WrittenPosts = [];

        this.$onInit = function () {
            $scope.Methods.GetWrittenPosts();
            $scope.View = $scope.Path + 'form.html';
        }

        $scope.Methods = {

            GetWrittenPosts: function () {

                if ($scope.UseAPI) {

                    var endpoint = document.location.href + 'api/approvalposts';
                    $http.get(endpoint).
                        then(function (response) {

                            if (response != null && response.status == 200) {

                                if (response.data.length == 0) {
                                    $scope.Alert = 'alert alert-success';
                                }

                                $scope.WrittenPosts = response.data;
                            } else {
                                $scope.Alert = 'alert alert-danger';
                            }

                            $scope.Message = (response.data.length == 0 ? "No one has written a post yet." : "");
                            $scope.DeleteMessage(5000);

                        }, function errorCallback(response) {

                        });

                } else {

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
                }

            },

            Approve: function (id) {

                if ($scope.UseAPI) {

                    var endpoint = document.location.href + 'api/approvalposts/' + id + '/approve';
                    $http.put(endpoint).
                        then(function (response) {

                            if (response != null && response.status == 200) {

                                toastr.success(response.data.message);
                                $scope.Methods.GetWrittenPosts();

                            } else {
                                $scope.Alert = 'alert alert-danger';
                            }

                            $scope.Message = response.data.message;
                            $scope.DeleteMessage(5000);

                        }, function errorCallback(response) {

                        });

                } else {

                    baseFactory.request(
                        $scope.MVCController,
                        'ApprovePost',
                        id
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

            },

            Reject: function (id) {

                if ($scope.UseAPI) {

                    var endpoint = document.location.href + 'api/approvalposts/' + id + '/reject';
                    $http.put(endpoint).
                        then(function (response) {

                            if (response != null && response.status == 200) {

                                toastr.success(response.data.message);
                                $scope.Methods.GetWrittenPosts();

                            } else {
                                $scope.Alert = 'alert alert-danger';
                            }

                            $scope.Message = response.data.message;
                            $scope.DeleteMessage(5000);

                        }, function errorCallback(response) {

                        });

                } else {

                    baseFactory.request(
                        $scope.MVCController,
                        'RejectPost',
                        id
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
        }

        $scope.DeleteMessage = function (time) {
            $timeout(function () {
                $scope.Message = '';
            }, time);
        }

    }
    ]);