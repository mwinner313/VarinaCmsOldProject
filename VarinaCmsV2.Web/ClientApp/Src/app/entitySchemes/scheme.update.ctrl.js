(function () {
    'use strict';

    angular
        .module('app')
        .controller('schemeUpdateCtrl', schemeUpdateCtrl);

    schemeUpdateCtrl.$inject = ['scheme', '$mdToast', '$mdDialog', 'schemeDataService', '$state', '$rootScope', 'fieldTypes', 'fdDataService', 'languages', 'lodash'];

    function schemeUpdateCtrl(scheme, $mdToast, $mdDialog, schemeDataService, $state, $rootScope, fieldTypes, fdDataService, languages, lodash) {
        var vm = this;
        vm.scheme = scheme.data;
        vm.fieldTypes = fieldTypes
        vm.newFd = {};
        vm.newFdGroup = {
            schemeId: vm.scheme.id
        };
        vm.isUpdatingField = false;
        vm.changeFdOrder = changeFdOrder;
        vm.editScheme = editScheme;
        vm.showFieldDefenitionDetails = showFieldDefenitionDetails;
        vm.addField = addField;
        vm.showFdRemoveDialog = showFdRemoveDialog;
        vm.showFdEditDialog = showFdEditDialog;
        vm.updateFieldGroup = updateFieldGroup;
        vm.confirmDeleteFdGroup = confirmDeleteFdGroup;
        vm.addFieldGroup = addFieldGroup;
        activate();

        ////////////////

        function activate() {
            vm.scheme.fieldDefenitions = lodash.orderBy(vm.scheme.fieldDefenitions, 'order', 'asc')
            $rootScope.title = "ویرایش موجودیت";
        }

        function showFieldDefenitionDetails(fd) {
            $mdDialog.show({
                clickOutsideToClose: true,
                template: ('<md-dialog-content dir="ltr" class="ltr-dialog"  layout-padding> <pre class="code">' + angular.toJson(fd, true) + '</pre></md-dialog-content>')
            })
        }

        function editScheme() {
            schemeDataService.put(vm.scheme).then(function (res) {
                $mdToast.showSimple('انجام شد');
                $rootScope.$broadcast('SchemesAddedOrUpdated');

                $state.go('schemes', {
                    pageNumber: 1,
                    lang: $rootScope.currentLang,
                    schemeType: vm.scheme.type
                });
            })
        }


        function addFieldGroup() {
            vm.fieldDefenitionGroupForm.$submitted = true;
            if (vm.fieldDefenitionGroupForm.$invalid) return;
            fdDataService.postFdGroup(vm.newFdGroup).then(function (res) {
                if (res.status != 200) return;
                vm.scheme.fieldDefenitionGroups.push(res.data);
                vm.newFdGroup = {
                    schemeId: vm.scheme.id
                };
                vm.fieldDefenitionGroupForm.$setPristine();
                vm.fieldDefenitionGroupForm.$setUntouched();
            })
        }

        function updateFieldGroup(group) {
            fdDataService.putFdGroup(group).then(function (res) {
                if (res.status === 200) {
                    $mdToast.showSimple('انجام شد');
                }
            })
        }

        function confirmDeleteFdGroup(group) {
            var confirm = $mdDialog.confirm()
                .title('بطور کامل حدف خواهد شد حذف شود؟')
                .textContent(group.title)
                .ok('بلی')
                .cancel('خیر');
            $mdDialog.show(confirm).then(function (res) {
                fdDataService.deleteFdGroup(group).then(function (res) {
                    if(res.status!==200){
                        $mdToast.showSimple(res.data.message);
                        return;
                    }
                    $mdToast.showSimple('انجام شد');
                    vm.scheme.fieldDefenitionGroups.splice(vm.scheme.fieldDefenitionGroups.indexOf(group), 1)
                })
            });
        }

     

        function addField() {
            vm.fieldDefenitionForm.$submitted = true;
            if (vm.fieldDefenitionForm.$invalid) return;
            vm.newFd.schemeId = vm.scheme.id;
            vm.newFd.order = vm.scheme.fieldDefenitions.length !== 0 ?
                vm.scheme.fieldDefenitions[vm.scheme.fieldDefenitions.length - 1].order + 1 : 1;
            fdDataService.post(vm.newFd).then(function (res) {
                $mdToast.showSimple('انجام شد');
                vm.scheme.fieldDefenitions.push(res.data);
                vm.newFd = {};
                vm.selectedType = undefined;
                vm.fieldDefenitionForm.$setPristine();
                vm.fieldDefenitionForm.$setUntouched();
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
                fdDataService.put(editedfd).then(function (res) {
                    var idx = lodash.indexOf(schemeOrGroup.fieldDefenitions, fd);
                    schemeOrGroup.fieldDefenitions[idx] = editedfd;
                    $mdToast.showSimple('انجام شد');
                });
            });
        }



        function showFdRemoveDialog(schemeOrGroup, fd) {
            var confirm = $mdDialog.confirm()
                .title('بطور کامل حدف خواهد شد حذف شود؟')
                .textContent(fd.title)
                .ok('بلی')
                .cancel('خیر');
            $mdDialog.show(confirm).then(function (res) {
                fdDataService.delete(fd.id).then(function (res) {
                    if (res.status !== 200) {
                        $mdToast.showSimple(res.data.message);
                        return;
                    }
                    $mdToast.showSimple('انجام شد');
                    schemeOrGroup.fieldDefenitions.splice(schemeOrGroup.fieldDefenitions.indexOf(fd), 1);
                })
            });
        }

        function changeFdOrder(schemeOrGroup) {
            vm.isUpdatingField = true;
            angular.forEach(schemeOrGroup.fieldDefenitions, function (item, idx) {
                if (schemeOrGroup == vm.scheme) {
                    item.schemeId = vm.scheme.id;
                    item.fieldDefenitionGroupId = undefined;
                } else {
                    item.fieldDefenitionGroupId = schemeOrGroup.id;
                    item.schemeId = undefined;
                }
                item.order = idx;
            });
            fdDataService.putBatch(schemeOrGroup.fieldDefenitions).then(function (res) {
                vm.isUpdatingField = false;
                $mdToast.showSimple('انجام شد');
            });
        }

    }
})();