angular.module('h5Forms.service.navigation', [])
       .factory('navigationService', [
          '$location',
           function ($location) {
               var sections = {
                   list: '/',
                   create: '/create',
                   edit: '/edit/'
               };
               return {
                   goToList: function () {
                       $location.path(sections.list);
                   },
                   goToCreate: function () {
                       $location.path(sections.create);
                   },
                   goToEdit: function (id) {
                       $location.path(sections.edit + id);
                   }
               };
           }]);