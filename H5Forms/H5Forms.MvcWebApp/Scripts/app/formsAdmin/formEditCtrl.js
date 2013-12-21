angular.module('h5Forms.formsAdmin.ctrl.formEdit', [])
       .controller('formEditCtrl', [
           '$scope',
           '$routeParams',
           'formsService',
           'navigationService',
           function ($scope, $routeParams, formsService, navigationService) {
               $scope.controlTypes = [];
               $scope.optionLayoutTypes = [];
               $scope.labelLayoutTypes = [];
               this.formInit = { title: 'Form title', enabled: true, controls: [], labelLayoutType: null };
               $scope.lastControlId = 0;
               $scope.form = null;
               $scope.currentControl = null;
               $scope.controlPropertiesTemplate = null;
               $scope.result = {hasErrors: false, messages: []};
               

               $scope.list = function() {
                   navigationService.goToList();
               };
               
               $scope.saveForm = function () {
                   $scope.result.hasErrors = false;

                   if ($scope.form.id) {
                       formsService.updateForm($scope.form).then(function (response) {
                           
                           if (!response.data.result.hasErrors) 
                               navigationService.goToList();
                           
                           $scope.result.hasErrors = true;
                           $scope.result.messages = response.data.result.messages;
                           

                       }, function () { throw 'Error on saveForm'; });
                   } else {
                       formsService.createForm($scope.form).then(function (response) {
                           
                           if (!response.data.result.hasErrors)
                               navigationService.goToList();
                           
                           $scope.result.hasErrors = true;
                           $scope.result.messages = response.data.result.messages;
                           
                       }, function () { throw 'Error on saveForm'; });
                   }                  
               };
                                                           
               //#region Controls
               
               $scope.addControl = function (controlType) {
                   formsService.createControl(controlType).then(function (response) {
                       var control = response.data.data;
                       
                       $scope.form.controls.forEach(function (ctrl) {
                           if (ctrl.id > $scope.lastControlId) {
                               $scope.lastControlId = ctrl.id;
                           }
                       });

                       control.id = $scope.lastControlId + 1;

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

                   $scope.currentControl.value = $scope.currentControl.selectedValues.join('|');
               };
               
               //#endregion
               
               //#region Init
                              
               formsService.getTypes().then(function (response) {
                    $scope.controlTypes = response.data.controlTypes;
                    $scope.optionLayoutTypes = response.data.optionLayoutTypes;
                    $scope.labelLayoutTypes = response.data.labelLayoutTypes;
                }, function() { throw 'Error on getTypes'; });
                             
               if (angular.isUndefined($routeParams.id)) {
                   $scope.form = this.formInit;
               } else {
                   formsService.getForm($routeParams.id).then(function (response) {
                       $scope.form = response.data.data;
                       
                       $scope.form.controls.forEach(function (ctrl) {
                           if (ctrl.id > $scope.lastControlId) {
                               $scope.lastControlId = ctrl.id;
                           }
                       });
                       
                   }, function () { throw 'Error on getForm'; });
               }               

               //#endregion
           }]);