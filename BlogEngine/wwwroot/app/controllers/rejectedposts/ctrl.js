app.controller('rejectedpostsCtrl',
    ['$scope', '$rootScope', 'baseFactory', '$timeout', function ($scope, $rootScope, baseFactory, $timeout) {

        $scope.MVCController = 'RejectedPosts';
        $scope.Path = '../app/controllers/rejectedposts/';
        $scope.View = '';

        $scope.RejectedPosts = [];

        this.$onInit = function () {
            $scope.Methods.GetRejectedPosts();
            $scope.View = $scope.Path + 'form.html';
        }

        $scope.Methods = {

            GetRejectedPosts: function () {

                baseFactory.request(
                    $scope.MVCController,
                    'GetAllRejectedPosts',
                    {}
                ).then(function successCallback(response) {

                    if (response != null && response.data.code == "1") {

                        if (response.data.data.length == 0) {
                            $scope.Alert = 'alert alert-success';
                        }

                        $scope.RejectedPosts = response.data.data;
                    } else {
                        $scope.Alert = 'alert alert-danger';
                    }

                    $scope.Message = response.data.message;
                    $scope.DeleteMessage(5000);

                }, function errorCallback(response) {
                });
            },

            SendToCheck: function (Post) {

                if ($scope.Validations.ValidatePost(Post)) {
                    baseFactory.request(
                        $scope.MVCController,
                        'UpdatePost',
                        Post
                    ).then(function successCallback(response) {

                        if (response != null && response.data.code == "1") {
                            
                            $scope.Methods.GetRejectedPosts();

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

        $scope.Validations = {

            ValidatePost: function (Post) {
                if (Post.title == null || Post.title == '') {
                    $scope.Message = 'Post title is required.';
                    $scope.Alert = 'alert alert-danger';
                    $scope.DeleteMessage(2000);
                    return false;
                }
                if (Post.body == null || Post.body == '') {
                    $scope.Message = 'Post body is required.';
                    $scope.Alert = 'alert alert-danger';
                    $scope.DeleteMessage(2000);
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