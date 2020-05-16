app.controller('postCtrl',
    ['$scope', '$rootScope', 'baseFactory', '$timeout', '$http', function ($scope, $rootScope, baseFactory, $timeout, $http) {

        //$http.defaults.headers.post["Content-Type"] = "application/x-www-form-urlencoded";

        $scope.MVCController = 'Post';
        $scope.Path = '../app/controllers/post/';
        $scope.View = '';

        $scope.Post = {};
        
        this.$onInit = function () {
            $scope.View = $scope.Path + 'form.html';
        }

        $scope.Methods = {

            SendForApproval: function () {

                if ($scope.Validations.ValidatePost()) {
                    baseFactory.request(
                        $scope.MVCController,
                        'SavePost',
                        $scope.Post
                    ).then(function successCallback(response) {

                        if (response != null && response.data.code == "1") {
                            $scope.Alert = 'alert alert-success';
                            $scope.Post = {};
                        } else {
                            $scope.Alert = 'alert alert-danger';
                        }

                        $scope.Message = response.data.message;
                        $scope.DeleteMessage(2000);

                    }, function errorCallback(response) {


                    });
                }
            }
        }

        $scope.Validations = {

            ValidatePost: function (){
                if ($scope.Post.Title == null || $scope.Post.Title == '')
                {
                    $scope.Message = 'Post title is required.';
                    $scope.Alert = 'alert alert-danger';
                    $scope.DeleteMessage(2000);
                    return false;
                }
                if ($scope.Post.Body == null || $scope.Post.Body == '') {
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