app.controller('homeCtrl',
    ['$scope', '$rootScope', 'baseFactory', '$timeout', function ($scope, $rootScope, baseFactory, $timeout) {

        $scope.MVCController = 'Home';
        $scope.Path = '../app/controllers/home/';
        $scope.View = '';

        $scope.ApprovedPosts = [];
        $scope.LoggedInUser = {};

        this.$onInit = function () {
            $scope.Methods.GetLoggedInUser();
            $scope.Methods.GetAllApprovedPosts();
            $scope.View = $scope.Path + 'form.html';
        }

        $scope.Methods = {

            GetLoggedInUser: function () {
                baseFactory.request(
                    $scope.MVCController,
                    'GetLoggedInUser',
                    {}
                ).then(function successCallback(response) {

                    if (response != null && response.data.code == "1") {
                        $scope.LoggedInUser = response.data.data;
                    }

                }, function errorCallback(response) {
                });
            },

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
            },

            AddComment: function (post) {

                if ($scope.Validations.ValidateComment(post.CurrentComment)) {
                    baseFactory.request(
                        $scope.MVCController,
                        'AddComment',
                        post
                    ).then(function successCallback(response) {

                        if (response != null && response.data.code == "1") {
                            toastr.success(response.data.message);
                            post.CurrentComment = '';
                            $scope.Methods.GetAllApprovedPosts();
                        } else {
                            toastr.error(response.data.message);
                        }

                    }, function errorCallback(response) {
                    });
                }
            },

            DeletePost: function (id) {

                baseFactory.request(
                    $scope.MVCController,
                    'DeletePost',
                    id
                ).then(function successCallback(response) {

                    if (response != null && response.data.code == "1") {

                        toastr.success(response.data.message);
                        $scope.Methods.GetAllApprovedPosts();

                    } else {
                        $scope.Alert = 'alert alert-danger';
                    }

                    $scope.Message = response.data.message;
                    $scope.DeleteMessage(5000);

                }, function errorCallback(response) {
                });

            }

        }

        $scope.Validations = {

            ValidateComment: function (comment) {

                $scope.DeleteMessage(2000);
                if (comment == null || comment == '') {
                    $scope.Message = 'Comment cannot be empty';
                    $scope.Alert = 'alert alert-danger';
                    toastr.error($scope.Message);
                    return false;
                }
                return true;
            }
        }

        $scope.DeleteMessage = function (time) {
            $timeout(function () {
                $scope.Message = '';
            }, time);
        }

    }
    ]);