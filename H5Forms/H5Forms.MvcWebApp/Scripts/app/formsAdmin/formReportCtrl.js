angular.module('h5Forms.formsAdmin.ctrl.formReport', [])
       .controller('formReportCtrl', [
           '$scope',
           '$routeParams',
           'formsService',
           'navigationService',
           function ($scope, $routeParams, formsService, navigationService) {
               $scope.entries = [];
               $scope.columnDefs = [];
                                            
               $scope.list = function () {
                   navigationService.goToList();
               };
                                            
               //#region Grid
               
               $scope.gridOptions = {                  
                   data: 'entries',
                   columnDefs: 'columnDefs',
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
               
               formsService.getFormEntries($routeParams.id).then(function (response) {
                   var gridColumns = [];
                   
                   for (var prop in response.data.data.columns)
                    {
                       if (response.data.data.columns.hasOwnProperty(prop)) {
                           var column = { field: prop, displayName: response.data.data.columns[prop] };
                           
                           gridColumns.push(column);
                        }
                    }

                   $scope.columnDefs = gridColumns;
                   $scope.entries = response.data.data.entries;
                   
               }, function () { throw 'Error on getFormEntries'; });
               
               //#endregion
              
           }]);