angular.module('h5Forms.formsAdmin.ctrl.formsList', [])
       .controller('formsListCtrl', [
           '$scope',           
           'formsService',
           'navigationService',
           function ($scope,  formsService, navigationService) {
               $scope.forms = [];
               $scope.url = location.origin + '/FormsTest/test/';
               $scope.urlLink;
               $scope.aLink;
               $scope.iframe;
                             
               
               $scope.editForm = function (id) {                   
                   navigationService.goToEdit(id);
               };
               
               $scope.createForm = function () {
                   navigationService.goToCreate();
               };
               
               $scope.report = function (id) {
                   navigationService.goToReport(id);
               };

               $scope.setCurrentLinks = function (form) {
                   $scope.urlLink = $scope.url + form.hash;

                   $scope.aLink = '<a  target="_blank" href="' + $scope.urlLink + '" >Complete this form</a>';

                   $scope.iframe = '<iframe    allowTransparency="true"  style="width: 100%; height: 600px; overflow: scroll;"  src="' + $scope.urlLink + '"></iframe>';
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