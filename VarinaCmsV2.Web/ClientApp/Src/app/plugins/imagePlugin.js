(function () {
    'use strict';

    // Usage:
    // 
    // Creates:
    // 

    angular
        .module('app')
        .component('imagePlugin', {
            templateUrl: '/Src/app/plugins/imagePlugin.html',
            controller: ControllerController,
            controllerAs: 'vm',
            bindings: {
                item: '=',
            },
        });

    ControllerController.$inject = ['$mdDialog', 'lodash'];

    function ControllerController($mdDialog, lodash) {
        var vm = this;
        vm.metaName = "image";
        vm.selectImageFromFilePicker = selectImageFromFilePicker;
        vm.removeImage = removeImage;
        ////////////////

        vm.$onInit = function () {
            vm.item.metaData=vm.item.metaData||[];
          var  imageMeta = lodash.find(vm.item.metaData, function (m) {
                return m.metaName === vm.metaName;
            })||{};
            vm.selectedImage=imageMeta.metaData;
        };
        vm.$onChanges = function (changesObj) {};
        vm.$onDestroy = function () {};

        function removeImage() {
            vm.item.metaData.splice(vm.item.metaData.indexOf(vm.selectedImage), 1)
            vm.selectedImage = {};
        }

        function selectImageFromFilePicker() {
            $mdDialog.show($mdDialog.imagePicker()).then(function (file) {
                if (itemHasImageMetaData())
                    changeItemMetaWithFile(file);
                else addNewImageMetaToItem(file);

                vm.selectedImage = {
                    path: file.path,
                    name: file.name
                };
            });

            function itemHasImageMetaData() {
                return lodash.findIndex(vm.item.metaData, function (i) {
                    return i.metaName == vm.metaName;
                }) !== -1;
            }

            function changeItemMetaWithFile(file) {
                var idx = lodash.findIndex(vm.item.metaData, function (i) {
                    return i.metaName == vm.metaName;
                });
                vm.item.metaData[idx].metaData = {
                    path: file.path,
                    name: file.name
                };
                vm.item.metaData[idx].hasChanges = true;
            }

            function addNewImageMetaToItem(file) {
                vm.item.metaData.push({
                    metaData: {
                        path: file.path,
                        name: file.name
                    },
                    metaName: vm.metaName,
                    targetId: vm.item.id
                })
            }

         
        }

    }

})();