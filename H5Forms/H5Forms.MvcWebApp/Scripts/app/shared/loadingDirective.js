angular.module('h5Forms.directive.loading', [])
    .directive('loading', ['$http', function($http) {
        return {
            restrict: 'E',
            replace: true,
            template: '<div class="loading" style="display: none;"></div>',
            link: function (scope, element, attrs) {
                var container = $("body")[0];
                
                scope.isLoading = function() {
                    return $http.pendingRequests.length > 0;
                };

                scope.$watch(scope.isLoading, function(v) {
                    if (v) {
                        scope.blockUI();                        
                    } else {
                        scope.unblockUI();
                    }
                });

                scope.blockUI = function() {                 
                    $(container).block(
                                {
                                    message: element,
                                    css: {
                                        width: 100,
                                        height: 80,
                                        border: 0,
                                        backgroundColor: '',
                                        zIndex: 4000
                                    },
                                    overlayCSS: { opacity: 0.0 }
                                });
                };

                scope.unblockUI = function () {
                    setTimeout(function () { $(container).unblock(); }, 300);
                };
            }
        };
    }]);
      