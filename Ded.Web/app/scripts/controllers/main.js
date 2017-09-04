'use strict';

angular.module('app')
    .controller('MainCtrl', function ($scope) {
        $scope.employee = {};
        $scope.dependent = {};
        $scope.employee.dependents = [];

        $scope.saveEmployee = function ()
        {
            $scope.employee.dependents.push($scope.dependent);
            $scope.dependent = null;
        }

        $scope.clearForm = function () {
            $scope.employee = null;
            $scope.dependent = null;
        }
  });
