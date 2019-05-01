(function () {
    'use strict';

    angular
        .module('app')
        .controller('fileUploadCtrl', fileUploadCtrl);

    fileUploadCtrl.$inject = ['FileUploader', 'api', '$mdDialog','$mdToast'];
    function fileUploadCtrl(FileUploader, api, $mdDialog,$mdToast) {
        var vm = this;
        vm.uplodedFileDatas=[];
        vm.uploader = new FileUploader({ url: api.file });
        vm.uploader.onSuccessItem =addToListAndCheckForHidingDialog;
        vm.closeDialog = closeDialog;

        activate();

        ////////////////

        function activate() { }

        

        function closeDialog() {
            $mdDialog.cancel();
        }

        function addToListAndCheckForHidingDialog(item,res,status,headers){
            vm.uplodedFileDatas.push(res);
            CheckForHidingDialog();
        }

        function CheckForHidingDialog(){
            if(vm.uplodedFileDatas.length==vm.uploader.queue.length){
                $mdToast.showSimple('انجام شد!');
                $mdDialog.hide(vm.uplodedFileDatas);
            }
        }
    }
})();