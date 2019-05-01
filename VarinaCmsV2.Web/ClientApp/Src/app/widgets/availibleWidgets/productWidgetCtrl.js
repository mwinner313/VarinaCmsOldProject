(function () {
    'use strict';

    angular
        .module('app')
        .controller('productWidgetCtrl', productWidgetCtrl);

    productWidgetCtrl.$inject = ['$mdDialog', '$q', 'identityManager', 'lodash', 'orderOption', 'timeRange'];

    function productWidgetCtrl($mdDialog, $q, identityManager, lodash, orderOption, timeRange) {
        var vm = this;
        vm.widget = vm.widget || {
            metaData: {
                count: 10
            },
            type: 'product'
        }
        vm.selectedScheme = {
            id: vm.widget.metaData.schemeId
        }
        vm.ok = ok;
        vm.closeDialog = closeDialog;
        vm.isInRole = isInRole;
        vm.orderOption = orderOption;
        vm.timeRange = timeRange;
        activate();

        ////////////////
        function activate() {

        }

        function closeDialog() {
            $mdDialog.cancel();
        }

        function ok() {
            vm.widget.metaData.schemeId = vm.selectedScheme.id;
            vm.widget.title = ['', null, undefined].indexOf(vm.widget.title) === -1 ? vm.widget.title : 'محصولات';
            $mdDialog.hide(vm.widget);
        }

        function isInRole(role) {
            return identityManager.isInRole(role);
        }
    }
})();