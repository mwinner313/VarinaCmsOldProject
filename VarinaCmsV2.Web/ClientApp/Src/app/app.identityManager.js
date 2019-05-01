; (function () {
    'use strict';

    angular
        .module('app')
        .service('identityManager', identityManager);

    identityManager.$inject = ['FileUploader', '$sessionStorage', '$rootScope', 'api', '$http'];
    function identityManager(FileUploader, $sessionStorage, $rootScope, api, $http) {
        var currentIdentity = $sessionStorage.currentUser;

        var editActions = {
            none: 0,
            editInfo: 1,
            changePassword: 2,
            changeAvatar: 3,
        }

        var service = {
            hasAccess: hasAccess,
            isInRole: isInRole,
            changePassword: changePassword,
            editInfo: editInfo,
            getUserAvatarUploader: getUserAvatarUploader
        }

        $rootScope.isInRole = isInRole;

        return service;

        ////////////////

        function hasAccess(premissin) {
            return currentIdentity.premissions.some(function (x) { return x === premissin })
                || currentIdentity.roles.some(function (x) { return x === 'Supervisor' });
        }

        function isInRole(role) {
            return currentIdentity.roles.some(function (x) { return x === role })
                || currentIdentity.roles.some(function (x) { return x === 'Supervisor' });
        }

        function changePassword(data) {
            return $http.put(api.currentUser, {
                action: editActions.changePassword,
                changePasswordViewModel: data
            });
        }

        function editInfo(info) {
            return $http.put(api.currentUser, {
                action: editActions.editInfo,
                userInfoEditViewModel: info
            });
        }
        function getUserAvatarUploader() {
          
            return new FileUploader({
                url: api.currentUser,
                autoUpload :true,
                headers:{
                    contentType:""
                }
            });
        }
    }
})();