app.controller('authenticateCtrl',
    ['$scope', '$rootScope', 'baseFactory', '$timeout', '$window', function ($scope, $rootScope, baseFactory, $timeout, $window) {

        $scope.MVCController = 'Authenticate';
        $scope.Path = '../app/controllers/authenticate/';
        $scope.View = '';

        $scope.User = {};
        
        this.$onInit = function () {
            $scope.View = $scope.Path + 'form.html';
        }

        $scope.Methods = {

            Login: function () {

                if ($scope.Validations.ValidateUser()) {
                    baseFactory.request(
                        $scope.MVCController,
                        'Login',
                        $scope.User
                    ).then(function successCallback(response) {

                        if (response != null && response.data.code == "1") {

                            $scope.Alert = 'alert alert-success';

                            $scope.ChangeContentUrl('Home');
                            $window.location.reload();

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
                if ($scope.User.Username == null || $scope.User.Username == '') {
                    $scope.Message = 'Username is required.';
                    $scope.Alert = 'alert alert-danger';
                    $scope.DeleteMessage(2000);
                    return false;
                }
                if ($scope.User.Password == null || $scope.User.Password == '') {
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