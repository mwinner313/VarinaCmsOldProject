(function () {
    'use strict';

    angular
        .module('app')
        .controller('userProfileCtrl', userProfileCtrl);

    userProfileCtrl.$inject = ['$sessionStorage', '$mdToast', 'identityManager'];
    function userProfileCtrl($sessionStorage, $mdToast, identityManager) {
        var vm = this;
        vm.changeInfo = changeInfo;
        vm.changePassword = changePassword;
        vm.userInfo = $sessionStorage.currentUser;
        vm.userAvatarUploader = identityManager.getUserAvatarUploader()
        vm.userAvatarUploader.onSuccessItem = updateUserInSessionStorage;
        vm.triggerFilePickerClick = triggerFilePickerClick;
        activate();

        ////////////////

        function activate() {

        }
        function triggerFilePickerClick() {
            angular.element('#filepicker').trigger('click')
        }
        function changePassword() {
            if (vm.changePasswordForm.$invalid) {
                $mdToast.showSimple('ورودی ها را بررسی کنید');
                return;
            }

            identityManager.changePassword(vm.changePasswordModel)
                .then(function (res) {
                    if (res.status == 200)
                        $mdToast.showSimple('انجام شد');
                    if (res.status == 500)
                        $mdToast.showSimple('رمز عبور قبلی اشتباه است');
                }, function (err) {
                    $mdToast.showSimple('عملیات ناموفق رمز عبور اشتباه است');
                });
        }
        function changeInfo() {
            if (vm.userInfoForm.$invalid) {
                $mdToast.showSimple('ورودی ها را بررسی کنید');
                return;
            }

            identityManager.editInfo(vm.userInfo).then(function (res) {
                $mdToast.showSimple('انجام شد');
            });
        }

        function updateUserInSessionStorage(item, response, status, headers) {
            $mdToast.showSimple('انجام شد');
            $sessionStorage.currentUser = response;
            vm.userInfo = response;
        }
    }
})();