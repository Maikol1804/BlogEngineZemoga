app.controller('authenticateCtrl',
    ['$scope', '$rootScope', 'baseFactory', '$timeout', function ($scope, $rootScope, baseFactory, $timeout) {

        $scope.MVCController = 'Authenticate';
        $scope.Path = '../app/controllers/authenticate/';
        $scope.View = '';
        
        this.$onInit = function () {
            $scope.View = $scope.Path + 'form.html';
        }

        $scope.Methods = {

            SendForApproval: function () {

                if ($scope.Validations.ValidateUser()) {
                    baseFactory.request(
                        $scope.MVCController,
                        'SignIn',
                        $scope.User
                    ).then(function successCallback(response) {

                        if (response != null && response.data.code == "1") {
                            $scope.Alert = 'alert alert-success';


                            //TODO ChangeLayaout

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

            ValidateUser: function () {
                if ($scope.User.Username == null || $scope.Post.Username == '') {
                    $scope.Message = 'Username is required.';
                    $scope.Alert = 'alert alert-danger';
                    $scope.DeleteMessage(2000);
                    return false;
                }
                if ($scope.Post.Password == null || $scope.Post.Password == '') {
                    $scope.Message = 'Password is required.';
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