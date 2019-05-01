(function () {
    'use strict';

    angular
        .module('app')
        .controller('fdUpdateCtrl', fdUpdateCtrl);

    fdUpdateCtrl.$inject = ['lodash', '$mdDialog'];
    function fdUpdateCtrl(lodash, $mdDialog) {
        var vm = this;
        vm.edit = edit

        activate();

        ////////////////

        function activate() {
            vm.defaultValueForFd = {rawValue:vm.fd.defaultValue};
            FixPrevieosSelectedItemInAutocompelete();
        }

        function edit() {
            vm.fieldDefenitionForm.$submitted = true;
            if (vm.fieldDefenitionForm.$invalid) return;
            vm.fd.defaultValue = vm.defaultValueForFd.rawValue;
            $mdDialog.hide(vm.fd);
        }
        
        function FixPrevieosSelectedItemInAutocompelete() {
            vm.selectedType = lodash.find(vm.fieldTypes, function (i) { return i.type == vm.fd.type });
        }

       
    }
})();