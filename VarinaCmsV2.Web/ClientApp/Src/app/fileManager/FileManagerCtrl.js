(function () {
    'use strict';

    angular
        .module('app')
        .controller('FileManagerCtrl', FileManagerCtrl);

    FileManagerCtrl.$inject = ['$mdDialog', 'fileDataSrv', '$mdToast'];

    function FileManagerCtrl($mdDialog, fileDataSrv, $mdToast) {
        var vm = this;
        vm.selectedFile = {};
        vm.selectedFiles = [];
        vm.multiSelectEnabled = false;
        vm.isLoading = false;
        vm.clearSelectedFilesAndChangeMultiSelectSatae = clearSelectedFilesAndChangeMultiSelectSatae;
        vm.isActionInProgres = false;
        vm.dontLoadAnyMore = false;
        vm.clipBoardCopiedCb = clipBoardCopiedCb;
        var pagination = {
            size: 50,
            number: 1
        };
        vm.isSelectedFile = isSelectedFile;
        vm.selectFile = selectFile;
        vm.close = close;
        vm.loadMore = loadMore;
        vm.deleteSelectedFiles = deleteSelectedFiles;
        vm.search = search;
        vm.remove = remove;
        vm.showEditDialog = showEditDialog;
        vm.showFileUploadDialog = showFileUploadDialog;
        vm.sendToDialogOpener = sendToDialogOpener;
        if (vm.openedByOtherComponent)
            vm.selectImageFromPlugin = selectImageFromPlugin;
        activate();

        ////////////////

        function activate() {
            vm.isLoading = true;
            fileDataSrv.get({
                pageSize: pagination.size,
                searchText: vm.searchText,
                extension: vm.extension
            }).then(function (res) {
                vm.groupedFiles = res.data;
                vm.isLoading = false;
            });
        }


        function deleteSelectedFiles($event) {
            var confirm = $mdDialog.confirm()
                .title('از انجام عملیات حذف اطمینان دارید؟')
                .textContent()
                .targetEvent($event)
                .ok('بلی')
                .cancel('خیر')
                .multiple(true);

            $mdDialog.show(confirm).then(function () {
                vm.isActionInProgres = true;
                angular.forEach(vm.selectedFiles, function (file) {
                    fileDataSrv.delete(file.id).then(function (res) {
                        vm.selectedFiles.splice(vm.selectedFiles.indexOf(file), 1);
                        if (!vm.selectedFiles.length) {
                            activate();
                            $mdToast.showSimple('انجام شد');
                            vm.isActionInProgres = false;
                        }
                    });
                })
            });
        }



        function isSelectedFile(file) {
            return vm.multiSelectEnabled ?
                existInSelectedFiles(file) : (vm.selectedFile.id == file.id);
        }

        function existInSelectedFiles(file) {
            return vm.selectedFiles.indexOf(file) !== -1
        }

        function sendToDialogOpener(file) {
            if (vm.openedByOtherComponent) {
                if (vm.multiSelectEnabled && vm.canAcceptMultiFile) {
                    $mdDialog.hide(vm.selectedFiles);
                }else{
                    $mdDialog.hide(file);
                }
            }
        }

        function clearSelectedFilesAndChangeMultiSelectSatae() {
            vm.selectedFile = {};
            vm.selectedFiles = [];
            vm.multiSelectEnabled = !vm.multiSelectEnabled
        }

        function close() {
            $mdDialog.cancel();
        }

        function selectFile(file) {
            if (vm.multiSelectEnabled) {
                if (existInSelectedFiles(file)) {
                    vm.selectedFiles.splice(vm.selectedFiles.indexOf(file), 1);
                } else {
                    vm.selectedFiles.push(file)
                }
            } else {
                vm.selectedFile = file;
                vm.selectedFileFullPath = window.location.host + file.path;
            }
        }

        function selectImageFromPlugin() {
            return function (file) {
                $mdDialog.hide(file);
            }
        }

        function loadMore() {
            if (vm.isLoading || vm.dontLoadAnyMore) return;
            vm.isLoading = true;
            fileDataSrv.get({
                pageSize: pagination.size,
                pageNumber: (++pagination.number),
                searchText: vm.searchText,
                extension: vm.extension
            }).then(function (res) {

                if (!res.data.length)
                    vm.dontLoadAnyMore = true

                angular.forEach(res.data, function (g) {
                    if (vm.groupedFiles[(vm.groupedFiles.length - 1)].dateTime == g.dateTime) {
                        Array.prototype.push.apply(vm.groupedFiles[(vm.groupedFiles.length - 1)].files, g.files)
                    } else {
                        vm.groupedFiles.push(g);
                    }
                });
                vm.isLoading = false;
            });
        }

        function search() {
            pagination = {
                size: 50,
                number: 1
            };
            vm.dontLoadAnyMore = false;
            vm.groupedFiles = [];
            activate();
        }

        function remove(group, file, $event) {

            var confirm = $mdDialog.confirm()
                .title('از انجام عملیات حذف اطمینان دارید؟')
                .textContent(file.name)
                .targetEvent($event)
                .ok('بلی')
                .cancel('خیر')
                .multiple(true);

            $mdDialog.show(confirm).then(function () {
                vm.isActionInProgres = true;
                fileDataSrv.delete(file.id).then(function (res) {
                    vm.isActionInProgres = false;
                    group.files.splice(group.files.indexOf(file), 1);
                    if (!group.files.length) {
                        vm.groupedFiles.splice(vm.groupedFiles.indexOf(group), 1);
                    }
                    $mdToast.showSimple('انجام شد');
                    if (!!vm.selectedFile && vm.selectedFile.id == file.id) vm.selectedFile = undefined;
                });
            });
        }

        function showEditDialog(file, group) {
            $mdDialog.show({
                templateUrl: '/Src/app/fileManager/fileEdit.tpl.html',
                controller: 'FileEditCtrl',
                controllerAs: 'vm',
                multiple: true,
                clickOutsideToClose: true,
                fullscreen: true,
                bindToController: true,
                locals: {
                    file: angular.copy(file)
                }
            }).then(function (res) {
                group.files.splice(group.files.indexOf(file), 1, res);

                if (vm.selectedFile.id == file.id) vm.selectedFile = res;
            });
        }

        function clipBoardCopiedCb() {
            $mdToast.showSimple('انجام شد');
        }

        function showFileUploadDialog() {
            $mdDialog.show({
                templateUrl: '/Src/app/fileManager/fileUpload.tpl.html',
                controller: 'fileUploadCtrl',
                controllerAs: 'vm',
                multiple: true,
                clickOutsideToClose: true,
                fullscreen: true,
                bindToController: true,
            }).then(function (files) {
                angular.forEach(files, function (file) {
                    if ((!!vm.groupedFiles.length) &&
                        (file.createDateTimeString.slice(0, "1111/01/01".length) == vm.groupedFiles[0].dateTime))
                        vm.groupedFiles[0].files.splice(0, 0, file);
                    else
                        vm.groupedFiles.splice(0, 0, {
                            dateTime: file.createDateTimeString.slice(0, "1111/01/01".length),
                            files: [file]
                        });
                });
            });
        }

    }
})();