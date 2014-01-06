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

                   $scope.setForm();

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

               $scope.setForm = function() {
                   var formattedNumbers = [];                  

                   $scope.form.controls.forEach(function(control) {
                       if (control.controlType == "FormattedNumber") formattedNumbers.push(control);                       
                   });

                   for (var j = 0; j < formattedNumbers.length; j++) {
                       var regEx = "^";
                       var emptyRegex = "";
                       var control = formattedNumbers[j];

                       for (var i = 0; i < control.parts.length; i++) {
                           var part = control.parts[i];
                           regEx = regEx + "\\d{" + part.length + "}";                          

                           if (control.parts.indexOf(part) < control.parts.length - 1) {
                               regEx = regEx + control.separator;
                               emptyRegex = emptyRegex + control.separator;
                           }
                       }

                       regEx = regEx + "$";
                       regEx = regEx + "|^$";
                       regEx = regEx + "|^" + emptyRegex + "$";
                       

                       control.validationRules.forEach(function (validator) {
                           if (validator.validationType == "FormattedNumber") {
                               validator.regEx = regEx;
                           }
                       });
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
               
               $scope.addPart = function (index) {                  
                   $scope.currentControl.parts.splice(index, 0, { length: 1, value: '' });
               };

               $scope.deletePart = function (part) {
                   if ($scope.currentControl.parts.length == 1) return;

                   var index = $scope.currentControl.parts.indexOf(part);
                   $scope.currentControl.parts.splice(index, 1);

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