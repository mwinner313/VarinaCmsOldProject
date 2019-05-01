(function () {
  "use strict";

  angular.module("app").controller("entityAddCtrl", entityAddCtrl);

  entityAddCtrl.$inject = [
    "scheme",
    "toastr",
    "entityDataService",
    "lodash",
    "$mdToast",
    "$timeout",
    "$state",
    "$rootScope",
    "datetimeRegex",
    "currentLang",
    "categoryType"
  ];

  function entityAddCtrl(
    scheme,
    toastr,
    entityDataService,
    lodash,
    $mdToast,
    $timeout,
    $state,
    $rootScope,
    datetimeRegex,
    currentLang,
    categoryType
  ) {
    var vm = this;

    vm.scheme = scheme.data;
    vm.categoryType = categoryType;
    vm.entity = {
      fields: [],
      languageName: currentLang.get(),
      schemeId: vm.scheme.id,
      isVisible: true
    };

    vm.datetimeRegex = datetimeRegex;
    vm.formSubmitted = false;
    vm.add = add;
    vm.getCreatedIndex = getCreatedIndex;
    vm.confirmCategoryChange = confirmCategoryChange;
    vm.secondaryCatParams = {
      scheme: vm.scheme.handle,
      checkByType: true,
      categoryType: vm.categoryType.secondary
    };
    vm.isSending = false;
    activate();

    ////////////////

    function activate() {
      $rootScope.title = "افزودن" + " " + vm.scheme.title;
    }
    var indexes = [];
    var counter = 0

    function getCreatedIndex(fd) {
      var indexed = lodash.find(indexes, function (idx) {
        return idx.id == fd.id
      });
      if (indexed) return indexed.index;
      indexes.push({
        id: fd.id,
        index: counter
      });
      return counter++;
    }

    function add() {



      vm.formSubmitted = true;
      vm.entityForm.$submitted = true;
      if (vm.entityForm.$invalid) {
        toastr.warning("لطفاورودی هارا چک کنید");
        return;
      }
      vm.isSending = true;
      entityDataService.post(vm.entity).then(function (res) {
        vm.isSending = false;
        if (res.status != 200) return;
        $mdToast.showSimple('انجام شد!');
        $state.go("entities", {
          scheme: vm.scheme.handle,
          pageNumber: 1,
          lang: $rootScope.currentLang
        });
      });
    }
  }

  function confirmCategoryChange() {
    return true;
  }

})();