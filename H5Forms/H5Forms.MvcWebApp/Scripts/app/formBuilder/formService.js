angular.module('h5Forms.service.form', [])
       .factory('formService', [
           '$http',           
           function ($http) {
               return {                                      
                   getTypes: function () {
                       return $http({
                           method: 'GET',
                           url: '/Forms/GetTypes'
                       });                       
                   },
                   createControl: function (controlType) {
                       return $http({
                           method: 'POST',
                           url: '/Forms/CreateControl',
                           data: { controlType: controlType }
                       });
                   },
                   getForms: function () {
                       return $http({
                           method: 'GET',
                           url: '/Forms/GetForms'
                       });
                   },
                   getForm: function (id) {
                       return $http({
                           method: 'POST',
                           url: '/Forms/GetForm',
                           data: { formId: id }
                       });
                   }
               };                                                        
       }]);