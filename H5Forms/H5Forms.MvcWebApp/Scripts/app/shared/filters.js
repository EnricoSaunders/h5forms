angular.module('h5Forms.filters', []).
  filter('htmlToPlaintext', function () {
      return function (text) {
          return $(text).html(value).text();
      }
  }
);