;
(function () {
    'use strict';

    angular
        .module('app')
        .controller('sideNavCtrl', SideNavCtrl);

    SideNavCtrl.$inject = ['SchemeTypes', '$mdSidenav', '$timeout', '$scope', 'ssSideNav', 'identityManager', '$sessionStorage', 'currentLang'];

    function SideNavCtrl(SchemeTypes, $mdSidenav, $timeout, $scope, ssSideNav, identityManager, $sessionStorage, currentLang) {
        var vm = this;
        vm.schemes = []
        vm.menu = ssSideNav;
        vm.$storage = $sessionStorage;
        $scope.$on('SchemesAddedOrUpdated', function () {
            activate();
        });
        $scope.$on('languageChanged', function () {
            activate();
        });
        activate();
        ////////////////

        function activate() {
            var entitySchemes = $sessionStorage.entitySchemes;
            var formSchemes = $sessionStorage.formSchemes;
            ssSideNav.sections = [];
            ssSideNav.sections.push({
                name: 'داشبورد',
                type: 'link',
                icon: 'dashboard',
                state: "dashboard({lang:'" + currentLang.get() + "'})"
            });
            if (identityManager.isInRole('Developer')) {
                ssSideNav.sections.push({
                    name: 'مدیریت موجودیت ها',
                    type: 'link',
                    icon: 'entities',
                    state: "schemes({lang:'" + currentLang.get() + "',pageNumber:1,schemeType:'" + SchemeTypes.entity + "'})"
                });
                ssSideNav.sections.push({
                    name: 'مدیریت موجودیت های فروشگاه',
                    type: 'link',
                    icon: 'shop-entities',
                    state: "schemes({lang:'" + currentLang.get() + "',pageNumber:1,schemeType:'" + SchemeTypes.product + "'})"
                });
            }


            if (identityManager.isInRole('Administrator') || identityManager.isInRole('PrincipalAdministrator')) {
                ssSideNav.sections.push({
                    name: 'مدیریت کاربران',
                    type: 'link',
                    icon: 'users',
                    state: "userManagement({lang:'" + currentLang.get() + "',pageNumber:1})"
                });
            }

            ssSideNav.sections.push({
                name: 'مدیریت صفحات ثابت',
                type: 'toggle',
                icon: 'pages',
                pages: [{
                    name: '-- + افزودن',
                    state: 'addPage({lang:"' + currentLang.get() + '"})'
                }, {
                    name: '-- لیست',
                    state: "pages({lang:'" + currentLang.get() + "',pageNumber:1})"
                }]
            });

            angular.forEach(entitySchemes, function (s) {
                ssSideNav.sections.push({
                    name: ' مدیریت ' + s.title,
                    type: 'toggle',
                    pages: [{
                        name: '-- + افزودن',
                        state: "entityAdd({lang:'" + currentLang.get() + "',scheme:'" + s.handle + "'})"
                    }, {
                        name: '-- لیست',
                        state: "entities({lang:'" + currentLang.get() + "',pageNumber:1,scheme:'" + s.handle + "'})"
                    }, {
                        name: '-- دسته بندی ها',
                        state: "categories({lang:'" + currentLang.get() + "',pageNumber:1,scheme:'" + s.handle + "'})"
                    }]
                })
            });
            ssSideNav.sections.push({
                name: 'مدیریت محصولات',
                type: 'link',
                icon: 'box',
                state: "products({lang:'" + currentLang.get() + "',pageNumber:1})"
            });
            ssSideNav.sections.push({
                name: 'مدیریت سفارشات',
                type: 'link',
                icon: 'notepad',
                state: "orders({lang:'" + currentLang.get() + "',pageNumber:1})"
            });
            ssSideNav.sections.push({
                name: 'مدیریت تخفیفات',
                type: 'link',
                icon: 'discount',
                state: "discounts({lang:'" + currentLang.get() + "',pageNumber:1})"
            });
            ssSideNav.sections.push({
                name: 'مدیریت دسته بندی محصولات',
                type: 'link',
                icon: 'category-product',
                state: "productCategories({lang:'" + currentLang.get() + "'})"
            });
            ssSideNav.sections.push({
                name: 'مدیریت فهرست ها',
                type: 'link',
                icon: 'menu1',
                state: "menu({lang:'" + currentLang.get() + "'})"
            });

            ssSideNav.sections.push({
                name: 'مدیریت ابزارک ها (widgets)',
                type: 'link',
                icon: 'widget',
                state: "widgets({lang:'" + currentLang.get() + "'})"
            });
            if (identityManager.isInRole('Administrator') || identityManager.isInRole('PrincipalAdministrator')) {

                ssSideNav.sections.push({
                    name: 'مدیریت فرم ها',
                    icon: 'formSchemes',
                    type: 'toggle',
                    pages: [{
                        name: '-- +  افزودن قالب',
                        state: "addFormScheme({lang:'" + currentLang.get() + "'})",
                    }, {
                        name: '-- لیست قالب ها',
                        state: "formSchemes({lang:'" + currentLang.get() + "',pageNumber:1})",
                    }]
                });
            }
            var formRoutes = [];
            angular.forEach(formSchemes.items, function (f) {
                formRoutes.push({
                    name: f.title,
                    state: "forms({pageNumber:1,formHandle:'" + f.handle + "',lang:'" + currentLang.get() + "'})",
                });
            });
            ssSideNav.sections.push({
                name: 'پیام های دریافت شده',
                icon: 'messages',
                type: 'toggle',
                pages: formRoutes
            });
            ssSideNav.sections.push({
                name: 'نظرات',
                icon: 'comment',
                type: 'link',
                state: 'comments({lang:"' + currentLang.get()+'",pageNumber:1})'
            });
            ssSideNav.sections.push({
                name: 'تنظیمات',
                icon: 'setting',
                type: 'link',
                state: 'setting({lang:"' + currentLang.get()+'"})'
            });
        }
    }
})();