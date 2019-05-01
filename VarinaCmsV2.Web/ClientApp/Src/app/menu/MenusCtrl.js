(function () {
    'use strict';

    angular
        .module('app')
        .controller('MenusCtrl', MenusCtrl);

    MenusCtrl.$inject = ['guid', 'entityDataService','productCategories', 'categoryDataService', 'userDataSrv', 'pageDataService', 'menus', 'menuDataSrv', '$rootScope', 'pages', 'categories', 'userProfiles', 'entities', 'lodash', 'menuItemTargetType', '$mdDialog', '$mdToast', 'arrayFlatter', 'currentLang', '$mdExpansionPanel'];

    function MenusCtrl(guid, entityDataService,productCategories, categoryDataService, userDataSrv, pageDataService, menus, menuDataSrv, $rootScope, pages, categories, userProfiles, entities, lodash, menuItemTargetType, $mdDialog, $mdToast, arrayFlatter, currentLang, $mdExpansionPanel) {
        var vm = this;
        vm.uiTreeOptions = {
            dropped: nodeDropped
        }

        var productPickerDialogOptions = {
            templateUrl: '/Src/app/eshop/products/productPicker.tpl.html',
            controller: 'productPickerCtrl',
            controllerAs: 'vm',
            fullscreen: true,
            clickOutsideToClose: true
        }

        vm.menuItemTargetType = menuItemTargetType;
        vm.menus = menus.data;
        vm.pages = pages.data.items;
        vm.entities = entities.data.entities;
        vm.categories = categories.data;
        vm.userProfiles = userProfiles.data.items;
        vm.productCategories = productCategories.data;
        vm.customLink = {
            url: 'http://'
        };

        vm.selectedMenu;
        vm.selectedPages = [];
        vm.selectedEntities = [];
        vm.selectedUserProfiles = [];
        vm.selectedProducts = [];
        vm.selectedCategories = [];
        vm.selectedProductCategories = [];

        vm.changeSelectedMenu = changeSelectedMenu;
        vm.addSelectedPagesToMenu = addSelectedPagesToMenu;
        vm.addSelectedEntitiesToMenu = addSelectedEntitiesToMenu;
        vm.addSelectedUserProfilesToMenu = addSelectedUserProfilesToMenu;
        vm.chooseFromProductPicker = chooseFromProductPicker;
        vm.addSelectedCategoriesToMenu = addSelectedCategoriesToMenu;
        vm.addCustomLinkToMenu = addCustomLinkToMenu;
        vm.addSelectedProductCategoriesToMenu = addSelectedProductCategoriesToMenu;

        vm.searchForPages = searchForPages;
        vm.searchForUserProfiles = searchForUserProfiles;
        vm.searchForCategories = searchForCategories;
        vm.searchForEntities = searchForEntities;

        vm.removeMenu = removeMenu;
        vm.removeMenuItem = removeMenuItem;
        vm.addMenu = addMenu;
        vm.saveMenu = saveMenu;
        vm.saveNewMenu = saveNewMenu;

        activate();

        ////////////////

        function activate() {
            $rootScope.title = "مدیریت فهرست ها";
        }


        function changeSelectedMenu(menu) {
            if (vm.selectedMenu && !vm.selectedMenu.id) {
                vm.menus.splice(0, 1);
                vm.unSavedNewMenuExists = false;
            }
            vm.selectedMenu = menu;
        }

        function addSelectedProductCategoriesToMenu() {
            var newItems = [];
            angular.forEach(lodash.uniqBy(vm.selectedProductCategories, function (i) {
                return i.id
            }), function (item, itemIdx) {
                if (existsInMenu(item, menuItemTargetType.productCategory)) return;
                newItems.push({
                    title: item.title,
                    targetId: item.id,
                    targetType: menuItemTargetType.productCategory,
                    childs: [],
                    languageName: currentLang.get(),
                    menuId: vm.selectedMenu.id,
                    toFullUrl: item.toFullUrl
                });
            });
            if (newItems.length)
                menuDataSrv.addMenuItems(newItems).then(function (res) {
                    vm.selectedMenu.menuItems.push.apply(vm.selectedMenu.menuItems, res.data);
                    vm.selectedProductCategories = [];
                    $mdToast.showSimple('انجام شد!');
                })
        }

        function addSelectedPagesToMenu() {
            var newItems = [];
            angular.forEach(lodash.uniqBy(vm.selectedPages, function (i) {
                return i.id
            }), function (item, itemIdx) {
                if (existsInMenu(item, menuItemTargetType.page)) return;
                newItems.push({
                    title: item.title,
                    targetId: item.id,
                    targetType: menuItemTargetType.page,
                    childs: [],
                    languageName: currentLang.get(),
                    menuId: vm.selectedMenu.id,
                    toFullUrl: item.toFullUrl
                });
            });
            if (newItems.length)
                menuDataSrv.addMenuItems(newItems).then(function (res) {
                    vm.selectedMenu.menuItems.push.apply(vm.selectedMenu.menuItems, res.data);
                    vm.selectedPages = [];
                    $mdToast.showSimple('انجام شد!');
                })
        }

        function addSelectedEntitiesToMenu() {
            var newItems = [];
            angular.forEach(lodash.uniqBy(vm.selectedEntities, function (i) {
                return i.id
            }), function (item, itemIdx) {
                if (existsInMenu(item, menuItemTargetType.entity)) return;
                newItems.push({
                    title: item.title,
                    targetId: item.id,
                    targetType: menuItemTargetType.entity,
                    childs: [],
                    languageName: currentLang.get(),
                    menuId: vm.selectedMenu.id,
                    toFullUrl: item.toFullUrl
                });
            });
            if (newItems.length)
                menuDataSrv.addMenuItems(newItems).then(function (res) {
                    vm.selectedMenu.menuItems.push.apply(vm.selectedMenu.menuItems, res.data);
                    vm.selectedEntities = [];
                    $mdToast.showSimple('انجام شد!');
                })
        }

        function addSelectedCategoriesToMenu() {
            var newItems = [];
            angular.forEach(lodash.uniqBy(vm.selectedCategories, function (i) {
                return i.id
            }), function (item, itemIdx) {
                if (existsInMenu(item, menuItemTargetType.category)) return;
                newItems.push({
                    title: item.title,
                    targetId: item.id,
                    targetType: menuItemTargetType.category,
                    menuId: vm.selectedMenu.id,
                    childs: [],
                    languageName: currentLang.get(),
                    toFullUrl: item.toFullUrl
                });
            });
            if (newItems.length)
                menuDataSrv.addMenuItems(newItems).then(function (res) {
                    vm.selectedMenu.menuItems.push.apply(vm.selectedMenu.menuItems, res.data);
                    $mdToast.showSimple('انجام شد!');
                    vm.selectedCategories = [];
                });
        }

        function addSelectedUserProfilesToMenu() {
            var newItems = [];
            angular.forEach(lodash.uniqBy(vm.selectedUserProfiles, function (i) {
                return i.id
            }), function (item, itemIdx) {
                if (existsInMenu(item, menuItemTargetType.userProfile)) return;
                newItems.push({
                    title: (item.name || item.userName),
                    targetId: item.id,
                    targetType: menuItemTargetType.userProfile,
                    childs: [],
                    languageName: currentLang.get(),
                    toFullUrl: item.toFullUrl,
                    menuId: vm.selectedMenu.id
                });
            });
            if (newItems.length)
                menuDataSrv.addMenuItems(newItems).then(function (res) {
                    vm.selectedMenu.menuItems.push.apply(vm.selectedMenu.menuItems, res.data);
                    vm.selectedUserProfiles = [];
                    $mdToast.showSimple('انجام شد!');
                });
        }

        function addCustomLinkToMenu() {
            menuDataSrv.addMenuItems([{
                title: vm.customLink.title,
                targetType: menuItemTargetType.customLink,
                menuId: vm.selectedMenu.id,
                languageName: currentLang.get(),
                url: vm.customLink.url
            }]).then(function (res) {
                vm.selectedMenu.menuItems.push.apply(vm.selectedMenu.menuItems, res.data);
                vm.customLink.url = "http://";
                vm.customLink.title = "";
                vm.customLinkForm.$setUntouched();
                $mdToast.showSimple('انجام شد!');
            });
        }

        function chooseFromProductPicker() {
            $mdDialog.show(productPickerDialogOptions).then(function (products) {
                var newItems = [];
                angular.forEach(lodash.uniqBy(products, function (i) {
                    return i.id
                }), function (item, itemIdx) {
                    if (existsInMenu(item, menuItemTargetType.product)) return;
                    newItems.push({
                        title: (item.title),
                        targetId: item.id,
                        targetType: menuItemTargetType.product,
                        childs: [],
                        languageName: currentLang.get(),
                        toFullUrl: item.toFullUrl,
                        menuId: vm.selectedMenu.id
                    });
                });
                if (newItems.length)
                    menuDataSrv.addMenuItems(newItems).then(function (res) {
                        vm.selectedMenu.menuItems.push.apply(vm.selectedMenu.menuItems, res.data);
                        $mdToast.showSimple('انجام شد!');
                    });
            });
        }

        function searchForPages(searchText) {
            pageDataService.get({
                languageName: currentLang.get(),
                searchText: searchText
            }).then(function (res) {
                vm.searchedPages = res.data.items;
            });
        }

        function searchForUserProfiles(searchText) {
            userDataSrv.get({
                searchText: searchText
            }).then(function (res) {
                vm.searchedUserProfiles = res.data.items;
            });
        }

        function searchForCategories(searchText) {
            categoryDataService.get({
                searchText: searchText,
                languageName: currentLang.get(),
                mapForTreeView: false
            }).then(function (res) {
                vm.searchedCategories = res.data;
            });
        }

        function searchForEntities(searchText) {
            entityDataService.get({
                languageName: currentLang.get(),
                searchText: searchText
            }).then(function (res) {
                vm.searchedEntities = res.data.entities;
            });
        }

        function addMenu() {
            var newMenu = {
                index: guid.new(),
                menuItems: []
            };
            vm.menus.splice(0, 0, newMenu);
            $mdExpansionPanel().waitFor('menu' + newMenu.index).then(function (instance) {
                instance.expand();
            });
            vm.selectedMenu = newMenu;
            vm.unSavedNewMenuExists = true;
        }

        function saveNewMenu() {
            vm.savingNewMenu = true;
            vm.selectedMenu.languageName = currentLang.get();
            menuDataSrv.post(vm.selectedMenu).then(function (res) {
                vm.selectedMenu.id = res.data.id;
                vm.unSavedNewMenuExists = false;
                vm.savingNewMenu = false;
            });
        }

        function saveMenu() {
            var data = {
                menuItems: arrayFlatter.flat(angular.copy(vm.selectedMenu.menuItems), 'childs'),
                id: vm.selectedMenu.id,
                title: vm.selectedMenu.title,
                languageName: vm.selectedMenu.languageName
            }

            menuDataSrv.put(data).then(function (res) {
                $mdToast.showSimple('انجام شد');
            });
        }

        function removeMenu(menu) {
            var confirm = $mdDialog
                .confirm()
                .title('از حذف این فهرست اطمینان دارید؟')
                .textContent(menu.title)
                .ok('بلی')
                .cancel('خیر');

            $mdDialog.show(confirm).then(function () {
                menuDataSrv.delete(menu.id).then(function (res) {
                    lodash.remove(vm.menus, function (m) {
                        return m.id == menu.id;
                    })
                    $mdToast.showSimple('انجام شد');
                });
            });
        }

        function removeMenuItem(item, itemEl) {
            var confirm = $mdDialog
                .confirm()
                .title('از حذف این مورد اطمینان دارید؟ تازمانی که دکمه ذخیره تغییرات را نزنید حذف واقعی انجام نمیشود.')
                .textContent(item.title)
                .ok('بلی')
                .cancel('خیر');

            $mdDialog.show(confirm).then(function () {
                itemEl.remove();
            });
        }

        function existsInMenu(finding, targetType) {
            var exists = false;
            angular.forEach(vm.selectedMenu.menuItems, function (item) {
                if (exists) return;
                exists = (item.targetId === finding.id && item.targetType === targetType) || existInChilds(item, finding, targetType)
            });
            return exists;

            function existInChilds(item, finding, targetType) {
                var exists = false;
                angular.forEach(item.childs, function (ch) {
                    if (exists) return;
                    exists = (ch.targetId === finding.id && ch.targetType === targetType) ||
                        existInChilds(ch, finding, targetType);
                });
                return exists;
            }
        }

        function nodeDropped(event) {
            if (event.dest.nodesScope.$nodeScope != null) {
                event.source.nodeScope.$modelValue
                    .parentId = event.dest.nodesScope.$nodeScope.$modelValue.id;
                changeOrder(event.dest.nodesScope.$nodeScope.$modelValue.childs);
            } else {
                event.source.nodeScope.$modelValue.parentId = null;
                changeOrder(event.dest.nodesScope.$modelValue);
            }
        }

        function changeOrder(childs) {
            angular.forEach(childs, function (ch, idx) {
                ch.order = idx;
            })
        }


    }
})();