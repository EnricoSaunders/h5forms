angular.module('h5Forms.formBuilder', [
    'h5Forms.formBuilder.ctrl.formList',
    'h5Forms.formBuilder.ctrl.formBuilder',     
    'h5Forms.service.form',
    'h5Forms.directive.formView',
    'h5Forms.directive.tabs',
    'ngRoute'
]).config([
    '$routeProvider',
    '$locationProvider',
    function ($routeProvider, $locationProvider ) {
        $routeProvider.when('/', {
            templateUrl: 'Forms/list',
            controller: 'formListCtrl'
        });
        
        $routeProvider.when('/detail/:id', {
            templateUrl: 'Forms/edit',
            controller: 'formBuilderCtrl'
        });
        
        $routeProvider.when('/create', {
            templateUrl: 'Forms/edit',
            controller: 'formBuilderCtrl'
        });
        
        $routeProvider.otherwise({
            redirectTo: '/'
        });
    }
]);
      