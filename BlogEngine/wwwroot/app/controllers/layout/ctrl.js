app.controller('layoutCtrl',
    ['$scope', '$rootScope', 'baseFactory', '$timeout', '$interval', '$sce', "$window", function ($scope, $rootScope, baseFactory, $timeout, $interval, $sce, $window) {
        $scope.MVCController = 'Home';
        $scope.UsersController = 'Users';
        $scope.Path = '../app/controllers/layout/';

        $scope.rolTypes = {
            WRITER: '1',
            EDITOR: '2'
        }

        toastr.options = {
            "closeButton": true,
            "debug": false,
            "newestOnTop": true,
            "progressBar": true,
            "positionClass": "toast-top-right",
            "preventDuplicates": false,
            "onclick": null,
            "showDuration": "300",
            "hideDuration": "1000",
            "timeOut": "3000",
            "extendedTimeOut": "1000",
            "showEasing": "swing",
            "hideEasing": "linear",
            "showMethod": "fadeIn",
            "hideMethod": "fadeOut"
        }

        var keyLocation = "locationSession";
        var locationSession = $window.sessionStorage.getItem(keyLocation);

        var controller = "";
        if (locationSession != null)
            controller = locationSession;
        else
            controller = (location.pathname).replace("/", "");

        if (controller == "")
            controller = "Home";

        $scope.contentUrl = 'app/controllers/' + controller + '/view.html';

        this.$onInit = function () {

        }

        $scope.ChangeContentUrl = function (controller) {
            $scope.contentUrl = 'app/controllers/' + controller + '/view.html';
            $window.sessionStorage.setItem(keyLocation, controller);
        }

        $scope.Logout = function () {

            baseFactory.request(
                $scope.MVCController,
                'Logout',
                {}
            ).then(function successCallback(response) {

                $window.sessionStorage.removeItem(keyLocation);
                $scope.ChangeContentUrl("Home");
                $window.location.reload();

            }, function errorCallback(response) {
            });

        }

    }
    ]);