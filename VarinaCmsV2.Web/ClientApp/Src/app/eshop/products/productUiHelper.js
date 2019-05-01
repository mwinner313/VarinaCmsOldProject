(function () {
    'use strict';

    angular
        .module('app')
        .service('productUiHelper', prductUiHelper);

    prductUiHelper.$inject = ['lodash', '$mdDialog', 'productSrv'];

    function prductUiHelper(lodash, $mdDialog, productSrv) {
        var _product;
        var productPickerDialogOptions = {
            templateUrl: '/Src/app/eshop/products/productPicker.tpl.html',
            controller: 'productPickerCtrl',
            controllerAs: 'vm',
            fullscreen: true,
            clickOutsideToClose: true
        }
        var service = {
            createNewProductModel: createNewProductModel,
            setForEdit:setForEdit,
            openFilePicker: openFilePicker,
            openFilePickerForSampleFile: openFilePickerForSampleFile,
            chooseRequiredProductDialog: chooseRequiredProductDialog,
            chooseCrossSellingsProductDialog: chooseCrossSellingsProductDialog,
            chooseEshantionsProductDialog:chooseEshantionsProductDialog,
            chooseRelatedProductDialog: chooseRelatedProductDialog,
            chooseAssociatedProductDialog: chooseAssociatedProductDialog,
            removeFromRequiredProducts: removeFromRequiredProducts,
            removeFromUpSellings: removeFromUpSellings,
            removeFromCrossSelledProducts: removeFromCrossSelledProducts,
            removeFromAssociatedProducts: removeFromAssociatedProducts,
            removeFromEshantionProducts:removeFromEshantionProducts, 
            confirmImageDelete:confirmImageDelete, };

        return service;

        ////////////////
        function createNewProductModel() {
            _product = {
                visibleIndividually: true,
                allowCustomerReviews: true,
                canAddToCart: true,
                relatedProductLoadType: 5,
                type: 5,
                isVisible: true,
                shipment: {
                    isShipEnabled:true
                },
                inventory: {
                    orderMaximumQuantity: 1000,
                    orderMinimumQuantity: 1,
                    minStockQuantity: 0,
                    notifyAdminForQuantityBelow: 0,
                    trackMethod:5
                },
                crossSellings: [],
                requiredProducts: [],
                upSellings: [],
                associatedProducts: [],
                tags: [],
                pictures:[],
                fields:[],
                relatedCategories:[],
                eshantions:[],
                price:0,
                
            };
            return _product;
        }
        function setForEdit(product){
            _product=product;
            return _product;
        }

        function openFilePicker() {
            $mdDialog.show($mdDialog.filePicker()).then(function (file) {
                _product.file = file;
                _product.fileId = file.id;
            });
        }

        function openFilePickerForSampleFile() {
            $mdDialog.show($mdDialog.filePicker()).then(function (file) {
                _product.sampleFile = file;
                _product.sampleFileId = file.id;
            });
        }


        function chooseRequiredProductDialog() {
            $mdDialog.show(productPickerDialogOptions).then(function (products) {
                var news = lodash.differenceBy(products, _product.requiredProducts, function (p) {
                    return p.id
                })
                Array.prototype.push.apply(_product.requiredProducts, news);
            });
        }

        function chooseCrossSellingsProductDialog() {
            $mdDialog.show(productPickerDialogOptions).then(function (products) {
                var news = lodash.differenceBy(products, _product.crossSellings, function (p) {
                    return p.id
                })
                Array.prototype.push.apply(_product.crossSellings, news);
            });
        }
        function chooseEshantionsProductDialog() {
            $mdDialog.show(productPickerDialogOptions).then(function (products) {
                var news = lodash.differenceBy(products, _product.eshantions, function (p) {
                    return p.id
                })
                Array.prototype.push.apply(_product.eshantions, news);
            });
        }

        function chooseAssociatedProductDialog() {
            $mdDialog.show(productPickerDialogOptions).then(function (products) {
                var news = lodash.differenceBy(products, _product.associatedProducts, function (p) {
                    return p.id
                })
                Array.prototype.push.apply(_product.associatedProducts, news);
            });
        }

        function chooseRelatedProductDialog() {
            $mdDialog.show(productPickerDialogOptions).then(function (products) {
                var news = lodash.differenceBy(products, _product.upSellings, function (p) {
                    return p.id
                })
                Array.prototype.push.apply(_product.upSellings, news);
            });
        }

        function removeFromRequiredProducts(product, $event) {
            $mdDialog.show(getDeleteProductDialogOpts(product, $event)).then(function () {
                _product.requiredProducts.splice(_product.requiredProducts.indexOf(product), 1)
            });
        }

        //
        function removeFromUpSellings(product, $event) {
            $mdDialog.show(getDeleteProductDialogOpts(product, $event)).then(function () {
                _product.upSellings.splice(_product.upSellings.indexOf(product), 1)
            });
        }

        function removeFromCrossSelledProducts(product, $event) {
            $mdDialog.show(getDeleteProductDialogOpts(product, $event)).then(function () {
                _product.crossSellings.splice(_product.crossSellings.indexOf(product), 1)
            });
        }

        function removeFromAssociatedProducts(product, $event) {
            $mdDialog.show(getDeleteProductDialogOpts(product, $event)).then(function () {
                _product.associatedProducts.splice(_product.associatedProducts.indexOf(product), 1)
            });
        }
        function removeFromEshantionProducts(product, $event) {
            $mdDialog.show(getDeleteProductDialogOpts(product, $event)).then(function () {
                _product.eshantions.splice(_product.eshantions.indexOf(product), 1)
            });
        }

        function getDeleteProductDialogOpts(product, $event) {
            return $mdDialog.confirm()
                .title("حذف شود؟")
                .textContent(product.title)
                .targetEvent($event)
                .ok('بلی')
                .cancel('خیر')
        }

        function confirmImageDelete(pic,$event){
            var confirm = $mdDialog.confirm()
            .title('حذف شود؟')
            .textContent((pic.titleAttribute||pic.path))
            .targetEvent($event)
            .ok('بلی')
            .cancel('خیر');
            $mdDialog.show(confirm).then(function () {
                _product.pictures.splice(_product.pictures.indexOf(pic),1)
            });
        }
    
    }
})();