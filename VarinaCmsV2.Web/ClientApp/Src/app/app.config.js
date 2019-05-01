; (function () {
  'use strict';

  angular.module('app').config(config);
  config.$inject = ["$httpProvider",'$mdAriaProvider', 'ssSideNavSectionsProvider', '$mdThemingProvider', '$mdDialogProvider'];
  function config($httpProvider,$mdAriaProvider, ssSideNavSectionsProvider, $mdThemingProvider, $mdDialogProvider) {
    $mdAriaProvider.disableWarnings();
    $httpProvider.defaults.headers.common["Content-Type"] = "application/json";
    $httpProvider.defaults.headers.common["X-Panel-Request"] = "true";
    $httpProvider.defaults.headers.common = { 'X-Requested-With': 'XMLHttpRequest' };
    $httpProvider.interceptors.push('unAuthorizeInterceptor');
    $httpProvider.interceptors.push('requestLanguageInterceptor');

    ssSideNavSectionsProvider.initWithSections([]);
  //  ssSideNavSectionsProvider.initWithTheme($mdThemingProvider);

    $mdDialogProvider.addPreset('fileManager', {
      options: function () {
        return {
          templateUrl: '/Src/app/fileManager/filemanager.tpl.html',
          controller: 'FileManagerCtrl',
          controllerAs: 'vm',
          bindToController: true,
          clickOutsideToClose: true,
          escapeToClose: true,
          fullscreen: true,
          multiple: true
        };
      }
    });
    $mdDialogProvider.addPreset('filePicker', {
      options: function () {
        return {
          templateUrl: '/Src/app/fileManager/filemanager.tpl.html',
          controller: 'FileManagerCtrl',
          controllerAs: 'vm',
          bindToController: true,
          clickOutsideToClose: true,
          escapeToClose: true,
          fullscreen: true,
          multiple: true,
          locals: {
            openedByOtherComponent: true
          }
        };
      }
    });
    $mdDialogProvider.addPreset('imagePicker', {
      options: function () {
        return {
          templateUrl: '/Src/app/fileManager/filemanager.tpl.html',
          controller: 'FileManagerCtrl',
          controllerAs: 'vm',
          bindToController: true,
          clickOutsideToClose: true,
          escapeToClose: true,
          fullscreen: true,
          multiple: true,
          locals: {
            openedByOtherComponent: true,
            extension: '.png,.jpg,.jpeg'
          }
        };
      }
    });
    $mdDialogProvider.addPreset('multiImagePicker', {
      options: function () {
        return {
          templateUrl: '/Src/app/fileManager/filemanager.tpl.html',
          controller: 'FileManagerCtrl',
          controllerAs: 'vm',
          bindToController: true,
          clickOutsideToClose: true,
          escapeToClose: true,
          fullscreen: true,
          multiple: true,
          locals: {
            openedByOtherComponent: true,
            canAcceptMultiFile:true,
            extension: '.png,.jpg,.jpeg'
          }
        };
      }
    });
    $mdDialogProvider.addPreset('fdEdit', {
      options: function () {
        return {
          templateUrl: '/Src/app/fieldDefenition/fields.edit.tpl.html',
          controller: 'fdUpdateCtrl',
          controllerAs: 'vm',
          clickOutsideToClose: true,
          bindToController: true,
          multiple: true

        };
      }
    });
    $mdDialogProvider.addPreset('search', {
      options: function () {
        return {
          parent: document.getElementsByTagName('body')[0],
  
          templateUrl: '/Src/app.core/directives/SearchDialog/tpl.html',
          controller: 'searchDialogCtrl',
          controllerAs: 'vm',
          bindToController: true,
          clickOutsideToClose: true,
        };
      }
    });

  }
})();