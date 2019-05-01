(function () {
    'use strict';

    angular
        .module('app')
        .controller('formSchemesCtrl', formSchemesCtrl);

    formSchemesCtrl.$inject = ['$rootScope', '$state', '$mdDialog', '$q', 'formSchemes', 'formSchemeDataSrv', '$mdToast', '$mdExpansionPanel', '$stateParams', '$location', '$mdBottomSheet', '$timeout'];
    function formSchemesCtrl($rootScope, $state, $mdDialog, $q, formSchemes, formSchemeDataSrv, $mdToast, $mdExpansionPanel, $stateParams, $location, $mdBottomSheet, $timeout) {
        var vm = this;
        var routePageNumber = $stateParams.pageNumber;
        vm.formSchemes = formSchemes.data;
        vm.query = {
            pageSize: $location.search().pageSize || 10,
            pageNumber: $stateParams.pageNumber,
            totalCount: formSchemes.data.count
        }
        vm.showBottomSheetOptions = showBottomSheetOptions;
        vm.changePage = changePage;
        activate();

        ////////////////

        function activate() {
            $location.search({ pageSize: vm.query.pageSize })
            $rootScope.title = 'مدیریت قالب فرم ها';
        }
        function showBottomSheetOptions(form) {
            $mdBottomSheet.show({
                controller: 'bottomSheetCtrl',
                templateUrl: '/Src/app/partials/bottomSheetOptions-tpl.html',
                controllerAs: 'vm',
            }).then(function (res) {
                if (res == 'edit')
                    $state.go('updateFormScheme', { handle: form.handle,lang:$rootScope.currentLang });
                if (res == 'delete') {
                    showDeleteDialog(form);
                }
            });
        }

        function showDeleteDialog(form) {
            var confirm = $mdDialog.confirm()
                .title('از حذف این مورد اطمینان دارید')
                .textContent(form.title)
                .ok('بلی')
                .cancel('خیر');
            $mdDialog.show(confirm).then(function (a) {
                formSchemeDataSrv.delete(form.id).then(function (res) {
                    $mdToast.showSimple('انجام شد !');
                    $state.reload();
                });
            });
        }

        function changePage(pageNumber, pageSize) {
            if (pageNumber == routePageNumber
                && pageSize == $location.search().pageSize) return;
            routePageNumber = pageNumber;
            var defered = $q.defer();
            vm.tablePromise = defered.promise;
            formSchemeDataSrv.get({
                pageSize: pageSize,
                pageNumber: pageNumber,
                searchText: vm.searchText
            }).then(function (res) {
                vm.formSchemes = res.data;
                updateRouteParams();
                $timeout(function () {
                    defered.resolve();
                }, 1000);
            });
        }
        function updateRouteParams() {
            $location.search({ searchText: vm.searchText, pageSize: vm.query.pageSize, })
            $state.go($state.current, {
                pageNumber: vm.query.pageNumber,
            }, { reload: false });
        }




    }
})();