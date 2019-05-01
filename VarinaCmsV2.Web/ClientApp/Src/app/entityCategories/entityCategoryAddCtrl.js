(function () {
    'use strict';

    angular
        .module('app')
        .controller('EntityCategoryAddCtrl', EntityCategoryAddCtrl);

    EntityCategoryAddCtrl.$inject = ['$mdDialog', 'categoryDataService', 'currentLang','$mdToast','categoryType','identityManager'];
    function EntityCategoryAddCtrl($mdDialog, categoryDataService, currentLang,$mdToast,categoryType,identityManager) {
        var vm = this;
        vm.category={schemeId:vm.scheme.id,languageName: currentLang.get(),
            categoryType: categoryType.mixed
            ,
        fieldDefenitionGroups:[]}
        vm.close = close;
        vm.categoryType=categoryType;
        vm.ok = ok;
        vm.isInRole=identityManager.isInRole;
        
        activate();
        ////////////////
        function activate() { }

        function close() {
            $mdDialog.hide();
        }
        function ok() {
            vm.categoryForm.$submitted=true;
            if (vm.categoryForm.$invalid) return;
            vm.isSendingData = true;
            categoryDataService.post(vm.category).then(function (res) {
                $mdDialog.hide(res.data);
                $mdToast.showSimple('انجام شد');
            });
        }
    }
})();