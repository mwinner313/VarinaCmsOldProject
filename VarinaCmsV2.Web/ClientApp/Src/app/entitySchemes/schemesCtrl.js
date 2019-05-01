(function () {
    'use strict';

    angular
        .module('app')
        .controller('schemesCtrl', schemeCtrl);

    schemeCtrl.$inject = ['$mdDialog', 'schemes', 'schemeDataService', '$state', '$stateParams', '$mdBottomSheet', '$mdToast', 'bottomSheetOptions'];

    function schemeCtrl($mdDialog, schemes, schemeDataService, $state, $stateParams, $mdBottomSheet, $mdToast, bottomSheetOptions) {
        var vm = this;
        vm.tableProgressPromise
        vm.selectedScheme = {};
        vm.schemeType = $stateParams.schemeType;
        vm.schemes = schemes.data;
        vm.deleteScheme = deleteScheme;
        vm.showBottomSheetOptions = showBottomSheetOptions;
        vm.showAddNewDialog = showAddNewDialog;

        activate();

        ////////////////

        function activate() {}

        function showAddNewDialog(event) {
            $mdDialog.show({
                templateUrl: '/Src/app/entitySchemes/scheme.add.tpl.html',
                controller: 'schemeAddCtrl',
                controllerAs: 'vm',
                targetEvent: event
            }).then(function () {

            }, function () {})
        }

        function deleteScheme() {
            schemeDataService.delete(vm.selectedScheme.id).then(function (res) {
                toastr.success('انجام شد');
                $('#delete').modal('hide');
                $state.reload();
            });
        }

        function showBottomSheetOptions(scheme) {
            $mdBottomSheet.show(bottomSheetOptions).then(function (selectedAction) {
                if (selectedAction === 'edit')
                    $state.go('schemeUpdate', {
                        scheme: scheme.handle,
                        lang: $stateParams.lang
                    })
                if (selectedAction === 'delete')
                    showDeleteDialog(scheme);
            });
        }


        function showDeleteDialog(scheme) {
            var confirm = $mdDialog.confirm()
                .title('از حذف این موجودیت از سیستم مطمعن هستید؟')
                .textContent(scheme.title)
                .ariaLabel('delete scheme confirm')
                .ok('بلی')
                .cancel('خیر');
            $mdDialog.show(confirm).then(function (res) {
                schemeDataService.delete(scheme.id).then(function (res) {
                    if(res.status==200){
                        $mdToast.showSimple('انجام شد');
                        $state.reload();
                    }else{
                        $mdToast.showSimple(res.data.message)
                    }
                   
                })
            }, function (err) {

            })
        }


        function getSchemes(pageNumber, pageSize) {
            if (pageNumber == $stateParams.pageNumber &&
                pageSize == $location.search().pageSize) return;

            var defered = $q.defer();
            vm.tableProgressPromise = defered.promise;
            schemeDataService.get({
                pageSize: pageSize,
                pageNumber: pageNumber,
            }).then(function (res) {
                defered.resolve(res.data);
                vm.schemes = res.data;
                vm.schemes.count = 200;
            });

            updateRouteParams();
        }

        function updateRouteParams() {
            $location.search({
                pageSize: vm.query.pageSize,
            })
            $state.go($state.current, {
                pageNumber: vm.query.pageNumber,
                lang: $stateParams.lang
            }, {
                reload: false
            });
        }

    }
})();