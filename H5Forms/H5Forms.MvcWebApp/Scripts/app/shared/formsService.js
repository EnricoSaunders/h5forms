angular.module('h5Forms.service.forms', [])
       .factory('formsService', [
           '$http',           
           function ($http) {
               return {                                      
                   getTypes: function () {
                       return $http({
                           method: 'GET',
                           url: '/FormsAdmin/GetTypes'
                       });                       
                   },
                   createControl: function (controlType) {
                       return $http({
                           method: 'POST',
                           url: '/FormsAdmin/CreateControl',
                           data: { controlType: controlType }
                       });
                   },
                   getForms: function () {
                       return $http({
                           method: 'GET',
                           url: '/FormsAdmin/GetForms'
                       });
                   },
                   getForm: function (id) {
                       return $http({
                           method: 'POST',
                           url: '/FormsAdmin/GetForm',
                           data: { formId: id }
                       });
                   },
                   createForm: function(form) {
                       return $http({
                           method: 'POST',
                           url: '/FormsAdmin/CreateForm',
                           data: { form: form }
                       });
                   },
                   updateForm: function (form) {
                       return $http({
                           method: 'POST',
                           url: '/FormsAdmin/UpdateForm',
                           data: { form: form }
                       });
                   },
                   getFormEntries: function (id) {
                       return $http({
                           method: 'POST',
                           url: '/FormsAdmin/GetFormEntries',
                           data: { formId: id }
                       });
                   },
               };                                                        
       }]);