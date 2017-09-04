'use strict';
(function () {
    var PaycheckCtrl = function ($scope, employeeFactory, $uibModalInstance, A) {
        var vm = this;
        employeeFactory.getPaycheck(A.Id).then(function (res) {
            $scope.paycheckView = res.data;
        })
            .catch(function (err) {
                toastr.error(err);
            })

        $scope.cancel = function () {
            $uibModalInstance.dismiss('cancel');
        }
    };

    PaycheckCtrl.$inject = ['$scope', 'employeeFactory', '$uibModalInstance', 'A'];
    angular.module('app').controller('PaycheckCtrl', PaycheckCtrl);
}());
