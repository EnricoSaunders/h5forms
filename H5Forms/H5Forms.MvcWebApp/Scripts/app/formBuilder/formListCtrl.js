angular.module('h5Forms.formBuilder.ctrl.formList', [])
       .controller('formListCtrl', [
           '$scope',
           '$location',
           'formService',           
           function ($scope, $location, formService) {
               $scope.forms = [];                           
               
               $scope.editForm = function (id) {
                   $location.path("/edit/" + id);
               };
               
               $scope.createForm = function () {
                   $location.path("/create");
               };
               
               //#region Init
               
               formService.getForms().then(function (response) {
                   $scope.forms = response.data.forms;
               }, function () { throw 'Error on getForms'; });
               
               //#endregion
              
           }]);