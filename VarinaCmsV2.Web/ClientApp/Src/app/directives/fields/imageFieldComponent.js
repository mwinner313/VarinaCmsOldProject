(function() {
  "use strict";

  // Usage:
  //
  // Creates:
  //

  angular.module("app").component("imageField", {
    templateUrl: "/Src/app/directives/fields/imageField-tpl.html",
    controller: Controller,
    controllerAs: "vm",
    bindings: {
      model: "=",
      fd: "=",
      disableRequiredValidation: "<"
    }
  });

  Controller.$inject = ["$element", "$timeout", "ngDialog", "$mdDialog",'lodash'];
  function Controller($element, $timeout, ngDialog, $mdDialog,lodash) {
    var vm = this;
    vm.ngDialog = ngDialog;
    vm.showImagePicker = showImagePicker;
    vm.isFieldRequired = isFieldRequired;
    vm.form =
      $element.parent().controller("form") ||
      $element
        .parent()
        .parent()
        .controller("form");

    vm.$onInit = function() {
      $timeout(function() {
        vm.field = vm.form[vm.fd.handle];
      });
    };

    ////////////////
    function isFieldRequired() {
      if (vm.disableRequiredValidation) return false;
      return vm.fd.isRequired;
    }

    function showImagePicker() {
      $mdDialog.show($mdDialog.imagePicker()).then(function(image) {
        vm.model.rawValue = { path: image.path, name: image.name };
        if(lodash.has(image,'metaData')){
          vm.model.rawValue.resizeds={items:lodash.filter(image.metaData,function(m){
            return m.metaName=='resized-image';
          }).map(function(m){return m.metaData})}
        }
      });
    }
  }
})();
