angular.module('h5Forms.formsAdmin.ctrl.formEdit', [])
       .controller('formEditCtrl', [
           '$scope',
           '$routeParams',
           'formsAdminService',
           'navigationService',
           function ($scope, $routeParams, formsAdminService, navigationService) {
               $scope.controlTypes = [];
               $scope.layoutTypes = [];
               this.formInit = { title: 'Form title', enabled: true, controls: [] };
               $scope.form = null;
               $scope.currentControl = null;
               $scope.controlPropertiesTemplate = null;

               $scope.list = function() {
                   navigationService.goToList();
               };
               
               $scope.saveForm = function () {
                   if ($scope.form.id) {
                       formsAdminService.updateForm($scope.form).then(function (response) {
                           navigationService.goToList();
                       }, function () { throw 'Error on saveForm'; });
                   } else {
                       formsAdminService.createForm($scope.form).then(function (response) {
                           navigationService.goToList();
                       }, function () { throw 'Error on saveForm'; });
                   }                  
               };
                                                           
               //#region Controls
               
               $scope.addControl = function (controlType) {
                   formsAdminService.createControl(controlType).then(function (response) {
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
                             
               $scope.setCurrentControl = function(control) {
                   $scope.currentControl = control;                 
                   $scope.controlPropertiesTemplate = 'FormsAdmin/Properties/' + control.controlType + 'Properties.cshtml';
               };
               
               $scope.toggleCheck = function (id) {
                   if ($scope.currentControl.selectedValues.indexOf(id) === -1) {
                       $scope.currentControl.selectedValues.push(id);
                   } else {
                       $scope.currentControl.selectedValues.splice($scope.currentControl.selectedValues.indexOf(id), 1);
                   }
               };
               
               //#endregion
               
               //#region Init
                              
                formsAdminService.getTypes().then(function (response) {
                    $scope.controlTypes = response.data.controlTypes;
                    $scope.layoutTypes = response.data.layoutTypes;
                }, function() { throw 'Error on getTypes'; });
                             
               if (angular.isUndefined($routeParams.id)) {
                   $scope.form = this.formInit;
               } else {
                   formsAdminService.getForm($routeParams.id).then(function (response) {
                       $scope.form = response.data.data;
                   }, function () { throw 'Error on getForm'; });
               }               

               //#endregion
           }]);