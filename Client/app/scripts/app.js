'use strict';

/**
 * @ngdoc overview
 * @name dedwebApp
 * @description
 * # dedwebApp
 *
 * Main module of the application.
 */
(function () {
var app = angular.module('app', [
      'ui.router', 'ui.bootstrap','toastr'
    ]);

  let serverUrl = 'http://localhost:59906/';
  app.constant('defaultSettings', {
      serverURL: serverUrl
  });

})();
