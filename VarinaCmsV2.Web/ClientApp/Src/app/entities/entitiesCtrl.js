(function () {
    'use strict';

    angular
        .module('app')
        .controller('entitiesCtrl', entitesCtrl);

    entitesCtrl.$inject = ['categories', '$controller', '$timeout', '$q', '$state', '$stateParams', '$location', 'scheme', 'entities', "entityDataService", "toastr", '$rootScope', '$mdDialog', '$mdToast', 'currentLang', '$mdBottomSheet', 'bottomSheetOptions'];
    function entitesCtrl(categories, $controller, $timeout, $q, $state, $stateParams, $location, scheme, entities, entityDataService, toastr, $rootScope, $mdDialog, $mdToast, currentLang, $mdBottomSheet, bottomSheetOptions) {
        var routePageNumber = $stateParams.pageNumber;
        var vm = this;
        vm.searchText = "";
        vm.categories = categories.data;
        vm.entities = entities.data;
        vm.scheme = scheme.data;
        vm.selectedEntities = [];
        vm.tableProgressPromise;
        vm.confirmDelete = confirmDelete;
        vm.confirmDeleteList = confirmDeleteList;
        vm.getEntitiesAndUpdateRoute = getEntitiesAndUpdateRoute;
        vm.openSearchDialog = openSearchDialog;
        vm.search = search;
        vm.showBottomSheetOptions = showBottomSheetOptions;
        vm.query = {
            pageNumber: $stateParams.pageNumber,
            pageSize: ($location.search().pageSize || 15)
        }
        activate();

        ////////////////

        function activate() {
            $rootScope.title = "مدیریت" + " " + vm.scheme.title;
        }

        function showBottomSheetOptions(entity) {
            angular.extend(bottomSheetOptions, {
                locals: {
                    showUrl: entity.toFullUrl
                }
            });

            $mdBottomSheet.show(bottomSheetOptions).then(function (res) {
                if (res === 'delete') confirmDelete(entity);
                if (res === 'edit') $state.go('entityUpdate', {
                    id: entity.id,
                    scheme: vm.scheme.handle,
                    lang:$stateParams.lang
                });
            });
        }

        function confirmDeleteList($event) {
            var confirm = $mdDialog.confirm()
                .title('از انجام عملیات حذف اطمینان دارید؟')
                .textContent('')
                .targetEvent($event)
                .ok('بلی')
                .cancel('خیر');

            $mdDialog.show(confirm).then(function () {
                angular.forEach(vm.selectedEntities, function (entity, idx) {
                    entityDataService.delete(vm.scheme.handle, entity.id).then(function (res) {
                        if ((vm.selectedEntities.length - 1) == idx) {
                            $mdToast.show(
                                $mdToast.simple()
                                    .textContent('انجام شد!')
                                    .position("bottom left")
                                    .hideDelay(3000)
                            );
                            $state.reload();
                        }
                    });
                });
            });
        }

        function confirmDelete(entity) {
            var confirm = $mdDialog.confirm()
                .title('از انجام عملیات حذف اطمینان دارید؟')
                .textContent(entity.title)
                .ok('بلی')
                .cancel('خیر');

            $mdDialog.show(confirm).then(function () {
                entityDataService.delete(vm.scheme.handle, entity.id).then(function (res) {
                    $mdToast.show(
                        $mdToast.simple()
                            .textContent('انجام شد!')
                            .position("bottom left")
                            .parent()
                            .hideDelay(3000));
                    $state.reload();
                });
            });
        }
        function getEntitiesAndUpdateRoute(pageNumber, pageSize) {
            if (pageNumber == routePageNumber
                && pageSize == $location.search().pageSize) return;

            routePageNumber = pageNumber;

            var defered = $q.defer();
            vm.tableProgressPromise = defered.promise;
            entityDataService.get({
                scheme: vm.scheme.handle,
                pageSize: pageSize,
                pageNumber: pageNumber,
                languageName: currentLang.get(),
                searchText: vm.searchText,
                primaryCategoryId:getCatId()
            }).then(function (res) {
                defered.resolve(res.data);
                vm.entities = res.data;
            });

            updateRouteParams();
        }

        function search() {
            var defered = $q.defer();
            vm.tableProgressPromise = defered.promise;
            entityDataService.get({
                scheme: vm.scheme.handle,
                pageSize: vm.query.pageSize,
                pageNumber: 1,
                languageName: currentLang.get(),
                searchText: vm.searchText,
                primaryCategoryId:getCatId()
            }).then(function (res) {
                defered.resolve(res.data);
                vm.entities = res.data;
            });
            vm.query.pageNumber = 1;
            updateRouteParams();
        }

        function getCatId(){
            if(vm._selectedCat)
           return vm._selectedCat.id;

           return undefined;
        }
        function updateRouteParams() {
            $location.search({ searchText: vm.searchText, pageSize: vm.query.pageSize, })
            $state.go($state.current, {
                scheme: vm.scheme.handle,
                pageNumber: vm.query.pageNumber,
            }, { reload: false });
        }

        function openSearchDialog($event) {
            $mdDialog.show($mdDialog.search()).then(function (searchText) {
                vm.searchText = searchText;
                vm.search();
            });
        }
    }
})();