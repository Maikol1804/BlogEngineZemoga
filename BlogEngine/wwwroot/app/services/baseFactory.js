(function () {
    'use strict';

    angular
        .module('app')
        .factory('baseFactory', ["$http",
        function ($http) {
            return {
                request: function (ctrl, opt, data) {
                    return $http.post('/' + ctrl + '/' + opt, data);
                },
            };
        }
    ]);

})();
