angular.module('h5Forms.formBuilder.ctrl.formBuilder', [])
       .controller('formBuilderCtrl', [
           '$scope',
           'formService',
           function ($scope, formService) {
               $scope.controlTypes = [];
               $scope.form = { title: 'Form title', controls: [] };
               $scope.currentControl = null;
               $scope.controlPropertiesTemplate = null;

               formService.getControlTypes().then(function(response) {
                   $scope.controlTypes = response.data;
               }, function () { throw 'Error on get controlTypes'; });
               
               $scope.addControl = function (controlType) {
                   formService.addControl(controlType).then(function (response) {
                       $scope.form.controls.push(response.data.data);
                   }, function () { throw 'Error on addControl'; });

               };
               
               $scope.deleteControl = function (control) {
                   formService.deleteControl(control.id).then(function (response) {
                       var index = $scope.form.controls.indexOf(control);
                       $scope.form.controls.splice(index, 1);
                       $scope.currentControl = null;
                       $scope.controlPropertiesTemplate = null;
                   }, function () { throw 'Error on deleteControl'; });

               };
               
               $scope.setcurrentControl = function(control) {
                   $scope.currentControl = control;
                   $scope.controlPropertiesTemplate = 'Forms/ControlProperties/' + control.controlType + 'Properties.cshtml';
               };
           }]);