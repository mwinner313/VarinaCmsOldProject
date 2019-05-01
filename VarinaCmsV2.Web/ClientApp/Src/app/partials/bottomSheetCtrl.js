; (function () {
    'use strict';

    angular
        .module('app')
        .controller('bottomSheetCtrl', schemeBottomSheetCtrl);

    schemeBottomSheetCtrl.$inject = ['$mdBottomSheet'];
    function schemeBottomSheetCtrl($mdBottomSheet) {
        var vm = this;
        vm.remove = remove;
        vm.edit = edit;
        vm.show = show;
        activate();

        ////////////////

        function activate() { }
        function remove() {
            $mdBottomSheet.hide('delete')
        }
        function edit() {
            $mdBottomSheet.hide('edit')
        }
        function show() {
            $mdBottomSheet.hide('show')
        }


    }
})();