(function () {
    'use strict';

    angular
        .module('app')
        .controller('htmlWidgetCtrl', staticHtml);

    staticHtml.$inject = ['$mdDialog', 'tinyMceOptions', 'identityManager', 'codemirrorOptions'];

    function staticHtml($mdDialog, tinyMceOptions, identityManager, codemirrorOptions) {
        var vm = this;
        vm.editorOptions = tinyMceOptions;
        vm.closeDialog = closeDialog;
        vm.codemirrorOptions = {
            lineNumbers: true,
            lineWrapping: true,
            mode: 'xml',
            theme: 'material',
            autoCloseTags: true,
            styleActiveLine: true,
        };
        vm.isInRole = identityManager.isInRole;
        vm.widget = vm.widget || {
            type: 'html',
            metaData: {}
        };
        vm.ok = ok;
        activate();

        ////////////////

        function activate() {}

        function closeDialog() {
            $mdDialog.cancel();
        }

        function ok() {
            $mdDialog.hide(vm.widget); //||{metaData:{htmlContent:""}}
        }

    }
})();