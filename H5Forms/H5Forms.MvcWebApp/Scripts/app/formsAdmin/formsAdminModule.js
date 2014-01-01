angular.module('h5Forms.formsAdmin', [
    'h5Forms.formsAdmin.ctrl.formsList',
    'h5Forms.formsAdmin.ctrl.formEdit',
    'h5Forms.formsAdmin.ctrl.formReport',
    'h5Forms.service.forms',
    'h5Forms.service.navigation',
    'h5Forms.directive.formView',
    'h5Forms.directive.tabs',
    'h5Forms.directive.loading',     
    'ngRoute',
    'ngGrid',
    'ui.sortable'
]).config([
    '$routeProvider',
    '$locationProvider',
    function ($routeProvider, $locationProvider ) {
        $routeProvider.when('/', {
            templateUrl: 'FormsAdmin/list',
            controller: 'formsListCtrl'
        });
              
        $routeProvider.when('/create', {
            templateUrl: 'FormsAdmin/edit',
            controller: 'formEditCtrl'
        });
        
        $routeProvider.when('/edit/:id', {
            templateUrl: 'FormsAdmin/edit',
            controller: 'formEditCtrl'
        });
        
        $routeProvider.when('/report/:id', {
            templateUrl: 'FormsAdmin/report',
            controller: 'formReportCtrl'
        });
        
        $routeProvider.otherwise({
            redirectTo: '/'
        });
    }
]);
      