(function () {
    'use strict';

    angular
        .module('app')
        .controller('CommentDetailCtrl', CommentDetailCtrl);

    CommentDetailCtrl.$inject = ['$mdDialog'];
    function CommentDetailCtrl($mdDialog) {
        var vm = this;


        activate();

        ////////////////

        function activate() {
            if (vm.comment.page != null) {
                vm.commentTargetTitle = vm.comment.page.title;
                vm.comment.target = vm.comment.page;
            }
            if (vm.comment.entity != null) {
                vm.comment.target = vm.comment.entity;
                vm.commentTargetTitle = vm.comment.entity.schemeTitle + " | " + vm.comment.entity.title;
            }
        }
    }
})();