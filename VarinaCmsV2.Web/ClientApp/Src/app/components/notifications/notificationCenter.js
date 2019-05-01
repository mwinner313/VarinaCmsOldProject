(function () {
    'use strict';

    angular
        .module('app')
        .component('notificationCenter', {

            templateUrl: '/Src/app/components/notifications/notificationCenter.html',
            controller: controller,
            controllerAs: 'vm',
            bindings: {

            },
        });

    controller.$inject = ['$rootScope', '$sessionStorage', '$mdDialog', '$state', 'lodash', 'formDataSrv', '$http'];
    function controller($rootScope, $sessionStorage, $mdDialog, $state, lodash, formDataSrv, $http) {
        var vm = this;
        vm.getNotifCount = getNotifCount;
        vm.notifications = [];

        //////////////// 

        vm.$onInit = function () {
            init();
            formDataSrv.addSubscriberFunction(function () {
                $http.get('/api/form', {
                    params: {
                        noPaginate: true,
                        notSeen: true
                    }
                }).then(function (res) {
                    vm.notifications = [];
                    $sessionStorage.notSeenForms = res.data;
                    init();
                });
            })
        };

        vm.$onChanges = function (changesObj) { };
        vm.$onDestroy = function () { };



        function init() {
            var formGroups = lodash.groupBy($sessionStorage.notSeenForms.items, 'formSchemeTitle');
            angular.forEach(Object.keys(formGroups), function (key) {
                var formSchemeHandle = formGroups[key][0].formSchemeHandle;
                vm.notifications.push({
                    title: ('پیام جدید ' + key),
                    count: formGroups[key].length,
                    url: $state.href('forms', {
                        lang: $rootScope.currentLang,
                        formHandle: formSchemeHandle,
                        pageNumber: 1
                    }, { absolute: true })

                })
            })
        }

        function getNotifCount() {
            return lodash.sumBy(vm.notifications, function (n) { return n.count || 0 })
        }

    }
})();


        // function openNotificationsDialog(event) {
        //     $mdDialog.show(
        //         {
        //             controller: 'cotificationDilogCtrl',
        //             controllerAs: 'vm',
        //             templateUrl: '/Src/app/components/notifications/notificationsDialog.html',
        //             targetEvent: event,
        //             escapeToClose: true,
        //             bindToController: true,
        //             locals: { data: vm.data },
        //             clickOutsideToClose: true,
        //             fullscreen: true
        //         }
        //     )
        // }