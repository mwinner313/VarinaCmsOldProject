(function () {
    'use strict';

    angular
        .module('app')
        .controller('productEditCtrl', productEditCtrl);

    productEditCtrl.$inject = ['editorSetting', 'categoryType', 'product', 'productUiHelper', 'smallScrollOpts', 'tinyMceOptions', 'schemes', '$rootScope', '$mdToast', '$state', '$window', 'productSrv', '$mdDialog'];

    function productEditCtrl(editorSetting, categoryType, product, productUiHelper, smallScrollOpts, tinyMceOptions, schemes, $rootScope, $mdToast, $state, $window, productSrv, $mdDialog) {
        var vm = this;
        vm.editorSetting = editorSetting.data;
        vm.scrollopts = smallScrollOpts;
        vm.tinyMceOptions = tinyMceOptions;
        vm.product = productUiHelper.setForEdit(product.data);
        vm.newPicture = {};
        vm.schemes = schemes.data;

        vm.secondaryCatParams = {
            checkByType: true,
            categoryType: categoryType.secondary
        }

        vm.openFilePicker = productUiHelper.openFilePicker;
        vm.openFilePickerForSampleFile = productUiHelper.openFilePickerForSampleFile;
        vm.chooseRequiredProductDialog = productUiHelper.chooseRequiredProductDialog;
        vm.chooseRelatedProductDialog = productUiHelper.chooseRelatedProductDialog;
        vm.chooseCrossSellingsProductDialog = productUiHelper.chooseCrossSellingsProductDialog
        vm.chooseAssociatedProductDialog = productUiHelper.chooseAssociatedProductDialog;
        vm.chooseEshantionsProductDialog = productUiHelper.chooseEshantionsProductDialog;
        vm.removeFromRequiredProducts = productUiHelper.removeFromRequiredProducts;
        vm.removeFromUpSellings = productUiHelper.removeFromUpSellings;
        vm.removeFromCrossSelledProducts = productUiHelper.removeFromCrossSelledProducts;
        vm.removeFromAssociatedProducts = productUiHelper.removeFromAssociatedProducts;
        vm.removeFromEshantionProducts = productUiHelper.removeFromEshantionProducts;
        vm.confirmImageDelete = productUiHelper.confirmImageDelete;

        vm.confirmCategoryChange = confirmCategoryChange;
        vm.choosePicture = choosePicture;
        vm.addProductPicture = addProductPicture;
        vm.pictureDragged = pictureDragged;
        vm.submit = submit;

        activate();

        ////////////////


        function activate() {
            $rootScope.title = 'ویرایش محصول';
            vm.title="ویرایش";

        }

        function confirmCategoryChange() {
            if (vm.product.primaryCategory.fieldDefenitions.length)
                return confirm(
                    " درصورت تغییر فیلد های دسته بندی اصلی حدف خواهند شد  (درصورت اعمال تغییرات) "
                );
            return true;
        }

        function choosePicture() {
            $mdDialog.show($mdDialog.imagePicker()).then(function (picture) {
                vm.newPicture.path = picture.path;
            })
        }

        function addProductPicture() {
            vm.newPicture.displayOrder = vm.product.pictures.length;
            vm.product.pictures.push(vm.newPicture);
            vm.newPicture = {};
        }

        function submit() {
            vm.productForm.$submitted = true;
            if (vm.product.scheme)
                vm.product.schemeId = vm.product.scheme.id;
            if (vm.product.primaryCategory)
                vm.product.primaryCategoryId = vm.product.primaryCategory.id;
            if (vm.productForm.$invalid) {
                $mdToast.showSimple("مقادیر را بررسی کنید");
                return;
            }
            vm.isSendingData = true;
            productSrv.put(vm.product).then(function (res) {
                vm.isSendingData = false;
                if (res.status !== 200) return;

                $mdToast.show('انجام شد!');
                $window.history.back();
            })
        }

        function pictureDragged(item, index) {
            vm.product.pictures.splice(index, 1);
            angular.forEach(vm.product.pictures, function (pic, idx) {
                pic.displayOrder = idx;
            })
        }

    }
})();