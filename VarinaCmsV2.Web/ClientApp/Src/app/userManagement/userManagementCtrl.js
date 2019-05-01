(function () {
    'use strict';

    angular
        .module('app')
        .controller('UserManagementCtrl', UserManagementCtrl);

    UserManagementCtrl.$inject = ['$q', 'userDataSrv', 'users', '$mdToast', '$state', '$rootScope', '$mdDialog', '$stateParams', '$location'];

    function UserManagementCtrl($q, userDataSrv, users, $mdToast, $state, $rootScope, $mdDialog, $stateParams, $location) {
        var routePageNumber = $stateParams.pageNumber;

        var vm = this;
        vm.users = users.data;
        vm.search = search;
        vm.showEditDialog = showEditDialog;
        vm.showAddDialog = showAddDialog;
        vm.showRemoveDialog = showRemoveDialog;
        vm.getUsers = getUsers;
        vm.query = {
            pageNumber: $stateParams.pageNumber,
            pageSize: ($location.search().pageSize || 15)
        }
        activate();

        ////////////////

        function activate() {
            $rootScope.title = "مدیریت کاربران";
        }

        function showEditDialog($event, user) {
            $mdDialog.show({
                templateUrl: '/Src/app/userManagement/user-edit.tpl.html',
                controller: 'userEditCtrl',
                controllerAs: 'vm',
                locals: {
                    user: angular.copy(user)
                },
                targetEvent: $event,
                clickOutsideToClose: true,
                bindToController: true,
                fullscreen: true
            }).then(function (editedUser) {
                replaceInlist(user, editedUser);
                $mdToast.showSimple('انجام شد');
            })
        }

        function showAddDialog($event) {
            $mdDialog.show({
                templateUrl: '/Src/app/userManagement/user-add.tpl.html',
                controller: 'userAddCtrl',
                controllerAs: 'vm',
                targetEvent: $event,
                clickOutsideToClose: true,
                bindToController: true,
                fullscreen: true
            }).then(function (newUser) {
                $mdToast.showSimple('انجام شد');
                $state.reload();
            })
        }

        function showRemoveDialog($event, user) {
            var confirm = $mdDialog.confirm()
                .title('از حذف این کاربر اطمینان دارید؟')
                .textContent(user.userName).ok('بلی').cancel('خیر')
            $mdDialog.show(confirm).then(function () {
                userDataSrv.delete(user.id).then(function (res) {
                    $mdToast.showSimple('انجام شد');
                    $state.reload();
                });
            })
        }

        function replaceInlist(user, editedUser) {
            vm.users.items[vm.users.items.indexOf(user)] = editedUser;
        }

        function getUsers(pageNumber, pageSize) {
            if (pageNumber == routePageNumber &&
                pageSize == $location.search().pageSize) return;
            routePageNumber = pageNumber;

            var defered = $q.defer();
            vm.tableProgressPromise = defered.promise;
            userDataSrv.get(vm.query).then(function (res) {
                defered.resolve(res.data);
                vm.users = res.data;
            });
            updateRouteParams();
        }

        function updateRouteParams() {
            $state.go($state.current, {
                pageNumber: vm.query.pageNumber,
            }, {
                reload: false
            });
        }

        function search() {
            vm.query.pageNumber = 1;
            var defered = $q.defer();
            vm.tableProgressPromise = defered.promise;
            userDataSrv.get(vm.query).then(function (res) {
                defered.resolve(res.data);
                vm.users = res.data;
            });

            updateRouteParams();
        }

    }
})();