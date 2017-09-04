(function () {
    var employeeFactory = function ($http, defaultSettings) {
        var factory = {};

        factory.saveEmployee = function (employee) {
            return $http({
                url: defaultSettings.serverURL + 'api/Employee',
                method: "POST",
                data: angular.toJson(employee),
                headers: { 'Content-Type': 'application/json; charset=utf-8' }
            })
        }

        factory.getRelationships = function () {
            return $http({
                url: defaultSettings.serverURL + '/api/Relationships',
                method: "GET"
            })
        }

        factory.getEmployees = function () {
            return $http({
                url: defaultSettings.serverURL + 'api/Employee',
                method: "GET"
            });
        }

        factory.getPaycheck = function (id) {
            return $http({
                url: defaultSettings.serverURL + 'api/Paycheck/' + id,
                method: "GET"
            }); 
        }
         return factory;
    };

    employeeFactory.$inject = ['$http', 'defaultSettings'];
    angular.module('app').factory('employeeFactory', employeeFactory);
}());
