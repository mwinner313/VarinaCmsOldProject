; (function () {
    'use strict';

    angular
        .module('app')
        .controller('pageUpdateCtrl', pageUpdateCtrl);

    pageUpdateCtrl.$inject = ['identityManager', 'page', 'tinyMceOptions', 'pageDataService', 'toastr', '$state', 'permalinkFilter', 'datetimeRegex'];
    function pageUpdateCtrl(identityManager, page, tinyMceOptions, pageDataService, toastr, $state, permalinkFilter, datetimeRegex) {
        var vm = this;
        vm.datetimeRegex = datetimeRegex;
        vm.page = page.data;
        vm.isInRole = identityManager.isInRole;
        vm.HasDifrentUrl = permalinkFilter(vm.page.url) !== permalinkFilter(vm.page.title);
        vm.tinyMceOptions = tinyMceOptions;
        vm.updatePage = updatePage
        activate();

        ////////////////

        function activate() { }



        function updatePage() {
            if (vm.pageForm.$valid) {
                pageDataService.put(vm.page, vm.page.id).then(function (res) {
                    toastr.success('انجام شد');
                    window.history.back();
                });
            }
            else {
                toastr.warning('مقایر وارد شده را بررسی کنید')
            }
        }

    }

})();