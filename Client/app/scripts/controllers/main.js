'use strict';
(function () {
    var MainCtrl = function (toastr, employeeFactory, $uibModal) {
        var vm = this;

        vm.relationships = [];
        vm.relMessage = finishedRelMessage();

        var dependentIndex = 0;

        function init() {
            getEmployees();

            fillRelationshipList();

            initEmployee();
        }

        init();

        function getEmployees() {
            vm.isLoading = true;

            employeeFactory.getEmployees().then(function (res) {
                vm.employees = res.data;
                vm.isLoading = false;
            })
                .catch(function (err) {
                    toastr.error("Could not connect to the database.");
                    vm.isLoading = false;
                });
        }

        function fillRelationshipList()
        {
            //get relationships to populate drop down box
            vm.relMessage = "Loading...";

            employeeFactory.getRelationships().then(function (res) {
                vm.relationships = res.data;
                vm.relMessage = finishedRelMessage();
            })
                .catch(function (err) {
                    toastr.error("Could not connect to the database.");
                    vm.relMessage = finishedRelMessage();
                });
        }

        function initEmployee()
        {
            vm.employee = {};
            vm.dependent = {};
            vm.employee.dependents = [];
            dependentIndex = 0;
        }

        vm.saveEmployee = function ()
        {
            if (!allowAddEmployee()) {
                toastr.error('Data missing from the form. Please check your entry!');
            }
            else {
                //if user had not clicked plus button, add current form values to the array of dependents
                vm.addDependent();

                //post to db
                employeeFactory.saveEmployee(vm.employee).then(function (res) {
                    //reloads list of employees
                    console.log(res.data);
                    vm.employees = res.data;

                })
                    .catch(function (err) {
                        toastr.error("Could not save the data.");
                    });

                initEmployee();
            }
        }

        vm.clearForm = function () {
            initEmployee();
        }

        vm.addDependent = function () {
            if (!allowAddDependent())
            {
                toastr.error('Please provide Dependent First Name and Relationship')
            }
            else {
                vm.dependent.DependentId = ++dependentIndex;
                vm.employee.dependents.push(vm.dependent);
                vm.dependent = {};
            }
        }

        vm.showPaycheck = function(id)
        {
            var mInst = $uibModal.open({
                animation: true,
                templateUrl: '../app/views/paycheckModal.html',
                controller: 'PaycheckCtrl',
                size: 'md',
                resolve: { 
                    A: function () {
                        return { Id: id };
                    }
                }
            });
        }

        function allowAddEmployee()
        {
            return vm.employee && vm.employee.FirstName;
        }

        function allowAddDependent()
        {
            return vm.dependent && vm.dependent.FirstName && vm.dependent.Relationship_Id;
        }

        function finishedRelMessage()
        {
            return "Select Relationship";
        }
    };

    MainCtrl.$inject = ['toastr', 'employeeFactory','$uibModal'];
    angular.module('app').controller('MainCtrl', MainCtrl);
}());
