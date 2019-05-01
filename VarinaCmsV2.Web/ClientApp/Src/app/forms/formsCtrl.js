(function () {
    'use strict';

    angular
        .module('app')
        .controller('FormsCtrl', FormsCtrl);

    FormsCtrl.$inject = ['forms','$q', 'formScheme', '$mdDialog', '$stateParams', 'formDataSrv', '$location', '$rootScope', '$state'];
    function FormsCtrl(forms,$q, formScheme, $mdDialog, $stateParams, formDataSrv, $location, $rootScope, $state) {
        var routePageNumber = $stateParams.pageNumber;
        
        var vm = this;
        vm.formScheme = formScheme.data;
        vm.forms = forms.data;
        vm.searchText;
        vm.getItems = getItems;
        vm.showDetails = showDetails;
        vm.openSearchDialog = openSearchDialog;
        vm.search = search;
        vm.query = {
            pageNumber: $stateParams.pageNumber,
            pageSize: ($location.search().pageSize || 15)
        }
        activate();

        ////////////////

        function activate() {
            $location.search({ pageSize: vm.query.pageSize });
            $rootScope.title = "لیست فرم های ثبت شده" + vm.formScheme.title;
        }

        function showDetails($event, form) {
            $mdDialog.show({
                templateUrl: '/Src/app/forms/SubmittedFormDetails.tpl.html',
                controller: 'FormDetailsCtrl',
                controllerAs: 'vm',
                bindToController: true,
                fullscreen: true,
                targetEvent: $event,
                clickOutsideToClose:true,
                locals: {
                    form: form
                }
            });
        }

        function openSearchDialog($event) {
            $mdDialog.show($mdDialog.prompt()
            .placeholder('عبارت خود را وارد کنید')
            .ariaLabel('جستجو')
            .targetEvent($event)
            .ok('جستجو')
            .cancel('بستن')).then(function (searchText) {
                vm.searchText = searchText;
                search();
            });
        }

        function getItems(pageNumber, pageSize) {
            if (pageNumber == routePageNumber
                && pageSize == $location.search().pageSize) return;

                routePageNumber=pageNumber;
            var defered = $q.defer();
            vm.tableProgressPromise = defered.promise;
            formDataSrv.get({
                pageSize: pageSize,
                pageNumber: pageNumber,
                searchText: vm.searchText,
                formSchemeHandle: vm.formScheme.handle
            }).then(function (res) {
                defered.resolve(res.data);
                vm.forms = res.data;
            });
            updateRouteParams();
        }

        function updateRouteParams() {
            $location.search({ searchText: vm.searchText, pageSize: vm.query.pageSize, })
            $state.go($state.current, {
                pageNumber: vm.query.pageNumber,
            }, { reload: false });
        }

        function search() {
            var defered = $q.defer();
            vm.tableProgressPromise = defered.promise;
            formDataSrv.get({
                pageSize: vm.query.pageSize,
                pageNumber: 1,
                searchText: vm.searchText,
                formSchemeHandle: vm.formScheme.handle
            }).then(function (res) {
                defered.resolve(res.data);
                vm.forms = res.data;
            });
            vm.query.pageNumber = 1;
            updateRouteParams();
        }
    }
})();