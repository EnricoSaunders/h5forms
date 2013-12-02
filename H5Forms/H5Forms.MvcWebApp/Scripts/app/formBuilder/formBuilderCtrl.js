angular.module('h5Forms.formBuilder.ctrl.formBuilder', [])
       .controller('formBuilderCtrl', [
           '$scope',
           'formService',
           function ($scope, formService) {
               $scope.controlTypes = [];
               $scope.layoutTypes = [];
               $scope.form = { title: 'Form title', controls: [] };
               $scope.currentControl = null;
               $scope.controlPropertiesTemplate = null;           

               formService.getTypes().then(function(response) {
                   $scope.controlTypes = response.data.controlTypes;
                   $scope.layoutTypes = response.data.layoutTypes;
               }, function () { throw 'Error on getTypes'; });
               
               $scope.addControl = function (controlType) {
                   formService.createControl(controlType).then(function (response) {
                       var control = response.data.data;                       
                       var lastId = 1;

                       $scope.form.controls.forEach(function (ctrl) {
                           if (ctrl.id > lastId) {
                               lastId = ctrl.id;
                           }
                       });

                       control.id = lastId + 1;

                       $scope.form.controls.push(control);
                   }, function () { throw 'Error on createControl'; });

               };
               
               $scope.deleteControl = function (control) {                   
                   var index = $scope.form.controls.indexOf(control);
                   
                   $scope.form.controls.splice(index, 1);
                   $scope.currentControl = null;
                   $scope.controlPropertiesTemplate = null;                   
               };
               
               $scope.addOption = function (index) {
                   var lastId = 1;

                   $scope.currentControl.options.forEach(function(option) {
                       if (option.id > lastId) {
                           lastId = option.id;
                       }
                   });

                   $scope.currentControl.options.splice(index, 0, { id: lastId + 1, value: '' });
               };
               
               $scope.deleteOption = function (option) {
                   if ($scope.currentControl.options.length == 1) return;
                   
                   var index = $scope.currentControl.options.indexOf(option);
                   $scope.currentControl.options.splice(index, 1);
                   
               };                             
                             
               $scope.setcurrentControl = function(control) {
                   $scope.currentControl = control;                 
                   $scope.controlPropertiesTemplate = 'Forms/ControlProperties/' + control.controlType + 'Properties.cshtml';
               };
           }]);