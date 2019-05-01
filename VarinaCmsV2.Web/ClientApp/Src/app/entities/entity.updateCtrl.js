(function () {
  "use strict";

  angular.module("app").controller("entityUpdateCtrl", entityUpdateCtrl);

  entityUpdateCtrl.$inject = [
    "$rootScope",
    "entity",
    "scheme",
    "lodash",
    "categoryType",
    "$window",
    "entityDataService",
    "datetimeRegex",
    "toastr",
    "$mdToast"
  ];
  function entityUpdateCtrl(
    $rootScope,
    entity,
    scheme,
    lodash,
    categoryType,
    $window,
    entityDataService,
    datetimeRegex,
    toastr,
    $mdToast
  ) {
    var vm = this;
    vm.datetimeRegex = datetimeRegex;
    vm.entity = entity.data;
    vm.scheme = scheme.data;
    vm.categoryType = categoryType;
    vm.refreshPcatFdsForEntity = refreshPcatFdsForEntity;
    vm.edit = edit;
    vm.confirmCategoryChange = confirmCategoryChange;
    vm.isUpdating = false;
    vm.secondaryCatParams = {
      scheme: vm.scheme.handle,
      checkByType: true,
      categoryType: vm.categoryType.secondary
    };

    activate();

    ////////////////
    vm.titleSetArgs = function (val, el, attrs, ngModel) {
      return { value: val, id: vm.entity.id };
    }
    function activate() {
      angular.forEach(vm.entity.fields, function (f, idx) {
        vm.entity.fields[idx].isField = true;
      });

      $rootScope.title = "ویرایش" + " " + vm.scheme.title;
      var newFields = [];
      Array.prototype.push.apply(
        newFields,
        lodash.differenceBy(
          vm.scheme.fieldDefenitions,
          vm.entity.fields,
          function (item) {
            if (item.isField) return item.fieldDefenitionId;
            return item.id;
          }
        )
      );
      Array.prototype.push.apply(
        newFields,
        lodash.differenceBy(
          vm.entity.primaryCategory.fieldDefenitions,
          vm.entity.fields,
          function (item) {
            if (item.isField) return item.fieldDefenitionId;
            return item.id;
          }
        )
      );
      addNewFieldsToEntity(newFields);
    }

    function confirmCategoryChange() {
      if (vm.entity.primaryCategory.fieldDefenitions.length)
        return confirm(
          " درصورت تغییر فیلد های دسته بندی اصلی حدف خواهند شد  (درصورت اعمال تغییرات) "
        );
      return true;
    }
    function refreshPcatFdsForEntity() {
      lodash.remove(vm.entity.fields, function (f) {
        return !!f.fd.categoryId;
      });
      addNewFieldsToEntity(vm.entity.primaryCategory.fieldDefenitions);
    }

    function edit() {
      vm.entityForm.$submitted = true;
      if (vm.entityForm.$invalid) {
        toastr.warning("مقادیر را بررسی نمایید");
        return;
      }
      vm.isUpdating = true;
      entityDataService.put(vm.entity, vm.scheme).then(function (res) {
        $mdToast.showSimple("انجام شد!");
        $window.history.back();
      });
    }

    function addNewFieldsToEntity(newFields) {
      angular.forEach(newFields, function (fd) {
        vm.entity.fields.push({
          entityId: vm.entity.id,
          fieldDefenitionId: fd.id,
          fd: fd
        });
      });
    }
  }
})();