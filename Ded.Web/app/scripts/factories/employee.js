(function () {
    var employeeFactory = function ($http, defaultSettings) {
        var factory = {};

        factory.saveEmployee = function (employee) {
            return $http({
                url: defaultSettings.serverURL + '/api/employee/create',
                method: "POST",
                data: angular.toJson(employee),
                headers: { 'Content-Type': 'application/json; charset=utf-8' }
            })
        }
         return factory;
    };

    employeeFactory.$inject = ['$http', 'defaultSettings'];
    angular.module('app').factory('employeeFactory', employeeFactory);
}());
