angular.module("h5Forms.directive.formView", [])                
       .directive("formView", [function() {
           return {
               restrict: 'E',
               transclude: true,               
               templateUrl: 'Forms/Controls/formView.cshtml',
               scope: {
                   title: '@'
               },
               controller: [
                   '$scope',                  
                   function ($scope) {
                       $scope.controlScopes = [];
                       
                        this.addControlScope = function (controlScope) {
                            $scope.controlScopes.unshift(controlScope);
                               
                            var that = this;                               
                            controlScope.$on('$destroy', function (event) {
                                that.removeControlScope(controlScope);
                            });
                        };
                       
                        this.removeControlScope = function(controlScope) {
                            var index = $scope.controlScopes.indexOf(controlScope);
                            if ( index !== -1 ) {
                                $scope.controlScopes.splice($scope.controlScopes.indexOf(controlScope), 1);
                            }
                        };

                   }
               ],
               compile: function(element, attributes) {
                   
               },
           };
       }])
       .directive("control", [function () {
            return {
                restrict: 'E',
                replace: true,
                require: '^formView',
                templateUrl: 'Forms/Controls/control.cshtml',
                link: function (scope, element, attrs, formViewCtrl) {
                    formViewCtrl.addControlScope(scope);
                },
            };
        }]);