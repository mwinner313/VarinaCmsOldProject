(function () {
    'use strict';

    // Usage:
    // 
    // Creates:
    // 

    angular
        .module('app')
        .component('fieldDefenitions', {

            controller: FieldDefenitions,
            controllerAs: 'vm',
            templateUrl: '/Src/app/fieldDefenition/fieldsTemplate.html',
            bindings: {
                scheme: '=',
                parentIdPropertyName: '@'
            },
        });

    FieldDefenitions.$inject = ['fieldTypes', '$mdDialog', 'fdDataService', '$mdToast', '$timeout'];

    function FieldDefenitions(fieldTypes, $mdDialog, fdDataService, $mdToast, $timeout) {
        var vm = this;
        vm.addField = addField;
        vm.fieldTypes = fieldTypes;
        vm.showFdEditDialog = showFdEditDialog;
        vm.showFdRemoveDialog = showFdRemoveDialog;
        vm.changeFdOrder = changeFdOrder;
        vm.addFieldGroup = addFieldGroup;
        vm.confirmDeleteFdGroup = confirmDeleteFdGroup;
        vm.updateFieldGroup = updateFieldGroup;
        vm.newFdGroup = {
            fieldDefenitions: [],
        };
        ////////////////

        vm.$onInit = function () {
            vm.scheme.fieldDefenitions = vm.scheme.fieldDefenitions || [];
        };
        vm.$onChanges = function (changesObj) {};
        vm.$onDestroy = function () {};

        function addFieldGroup() {
            vm.fieldDefenitionGroupForm.$submitted = true;
            if (vm.fieldDefenitionGroupForm.$invalid) return;
            if (vm.scheme.id) {
                vm.newFdGroup[vm.parentIdPropertyName] = vm.scheme.id
                fdDataService.postFdGroup(vm.newFdGroup).then(function (res) {
                    if (res.status != 200) return;
                    vm.scheme.fieldDefenitionGroups.push(res.data);
                    vm.newFdGroup = {
                        fieldDefenitions: []
                    };
                    vm.fieldDefenitionGroupForm.$setPristine();
                    vm.fieldDefenitionGroupForm.$setUntouched();
                    $mdToast.showSimple('انجام شد');
                })
            } else {
                vm.scheme.fieldDefenitionGroups.push(vm.newFdGroup);
                vm.newFdGroup = {
                    fieldDefenitions: []
                };
                vm.fieldDefenitionGroupForm.$setPristine();
                vm.fieldDefenitionGroupForm.$setUntouched();
            }

        }



        function updateFieldGroup(group) {
            if (vm.scheme.id)
                fdDataService.putFdGroup(group).then(function (res) {
                    if (res.status === 200) {
                        $mdToast.showSimple('انجام شد');
                    }
                })
        }

        function confirmDeleteFdGroup(group) {
            var confirm = $mdDialog.confirm()
                .title('حذف شود؟')
                .textContent(group.title).multiple(true)
                .ok('بلی')
                .cancel('خیر');
            if (vm.scheme.id) {
                $mdDialog.show(confirm).then(function (res) {
                    fdDataService.deleteFdGroup(group).then(function (res) {
                        if (res.status !== 200) {
                            $mdToast.showSimple(res.data.message);
                            return;
                        }
                        vm.scheme.fieldDefenitionGroups.splice(vm.scheme.fieldDefenitionGroups.indexOf(group), 1)
                        $mdToast.showSimple('انجام شد');
                    })
                });
            } else {
                $mdDialog.show(confirm).then(function (res) {
                    vm.scheme.fieldDefenitionGroups.splice(vm.scheme.fieldDefenitionGroups.indexOf(group), 1)
                    $mdToast.showSimple('انجام شد');
                });
            }
        }



        function addField() {
            vm.fieldDefenitionForm.$submitted = true;
            if (vm.fieldDefenitionForm.$invalid) return;
            vm.newFd.order = vm.scheme.fieldDefenitions.length !== 0 ?
                vm.scheme.fieldDefenitions.length + 1 :
                1;

            if (vm.scheme.id) {
                vm.newFd[vm.parentIdPropertyName] = vm.scheme.id;
                fdDataService.post(vm.newFd).then(function (res) {
                    $mdToast.showSimple('انجام شد');
                    vm.scheme.fieldDefenitions.push(res.data);
                    vm.newFd = {};
                    vm.selectedType = undefined;
                    vm.fieldDefenitionForm.$setPristine();
                    vm.fieldDefenitionForm.$setUntouched();
                })
            } else {
                vm.scheme.fieldDefenitions.push(vm.newFd);
                vm.newFd = {};
                vm.selectedType = undefined;
                vm.fieldDefenitionForm.$setPristine();
                vm.fieldDefenitionForm.$setUntouched();
            }
        }

        function showFdEditDialog(schemeOrGroup, fd) {
            $mdDialog.show($mdDialog.fdEdit({
                locals: {
                    fieldTypes: fieldTypes,
                    fd: fd
                }
            })).then(function (edited) {
                if (vm.scheme.id) {
                    fdDataService.put(fd).then(function (res) {
                        schemeOrGroup.fieldDefenitions[schemeOrGroup.fieldDefenitions.indexOf(fd)] = edited;
                        $mdToast.showSimple('انجام شد');
                    });
                } else {
                    schemeOrGroup.fieldDefenitions[schemeOrGroup.fieldDefenitions.indexOf(fd)] = edited;
                }
            });
        }

        function showFdRemoveDialog(schemeOrGroup, fd) {
            $mdDialog.show($mdDialog.confirm().title("از حذف این مورد اطمینان دارید؟").ok('بلی')
                .textContent(fd.title)
                .cancel('خیر')
                .multiple(true)

            ).then(function () {
                if (vm.scheme.id) {
                    fdDataService.delete(fd.id).then(function (res) {
                        schemeOrGroup.fieldDefenitions.splice(schemeOrGroup.fieldDefenitions.indexOf(fd), 1);
                        $mdToast.showSimple('انجام شد');
                    });
                } else {
                    schemeOrGroup.fieldDefenitions.splice(schemeOrGroup.fieldDefenitions.indexOf(fd), 1);
                }
            });
        }

        function changeFdOrder(schemeOrGroup) {
            angular.forEach(schemeOrGroup.fieldDefenitions, function (item, idx) {

                if (schemeOrGroup == vm.scheme) {
                    item[vm.parentIdPropertyName] = vm.scheme.id;
                    item.fieldDefenitionGroupId = undefined;
                } else {
                    item.fieldDefenitionGroupId = schemeOrGroup.id;
                    item[vm.parentIdPropertyName] = undefined;
                }

                item.order = idx;
            });
            if (vm.scheme.id)
                fdDataService.putBatch(schemeOrGroup.fieldDefenitions).then(function (res) {
                    $mdToast.showSimple('انجام شد');
                });
        }

    }
})();