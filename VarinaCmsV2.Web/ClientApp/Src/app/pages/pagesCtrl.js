;(function () {
    'use strict';

    angular
        .module('app')
        .controller('pagesCtrl', pagesCtrl);

    pagesCtrl.$inject = ['$q', "$rootScope", 'pages', 'pageDataService', '$stateParams', '$location', '$state', 'currentLang', '$mdDialog', '$mdToast', '$mdBottomSheet', 'bottomSheetOptions'];
    function pagesCtrl($q, $rootScope, pages, pageDataService, $stateParams, $location, $state, currentLang, $mdDialog, $mdToast, $mdBottomSheet, bottomSheetOptions) {
        var routePageNumber = $stateParams.pageNumber;
        var vm = this;
        vm.searchText = "";
        vm.pages = pages.data;
        vm.selectedPages = [];
        vm.tableProgressPromise;
        vm.openSearchDialog = openSearchDialog;
        vm.confirmDeleteList = confirmDeleteList;
        vm.search = search;
        vm.confirmDelete = confirmDelete;
        vm.getPages = getPages;
        vm.showBottomSheetOptions = showBottomSheetOptions;
        vm.query = {
            pageNumber: $stateParams.pageNumber,
            pageSize: ($location.search().pageSize || 15)
        }
        activate();
        ////////////////
        $rootScope.title = "مدیریت صفحات ثابت";

        function activate() {
            $location.search({pageSize:($location.search().pageSize || 15)})
        }

        function showBottomSheetOptions(page) {
            angular.extend(bottomSheetOptions, {
                locals: {
                    showUrl: page.toFullUrl
                }
            })
            $mdBottomSheet.show(bottomSheetOptions).then(function (res) {
                if (res === 'delete') confirmDelete(page);
                if (res === 'edit') $state.go('updatePage', { id: page.id,lang:$stateParams.lang });
            })
        }
        function confirmDeleteList($event) {
            var confirm = $mdDialog.confirm()
                .title('از انجام عملیات حذف اطمینان دارید؟')
                .textContent('')
                .targetEvent($event)
                .ok('بلی')
                .cancel('خیر');

            $mdDialog.show(confirm).then(function () {
                angular.forEach(vm.selectedPages, function (deletingPage, idx) {
                    pageDataService.delete(deletingPage.id).then(function (res) {
                        if ((vm.selectedPages.length) == (idx + 1)) {
                            $mdToast.show(
                                $mdToast.simple()
                                    .textContent('انجام شد!')
                                    .parent()
                                    .position("bottom left")
                                    .hideDelay(3000)
                            );
                            $state.reload();
                        }
                    });
                });
            });
        }
        function confirmDelete(page) {
            var confirm = $mdDialog.confirm()
                .title('از انجام عملیات حذف اطمینان دارید؟')
                .textContent(page.title)
                .ok('بلی')
                .cancel('خیر');

            $mdDialog.show(confirm).then(function () {
                pageDataService.delete(page.id).then(function (res) {
                    $mdToast.show(
                        $mdToast.simple()
                            .textContent('انجام شد!')
                            .position("bottom left")
                            .hideDelay(3000)
                    );
                    $state.reload();
                });
            });
        }

        function getPages(pageNumber, pageSize) {
            if (pageNumber == routePageNumber
                && pageSize == $location.search().pageSize) return;
                routePageNumber=pageNumber;

            var defered = $q.defer();
            vm.tableProgressPromise = defered.promise;
            pageDataService.get({
                pageSize: pageSize,
                pageNumber: pageNumber,
                languageName: currentLang,
                searchText: vm.searchText
            }).then(function (res) {
                defered.resolve(res.data);
                vm.pages = res.data;
            });

            updateRouteParams();
        }

        function search() {
            var defered = $q.defer();
            vm.tableProgressPromise = defered.promise;
            pageDataService.get({
                pageSize: vm.query.pageSize,
                languageName: currentLang,
                pageNumber:1,
                searchText: vm.searchText
            }).then(function (res) {
                defered.resolve(res.data);
                vm.pages = res.data;
            });
            vm.query.pageNumber=1;
            updateRouteParams();
        }
        function updateRouteParams() {
            $location.search({ searchText: vm.searchText, pageSize: vm.query.pageSize, })
            $state.go($state.current, {
                pageNumber: vm.query.pageNumber,
            }, { reload: false });
        }

        function openSearchDialog($event) {
            $mdDialog.show({
                parent: document.getElementsByTagName('body')[0],
                targetEvent: $event,
                templateUrl: '/Src/app.core/directives/SearchDialog/tpl.html',
                controller: 'searchDialogCtrl',
                controllerAs: 'vm',
                bindToController: true,
                clickOutsideToClose: true,
            }).then(function (searchText) {
                vm.searchText = searchText;
                vm.search();
            });
        }
    }
})();