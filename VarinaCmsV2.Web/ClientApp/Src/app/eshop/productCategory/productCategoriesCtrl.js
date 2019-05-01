(function () {
    'use strict';

    angular
        .module('app')
        .controller('ProductCategoriesCtrl', ProductCategoriesCtrl);

    ProductCategoriesCtrl.$inject = [ 'categories', "$mdDialog", "$mdToast", 'lodash', 'productCategorySrv', '$timeout', '$rootScope', '$state'];
    function ProductCategoriesCtrl( categories, $mdDialog, $mdToast, lodash, productCategorySrv, $timeout, $rootScope, $state) {
        var vm = this;
        var changedItems = [];
        
        vm.categories = categories.data;
        vm.uiTreeOptions = {
            dropped: nodeDropped
        }
        vm.showAddDialog = showAddDialog;
        vm.showEditDialog = showEditDialog;
        vm.showRemoveDialog = showRemoveDialog;
        vm.updateChildParent = updateChildParent
        activate();

        ////////////////

        function activate() {
            $rootScope.title = " مدیریت دسته بندی محصولات ";
        }

        function nodeDropped(event) {
            vm.collectionChanged = true;

            if (event.dest.nodesScope.$nodeScope != null) {
                event.source.nodeScope.$modelValue
                    .parentId = event.dest.nodesScope.$nodeScope.$modelValue.id;
                changeOrderAndAddToChangedItems(event.dest.nodesScope.$nodeScope.$modelValue.childs);
            }
            else {
                event.source.nodeScope.$modelValue.parentId = null;
                changeOrderAndAddToChangedItems(event.dest.nodesScope.$modelValue);

            }
            addToChangedItems(event.source.nodeScope.$modelValue);
        }

        function changeOrderAndAddToChangedItems(childs) {
            angular.forEach(childs, function (ch, idx) {
                ch.order = idx;
                addToChangedItems(ch);
            })
        }

        function addToChangedItems(item) {
            var idx = lodash.findIndex(changedItems, function (i) { return i.id == item.id });

            var changed = { id: item.id, parentId: item.parentId, order: item.order };

            if (idx === -1) changedItems.push(changed);
            else {
                changedItems.splice(idx, 1, changed);
            }

        }
        function updateChildParent() {
            productCategorySrv.putUpdateChildParent(changedItems).then(function (res) {
                vm.collectionChanged = false;
                $mdToast.showSimple("انجام شد");
             $state.reload();
            })
        }

        function showAddDialog($event) {
            $mdDialog.show({
                templateUrl: '/Src/app/eshop/productCategory/productCategory.add-update.tpl.html',
                controller: "ProductCategoryAddCtrl",
                controllerAs: 'vm',
                targetEvent: $event,
                clickOutsideToClose: true,
                bindToController: true,
                multiple :true,
                locals: {
                    title: 'افزودن دسته بندی',
                },
                fullscreen: true
            }).then(function(newCat){
                $state.reload();
            })
        };

        function showEditDialog($event, cat) {
            $mdDialog.show({
                templateUrl: '/Src/app/eshop/productCategory/productCategory.add-update.tpl.html',
                controller: "ProductCategoryUpdateCtrl",
                controllerAs: 'vm',
                targetEvent: $event,
                clickOutsideToClose: true,
                bindToController: true,
                locals: {
                    title: 'ویرایش دسته بندی',
                    category: angular.copy(cat)
                },
                fullscreen: true
            }).then(function (editedCat) {
              $state.reload();
            },function(){
                $state.reload();
            })
        }

        function replaceInlist(cat) {
            angular.forEach(vm.categories, function (item, idx) {
                if (item.id == cat.id) vm.categories[idx] = cat;
                else if (item.childs) goToChildsFindAnReplace(item);
            });

            function goToChildsFindAnReplace(item) {
                angular.forEach(item.childs, function (ch, idx) {
                    if (ch.id == cat.id) item.childs[idx] = cat;
                    else if (ch.childs) goToChildsFindAnReplace(ch);       
                });
            }
        }

        function showRemoveDialog($event, cat) {
            var confirm = $mdDialog.confirm()
            .title('از حذف این مورد اطمینان دارید؟')
            .textContent(cat.title)
            .targetEvent($event)
            .ok('بلی')
            .cancel('خیر');

            $mdDialog.show(confirm).then(function (res) {
                productCategorySrv.delete(cat).then(function (res) {
                    if(res.status==200){
                        $mdToast.showSimple('انجام شد');
                        $state.reload();
                    }
                    if(res.status==400){
                        $mdToast.showSimple(res.data.message);
                    }
                })
            })
        }
    }
})();