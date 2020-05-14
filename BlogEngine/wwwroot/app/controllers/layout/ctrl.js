app.controller('layoutCtrl',
    ['$scope', '$rootScope', 'baseFactory', '$timeout', '$interval', '$sce', "$window", function ($scope, $rootScope, baseFactory, $timeout, $interval, $sce, $window) {
        $scope.MVCController = 'Home';
        $scope.AccountController = 'Account';
        $scope.UsersController = 'Users';
        $scope.Path = '../app/controllers/layout/';
        $scope.ViewUserInfo = { Url: '', Loaded: false };
        $scope.ViewSideMenu = { Url: '', Loaded: false };
        $scope.Roles = {};
        $scope.AssignedRoles = [];

        $scope.UserInfo = {
            Username: '',
            Rol: '-'
        };
        $scope.Menu = {};

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
            //$scope.getMenuName(controller);
            //$scope.LoadUserInfo();
            //$scope.TraerRoles();
            //$scope.ViewUserInfo.Url = $scope.Path + 'userinfo.html';
            //$scope.ViewSideMenu.Url = $scope.Path + 'menu.html';
        }

        $scope.changeContentUrl = function (controller) {
            //$scope.LoadUserInfo();
            //$scope.TraerRoles();
            $scope.contentUrl = 'app/controllers/' + controller + '/view.html';
            $window.sessionStorage.setItem(keyLocation, controller);
        }

        $scope.onLoadingViewUserInfo = function () {
            $scope.ViewUserInfo.Loaded = true;
            $scope.VerifyLoader();
        }

        $scope.onLoadingViewSideMenu = function () {
            $scope.ViewSideMenu.Loaded = true;
            $scope.VerifyLoader();
        }

        $scope.VerifyLoader = function () {
            if ($scope.ViewUserInfo.Loaded && $scope.ViewSideMenu.Loaded) {
                app.LoaderClose(0);
            }
        }

        $scope.getMenuName = function (step) {
            if ($scope.ControllerName != undefined) $scope.ControllerName = undefined;
            if ($scope.ControllerSubTittle != undefined) $scope.ControllerSubTittle = undefined;
            baseFactory.request(step, "GetNameController", {}).success(function (result) {
                $scope.ControllerName = result.Controller;
                $scope.ControllerSubTittle = result.SubTitle;
            });
        }

        $scope.LoadUserInfo = function () {

            baseFactory.request(
                'Parameters',
                'getCompaniesByCurrentUserExternal',
                {}
            ).success(function (result) {
                $scope.Combos.CompaniesBelongs = result.Data1;

                baseFactory.request(
                    $scope.MVCController,
                    'getUserInfo',
                    {}
                ).success(function (result) {
                    $scope.UserInfo = result.Data1;
                    //$scope.CurrentCompany = result.Data2;

                    if (result.Data1 != null && result.Data2 != null) {
                        $scope.TraerAsignaciones(result.Data1.Id, result.Data2.Id);
                    }

                    if (result.Data2 != null) {
                        $scope.Combos.CompanyBelongs.Id = result.Data2.Id;
                        $scope.Combos.CompanyBelongs.Code = result.Data2.DocumentNumber;
                        $scope.Combos.CompanyBelongs.Value = result.Data2.TradeName;
                        $scope.Combos.CompanyBelongs.EncryptedId = result.Data2.EncryptedId;
                    }

                    $scope.HasCompanies($scope.UserInfo.EncryptedId);

                    baseFactory.request(
                        'Account',
                        'getAppAmbiente',
                        {}
                    ).success(function (result) {
                        if (result.Code == "1") {
                            $scope.UserInfo.appAmbiente = result.Message;
                        } else {
                            $scope.UserInfo.appAmbiente = "";
                        }
                    }).error(function (result, status) {
                        $scope.UserInfo.appAmbiente = "";
                    });

                }).error(function (result, status) {
                    app.LoaderClose(0);
                });

            }).error(function (result, status) {
                $scope.Combos.CompaniesBelongs = [];
            });

        }

        $scope.LoadMenu = function () {
            baseFactory.request(
                $scope.MVCController,
                'getMenu',
                {}
            ).success(function (result) {
                $scope.Menu = result.Data1;
            }).error(function (result, status) {

            });
        }

        $scope.LogOff = function () {
            baseFactory.request(
                $scope.AccountController,
                'LogOff'
            ).success(function (result) {
                $window.sessionStorage.removeItem(keyLocation)
                window.location.replace("/Account")
            });
        }

    }
    ]);