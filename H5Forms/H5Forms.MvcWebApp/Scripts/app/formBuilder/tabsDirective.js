angular.module('h5Forms.directive.tabs', [])
       .directive('tabs', [function() {
           return{
               restrict: 'E',
               transclude: true,
               templateUrl: 'Shared/Templates/tabs.cshtml',
               controller: [
                   '$scope',
                   function ($scope) {
                       $scope.panes = [];

                       this.addPane = function (paneScope) {
                           $scope.panes.unshif(paneScope);

                           var that = this;
                           paneScope.$on('$destroy', function(event) {
                               that.removePane(paneScope);
                           });
                       };

                       this.removePane = function(paneScope) {
                           var index = $scope.panes.indexOf(paneScope);
                           
                           if (index !== -1) {
                               $scope.panes.splice(index, 1);
                           }                           
                       };
                   }
               ]
           };
       }])
       .directive('pane', [function() {
           return {
               restrict: 'E',
               require: '^tabs',
               transclude: true,
               templateUrl: 'Shared/Templates/pane.cshtml',
               link: function(scope, element, attrs, tabsCtrl) {
                   tabsCtrl.addPane(scope);
               }               
           };
       }]);