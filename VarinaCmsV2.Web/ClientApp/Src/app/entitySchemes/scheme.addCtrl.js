(function () {
    'use strict';

    angular
        .module('app')
        .controller('schemeAddCtrl', shcemeAddCtrl);

    shcemeAddCtrl.$inject = ['$stateParams', '$state', 'schemeDataService', '$mdToast', 'fieldTypes', 'languages', '$rootScope', '$mdDialog', 'lodash'];

    function shcemeAddCtrl($stateParams, $state, schemeDataService, $mdToast, fieldTypes, languages, $rootScope, $mdDialog, lodash) {

        var vm = this;
        vm.scheme = {
            fieldDefenitions: [],
            fieldDefenitionGroups: []
        };
        vm.addScheme = addScheme;
        vm.newFd = {};
        vm.newFdGroup = {
            fieldDefenitions: []
        };
        vm.schemeType = $stateParams.schemeType;
        vm.changeFdOrder = changeFdOrder;
        vm.addField = addField;
        vm.confirmDeleteFdGroup=confirmDeleteFdGroup;
        vm.addFieldGroup = addFieldGroup;
        vm.fieldTypes = fieldTypes;
        vm.showFdEditDialog = showFdEditDialog;
        vm.showFdRemoveDialog = showFdRemoveDialog;
        vm.showFieldDefenitionDetails = showFieldDefenitionDetails;

        activate();

        ////////////////

        function activate() {
            $rootScope.title = "افزودن موجودیت";
        }

        function addFieldGroup() {
            vm.fieldDefenitionGroupForm.$submitted = true;
            if (vm.fieldDefenitionGroupForm.$invalid) return;
            vm.scheme.fieldDefenitionGroups.push(vm.newFdGroup);
            vm.newFdGroup = {
                fieldDefenitions: []
            };
            vm.fieldDefenitionGroupForm.$setPristine();
            vm.fieldDefenitionGroupForm.$setUntouched();
        }


        function confirmDeleteFdGroup(group) {
            var confirm = $mdDialog.confirm()
                .title('حذف شود؟')
                .textContent(group.title)
                .ok('بلی')
                .cancel('خیر');
            $mdDialog.show(confirm).then(function (res) {
                vm.scheme.fieldDefenitionGroups.splice(vm.scheme.fieldDefenitionGroups.indexOf(group), 1)
                $mdToast.showSimple('انجام شد');
            });
        }

        function addField() {
            vm.fieldDefenitionForm.$submitted = true;
            if (vm.fieldDefenitionForm.$invalid) return;
            vm.newFd.order = vm.scheme.fieldDefenitions.length !== 0 ?
                vm.scheme.fieldDefenitions.length + 1 :
                1;
            vm.scheme.fieldDefenitions.push(vm.newFd);
            vm.newFd = {};
            vm.selectedType = undefined;
            vm.fieldDefenitionForm.$setPristine();
            vm.fieldDefenitionForm.$setUntouched();
        }


        function addScheme() {
            vm.schemeForm.$submitted = true;

            if (vm.schemeForm.$invalid) {
                $mdToast.showSimple('مقادیر را بررسی کیند');
                return;
            }
            setFieldDefenitionOrders()
            vm.scheme.type = vm.schemeType;
            schemeDataService.post(vm.scheme).then(function (res) {
                $mdToast.showSimple('انجام شد');
                $rootScope.$broadcast('SchemesAddedOrUpdated');
                $state.go('schemes', {
                    pageNumber: 1,
                    lang: $rootScope.currentLang,
                    schemeType: $stateParams.schemeType
                });
            })

            function setFieldDefenitionOrders() {
                angular.forEach(vm.scheme.fieldDefenitions, function (fd, idx) {
                    fd.order = idx;
                    angular.forEach(vm.scheme.fieldDefenitionGroups, function (fdg, idx) {
                        angular.forEach(fdg.fieldDefenitions, function (fdgfd, idx) {
                            fdgfd.order = idx;
                        })
                    })
                })
            }
        }

        function showFdRemoveDialog(schemeOrGroup, fd) {
            var confirm = $mdDialog.confirm()
                .title('حذف شود؟')
                .textContent(fd.title)
                .ok('بلی')
                .cancel('خیر');
            $mdDialog.show(confirm).then(function (res) {
                var idx = schemeOrGroup.fieldDefenitions.indexOf(fd);
                schemeOrGroup.fieldDefenitions.splice(idx, 1)
            })
        }

        function showFieldDefenitionDetails(fd) {
            $mdDialog.show({
                clickOutsideToClose: true,
                template: ('<md-dialog-content dir="ltr" layout-padding> <pre class="code">' + angular.toJson(fd, true) + '</pre></md-dialog-content>')
            })
        }

        function changeFdOrder(schemeOrGroup) {
            angular.forEach(schemeOrGroup.fieldDefenitions, function (item, idx) {
                item.order = idx;
            })
        }

        function showFdEditDialog(schemeOrGroup, fd) {
            $mdDialog.show({
                templateUrl: '/Src/app/fieldDefenition/fields.edit.tpl.html',
                controller: 'fdUpdateCtrl',
                controllerAs: 'vm',
                clickOutsideToClose: true,
                bindToController: true,
                locals: {
                    fd: angular.copy(fd),
                    fieldTypes: fieldTypes,
                    languages: languages
                }
            }).then(function (editedfd) {
                var idx = lodash.indexOf(schemeOrGroup.fieldDefenitions, fd);
                schemeOrGroup.fieldDefenitions[idx] = editedfd;
                $mdToast.showSimple('انجام شد');
            })
        }

    }
})();