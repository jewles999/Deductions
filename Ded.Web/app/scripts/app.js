'use strict';

(function () {
var app = angular.module('app', [
      'ui.router', 'ui.bootstrap',
    ]);

  let serverUrl = 'http://localhost:59906/';
  app.constant('defaultSettings', {
      server: serverUrl
  });

})();
