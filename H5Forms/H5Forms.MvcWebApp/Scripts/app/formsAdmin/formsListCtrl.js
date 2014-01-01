angular.module('h5Forms.formsAdmin.ctrl.formsList', [])
       .controller('formsListCtrl', [
           '$scope',           
           'formsService',
           'navigationService',
           function ($scope,  formsService, navigationService) {
               $scope.forms = [];
               $scope.testBaseUrl = 'http://localhost:50060/FormsTest/test/';
                             
               
               $scope.editForm = function (id) {                   
                   navigationService.goToEdit(id);
               };
               
               $scope.createForm = function () {
                   navigationService.goToCreate();
               };
               
               $scope.report = function (id) {
                   navigationService.goToReport(id);
               };
               
               //#region Grid
               
               $scope.gridOptions = {                  
                   data: 'forms',
                   columnDefs: [{ field: 'id', displayName: 'Id' },
                                { field: 'title', displayName: 'Title' },
                                { field: 'createDate', displayName: 'Create date', cellFilter: 'date:\'dd/MM/yyyy\'' },
                                { field: 'updateDate', displayName: 'Update Date', cellFilter: 'date:\'dd/MM/yyyy\'' },
                                { field: 'enabled', displayName: 'Enabled' },
                                { displayName: 'Actions', cellTemplate: $("#gridActions").html() }
                   ],
                   showFooter: true,
                   enablePaging: true,
                   pagingOptions: {
                       pageSizes: [5, 10],
                       pageSize: 10,                       
                       currentPage: 1
                   },
               };
               
               //#endregion
               
               //#region Init
               
               formsService.getForms().then(function (response) {
                   $scope.forms = response.data.data;                   
               }, function () { throw 'Error on getForms'; });
               
               //#endregion
              
           }]);