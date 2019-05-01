(function () {
    'use strict';



    angular
        .module('app')
        .component('fieldsOrganizer', {
            templateUrl: '/Src/app/fieldOrganizer/fieldsOrganizer.tpl.html',
            controller: FieldOrganizer,
            controllerAs: 'vm',
            bindings: {
                entity: '=',
                primaryCategory: '=',
                scheme: '=',
                form:'='
            },
        });

    FieldOrganizer.$inject = ['lodash'];

    function FieldOrganizer(lodash) {
        var vm = this;

        vm.$onInit = function () {
            var groups = vm.scheme.fieldDefenitionGroups;
            Array.prototype.push.apply(groups, vm.primaryCategory.fieldDefenitionGroups)
            vm.groupedFields = [];
            angular.forEach(groups, function (g) {
                //fetch grouped fields
                var fieldsGrouped = {
                    group: g,
                    fields: (lodash.filter(vm.entity.fields, {
                        fieldDefenitionFieldDefenitionGroupId: g.id
                    })||[])
                }
                // new fields in group
                angular.forEach(lodash.differenceBy(g.fieldDefenitions, fieldsGrouped.fields, function (x) {
                    return x.fieldDefenitionId || x.id;
                }), function (fd) {
                    var newField = {
                        fd:fd,
                        fieldDefenitionId:fd.id
                    }
                    vm.entity.fields.push(newField)
                    fieldsGrouped.fields.push(newField);
                });
                vm.groupedFields.push(fieldsGrouped);
            })

        var allFieldDefenitions=vm.scheme.fieldDefenitions;
        Array.prototype.push.apply(allFieldDefenitions,vm.primaryCategory.fieldDefenitions)
            angular.forEach(lodash.differenceBy(allFieldDefenitions, vm.entity.fields, function (x) {
                return x.fieldDefenitionId || x.id;
            }),function(fd){
                var newField = {
                    fd:fd,
                    fieldDefenitionFieldDefenitionGroupId:'00000000-0000-0000-0000-000000000000',
                    fieldDefenitionId:fd.id
                }
                vm.entity.fields.push(newField)
            })
        };

        vm.$onChanges = function (changesObj) {};
        vm.$onDestroy = function () {};
    }
})();