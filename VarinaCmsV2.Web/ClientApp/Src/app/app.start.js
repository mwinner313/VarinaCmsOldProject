; (function () {


    angular.module('app').run(initialize)

    initialize.$inject = ['fileDataSrv', '$mdMedia', '$sessionStorage', 'identityManager', '$rootScope', '$mdSidenav', '$transitions', '$location', '$http', 'api', 'langDataSrv', 'currentLang', '$state', '$mdDialog','$stateParams'];
    function initialize(fileDataSrv, $mdMedia, $sessionStorage, identityManager, $rootScope, $mdSidenav, $transitions, $location, $http, api, langDataSrv, currentLang, $state, $mdDialog,$stateParams) {
        $rootScope.languages = $sessionStorage.languages;
  
        $rootScope.changeCurrentLang = function (lang) {
            currentLang.set(lang.name);
            $rootScope.currentLang = lang.name;
            var params=angular.copy($stateParams);
            angular.extend(params,{
                lang:lang.name
            })
            $state.go($state.current.name,params);
            $rootScope.$broadcast('languageChanged');
        }
        $rootScope.$mdSidenav = $mdSidenav;

        $rootScope.scrollConfig = {
            autoHideScrollbar: true,
            theme: 'light',
            advanced: {
                updateOnContentResize: true
            },
            mouseWheel:{ scrollAmount: 260 },
            scrollButtons:{ enable: false },
            scrollInertia:800,
            setHeight: window.innerHeight - 60,
            autoDraggerLength: false,
            axis: 'y'
        }
        $transitions.onStart({}, function ($transition$) {
            var from = $transition$.$from() || {};
            var to = $transition$.$to() || {};
            if (from.name === to.name) $rootScope.locationSearch = $location.search();
            
            currentLang.set($transition$.params().lang);
            $rootScope.currentLang = currentLang.get();
            
        });

        $transitions.onSuccess({}, function ($transition$) {
            var from = $transition$.$from() || {};
            var to = $transition$.$to() || {};
            if (from.name === to.name && $rootScope.locationSearch !== undefined)
                $location.search($rootScope.locationSearch);

        });
          //  $mdDialog.show($mdDialog.fileManager())
          Chart.defaults.global.defaultFontFamily= "'IRANSansUlt'";
    }
}())