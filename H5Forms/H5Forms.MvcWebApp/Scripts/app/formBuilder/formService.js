angular.module('h5Forms.service.form', [])
       .factory('formService', [
           '$http',           
           function ($http) {
               return {                                      
                   getControlTypes: function () {
                       return $http({
                           method: 'GET',
                           url: '/Forms/GetControlTypes'
                       });                       
                   },
                   addControl: function(controlType) {
                       return $http({
                           method: 'POST',
                           url: '/Forms/AddControl',
                           data: { controlType: controlType }
                       });
                   },
                   deleteControl: function (controlId) {
                       return $http({
                           method: 'POST',
                           url: '/Forms/DeleteControl',
                           data: { controlId: controlId }
                       });
                   }
               };                                                        
       }]);