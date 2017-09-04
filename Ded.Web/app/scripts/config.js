function configState($stateProvider, $urlRouterProvider) {

    // Set default state
    $urlRouterProvider.otherwise("/");
    $stateProvider
        .state('main',{
            url:"/",
            templateUrl: "../app/views/main.html",
            controller: 'MainCtrl'
        })
        .state('404',{
            url:"/404",
            templateUrl: "../app/views/404.html"
        })
}

angular
    .module('app')
    .config(['$stateProvider', '$urlRouterProvider', configState])
    .run(['$rootScope', '$state', '$window','$location',function($rootScope, $state, $window, $location) {
        $rootScope.$state = $state;

        $rootScope.$on('$locationChangeStart', function () {
            var interval = setInterval(function(){

            if (document.readyState == 'complete') {
                $window.scrollTo(0, 0);
                clearInterval(interval);
            }
        }, 200);
        });
    }]);
