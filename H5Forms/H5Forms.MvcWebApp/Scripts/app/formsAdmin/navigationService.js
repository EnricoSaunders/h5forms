angular.module('h5Forms.service.navigation', [])
       .factory('navigationService', [
          '$location',
           function ($location) {
               var sections = {
                   list: '/',
                   create: '/create',
                   edit: '/edit/',
                   report: '/report/'
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
                   },
                   goToReport: function (id) {
                       $location.path(sections.report + id);
                   }
               };
           }]);