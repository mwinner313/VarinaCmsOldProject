(function () {
    'use strict';

    angular
        .module('app')
        .controller('productAddCtrl', productAddCtrl);

    productAddCtrl.$inject = ['lodash', 'editorSetting', '$state', '$rootScope', 'schemes', 'categoryType', '$mdToast', 'productUiHelper', 'smallScrollOpts', 'datetimeRegex', 'productSrv', 'productCategorySrv', 'tinyMceOptions',
        '$mdDialog'
    ];

    function productAddCtrl(lodash, editorSetting, $state, $rootScope, schemes, categoryType, $mdToast, productUiHelper, smallScrollOpts, datetimeRegex, productSrv, productCategorySrv, tinyMceOptions, $mdDialog) {
        var vm = this;
        vm.scrollopts = smallScrollOpts;
        vm.tinyMceOptions = tinyMceOptions;
        vm.product = productUiHelper.createNewProductModel()
        vm.newPicture = {};
        vm.schemes = schemes.data;
        vm.editorSetting = editorSetting.data;

        vm.secondaryCatParams = {
            checkByType: true,
            categoryType: categoryType.secondary
        }
        // ui events
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
        vm.confirmCategoryChange = confirmCategoryChange;
        vm.choosePicture = choosePicture;
        vm.addProductPicture = addProductPicture;
        vm.confirmImageDelete = productUiHelper.confirmImageDelete;
        vm.pictureDragged = pictureDragged;
        vm.submit = submit;
        vm.getCreatedIndex = getCreatedIndex;
        activate();

        ////////////////

        function activate() {
            $rootScope.title = 'افزودن محصول';
            vm.title="افزودن";
        }

        var indexes = [];
        var counter = 0

        function getCreatedIndex(fd) {
            var indexed = lodash.find(indexes, function (idx) {
                return idx.id == fd.id
            });
            if (indexed) return indexed.index;
            indexes.push({
                id: fd.id,
                index: counter
            });
            return counter++;
        }

        function confirmCategoryChange() {
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

            productSrv.post(vm.product).then(function (res) {
            vm.isSendingData = false;
                
                if (res.status !== 200) return;
                $mdToast.show('انجام شد!');
                $state.go('products', {
                    lang: $rootScope.currentLang,
                    pageNumber: 1
                })
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