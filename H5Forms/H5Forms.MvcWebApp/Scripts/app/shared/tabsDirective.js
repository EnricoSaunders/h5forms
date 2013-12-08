angular.module('h5Forms.directive.tabs', [])
       .directive('tabs', [function() {
           return{
               restrict: 'E',
               transclude: true,
               templateUrl: 'Shared/Templates/tabs.cshtml',
               scope: {},
               controller: [
                   '$scope',
                   function ($scope) {
                       $scope.panes = [];

                       this.addPane = function (paneScope) {
                           if (!paneScope.selected && !$scope.panes.length) {
                               $scope.setActivePane(paneScope);
                           } else {
                               paneScope.selected = false;
                           }
                           
                           $scope.panes.unshift(paneScope);

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

                       $scope.setActivePane = function (pane) {
                           $scope.panes.forEach(function(apane) {
                               apane.selected = false;
                           });

                           pane.selected = true;
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
               scope: {
                   title: '@',
                   selected: '@'
               },
               link: function(scope, element, attrs, tabsCtrl) {
                   tabsCtrl.addPane(scope);
               }               
           };
       }]);