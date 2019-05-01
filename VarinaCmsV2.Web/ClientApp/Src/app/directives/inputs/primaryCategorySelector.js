(function () {
  "use strict";

  angular
    .module("app")
    .directive("primaryCategorySelector", primaryCategorySelector);

  primaryCategorySelector.$inject = [];

  function primaryCategorySelector() {

    var directive = {
      bindToController: true,
      controller: PrimaryCategorySelectorController,
      controllerAs: "vm",
      link: link,
      templateUrl: "/Src/app/directives/inputs/primaryCategorySelector.tpl.html",
      restrict: "E",
      scope: {
        model: "=",
        name: "@",
        useProductCategory:"@",
        scheme: "=?",
        inputPlaceholder: "@",
        form: "=",
        onSelect: "&",
        beforeSelect: "&"
      }
    };
    return directive;

    function link(scope, element, attrs) {}
  }
  /* @ngInject */
  PrimaryCategorySelectorController.$inject = [
    "categoryDataService",
    "productCategorySrv",
    "currentLang",
    "categoryType",
    "$timeout",
    "$q"
  ];

  function PrimaryCategorySelectorController(
    categoryDataService,
    productCategorySrv,
    currentLang,
    categoryType,
    $timeout,
    $q
  ) {
    var vm = this;
    vm.scheme = vm.scheme || {};
    vm.selectedCat;
    vm.getItems = getItems;
    vm.setCategory = setCategory;
    vm.removePcatIfTextIsEmpty = removePcatIfTextIsEmpty;
    $timeout(function () {
      if (vm.model.primaryCategoryId) {
        vm.selectedCat = vm.model.primaryCategory;
      }
      vm.firstChoice = vm.selectedCat;
    });

    function getItems(keyword) {
      var defered = $q.defer();
      getCategoriesByOptions().then(function (res) {
        defered.resolve(res.data);
      });
      return defered.promise;
    }

    function getCategoriesByOptions() {
      var dataService =vm.useProductCategory==="true"?productCategorySrv:categoryDataService;
      return dataService.get({
        languageName: currentLang.get(),
        checkByType: true,
        categoryType: categoryType.main,
        mapForTreeView: false,
        searchText: vm.categorySearchText,
        scheme: vm.scheme.handle
      });
    }

    function setCategory() {
      if (vm.firstChoice == vm.selectedCat || vm.selectedCat == null) return;
      if (!vm.beforeSelect()) {
        vm.selectedCat = vm.model.primaryCategory;
        vm.model.primaryCategoryId = vm.selectedCat.id;
      } else {
        vm.model.primaryCategoryId = vm.selectedCat.id;
        vm.model.primaryCategory = vm.selectedCat;
        vm.onSelect();
      }
    }

    function removePcatIfTextIsEmpty(searchText) {
      if (!vm.searchText) {
        vm.model.primaryCategoryId = undefined;
        vm.model.primaryCategoryTitle = undefined;
      }
    }
  }
})();