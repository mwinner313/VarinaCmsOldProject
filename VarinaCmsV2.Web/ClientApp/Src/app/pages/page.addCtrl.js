;(function () {
    'use strict';

    angular
        .module('app')
        .controller('pageAddCtrl', pageAddController);

    pageAddController.$inject = ['identityManager','tinyMceOptions', '$rootScope', 'pageDataService', 'toastr', '$state', 'currentLang','datetimeRegex'];
    function pageAddController(identityManager,tinyMceOptions, $rootScope, pageDataService, toastr, $state, currentLang,datetimeRegex) {
        var vm = this;
        vm.datetimeRegex = datetimeRegex;
        vm.page = {};
        vm.isInRole=identityManager.isInRole;
        vm.tinyMceOptions = tinyMceOptions;
        vm.addPage = addPage;
        activate();
        $rootScope.title = "افزودن صفحه ثابت";
        ////////////////
        function activate() { }


        function addPage() {
            vm.pageForm.$submitted = true;
            if (vm.pageForm.$valid) {
                vm.page.LanguageName = currentLang.get()
                pageDataService.post(vm.page).then(function (res) {
                    toastr.success('انجام شد');
                    $state.go('pages', { pageNumber: 1,lang:$rootScope.currentLang });
                }, function (err) {

                });
            }
            else {
                toastr.warning('مقایر وارد شده را بررسی کنید')
            }
        }
    }
})();