app.controller('createdpostsCtrl',
    ['$scope', '$rootScope', 'baseFactory', '$timeout', function ($scope, $rootScope, baseFactory, $timeout) {

        $scope.MVCController = 'CreatedPosts';
        $scope.Path = '../app/controllers/createdposts/';
        $scope.View = '';

        $scope.CreatedPosts = [];

        this.$onInit = function () {
            $scope.Methods.GetCreatedPosts();
            $scope.View = $scope.Path + 'form.html';
        }

        $scope.Methods = {

            GetCreatedPosts: function () {

                baseFactory.request(
                    $scope.MVCController,
                    'GetAllCreatedPosts',
                    {}
                ).then(function successCallback(response) {

                    if (response != null && response.data.code == "1") {

                        if (response.data.data.length == 0) {
                            $scope.Alert = 'alert alert-success';
                        }

                        $scope.CreatedPosts = response.data.data;
                    } else {
                        $scope.Alert = 'alert alert-danger';
                    }

                    $scope.Message = response.data.message;
                    $scope.DeleteMessage(5000);

                }, function errorCallback(response) {
                });
            },

            UpdatePost: function (Post) {

                if ($scope.Validations.ValidatePost(Post)) {
                    baseFactory.request(
                        $scope.MVCController,
                        'UpdatePost',
                        Post
                    ).then(function successCallback(response) {

                        if (response != null && response.data.code == "1") {

                            toastr.success(response.data.message);
                            $scope.Methods.GetCreatedPosts();

                        } else {
                            $scope.Alert = 'alert alert-danger';
                        }

                        $scope.Message = response.data.message;
                        $scope.DeleteMessage(5000);

                    }, function errorCallback(response) {
                    });
                }
            },

            Edit: function (id) {

                for (var i = 0; i < $scope.CreatedPosts.length; ++i) {
                    if ($scope.CreatedPosts[i].id == id) {
                        $scope.CreatedPosts[i].IsEditing = true;
                        break;
                    }
                }

            }

        }

        $scope.Validations = {

            ValidatePost: function (Post) {

                $scope.DeleteMessage(2000);
                if (Post.title == null || Post.title == '') {
                    $scope.Message = 'Post title is required.';
                    $scope.Alert = 'alert alert-danger';
                    toastr.error($scope.Message);
                    return false;
                }
                if (Post.body == null || Post.body == '') {
                    $scope.Message = 'Post body is required.';
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