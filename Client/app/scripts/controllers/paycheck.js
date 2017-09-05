'use strict';
(function () {
    var PaycheckCtrl = function ($scope, employeeFactory, $uibModalInstance, A) {

        $scope.isLoading = true;

        employeeFactory.getPaycheck(A.Id).then(function (res) {
            $scope.paycheckView = res.data;
            $scope.isLoading = false;
        })
            .catch(function (err) {
                toastr.error("Could not connect to the database.");
                $scope.isLoading = false;
            })

        $scope.cancel = function () {
            $uibModalInstance.dismiss('cancel');
        }
    };

    PaycheckCtrl.$inject = ['$scope', 'employeeFactory', '$uibModalInstance', 'A'];
    angular.module('app').controller('PaycheckCtrl', PaycheckCtrl);
}());
