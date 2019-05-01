(function () {
    'use strict';

    angular
        .module('app')
        .controller('CommentCtrl', CommentCtrl);

    CommentCtrl.$inject = ['$mdDialog', 'currentLang', '$mdToast', '$q', 'comments', 'commentDataSrv', '$location', '$stateParams', '$mdBottomSheet', 'bottomSheetOptions', '$state'];
    function CommentCtrl($mdDialog, currentLang, $mdToast, $q, comments, commentDataSrv, $location, $stateParams, $mdBottomSheet, bottomSheetOptions, $state) {
        var vm = this;
        vm.comments = comments.data;
        vm.selectedComments = [];
        vm.showDetail = showDetail;
        vm.approve = approve;
        vm.tablePromise;
        vm.confirmDeleteList = confirmDeleteList;
        vm.openSearchDialog = openSearchDialog;
        vm.getComments = getComments;
        vm.query = {
            pageSize: $location.search().pageSize || 15,
            pageNumber: $stateParams.pageNumber || 1
        }
        var routePageNumber = vm.query.pageNumber;

        activate();

        ////////////////

        function activate() { }

        function approve() {
            commentDataSrv.approve(vm.selectedComments.map(function (c) { return c.id })).then(function (res) {
                $mdToast.showSimple('انجام شد !');
                angular.forEach(vm.selectedComments, function (c) {
                    c.approved = !c.approved;
                })
                vm.selectedComments = [];
            });
        }
        function confirmDeleteList($event) {
            $mdDialog.show($mdDialog
                .confirm()
                .title('از حذف این موارد مطمعنید؟')
                .targetEvent($event)
                .ok('بلی')
                .cancel('خیر')).then(function () {
                    commentDataSrv.delete(vm.selectedComments.map(function (c) { return c.id })).then(function (res) {
                        $mdToast.showSimple('انجام شد !');
                        $state.reload();
                    });
                })
        }
        function getComments(pageNumber, pageSize) {
            if (pageNumber == routePageNumber
                && pageSize == $location.search().pageSize) return;
            routePageNumber = pageNumber;

            var defered = $q.defer();
            vm.tableProgressPromise = defered.promise;
            commentDataSrv.get({
                pageSize: pageSize,
                pageNumber: pageNumber,
                languageName: currentLang,
                searchText: vm.searchText
            }).then(function (res) {
                defered.resolve(res.data);
                vm.comments = res.data;
            });
            updateRouteParams();
        }

        function openSearchDialog() {

        }
        function showDetail(comment, $event) {
            commentDataSrv.getCommentDetail(comment.id).then(function (res) {
                $mdDialog.show({
                    controller: 'CommentDetailCtrl',
                    controllerAs: 'vm',
                    bindToController: true,
                    locals: {
                        comment: res.data.comment
                    },
                    fullscreen: true,
                    templateUrl: '/Src/app/comments/commentDetail.tpl.html',
                    targetEvent: $event,
                    clickOutsideToClose: true
                });
            })
        }

        function confirmDelete() {

        }

        function search() {
            var defered = $q.defer();
            vm.tableProgressPromise = defered.promise;
            pageDataService.get({
                pageSize: vm.query.pageSize,
                languageName: currentLang,
                pageNumber: 1,
                searchText: vm.searchText
            }).then(function (res) {
                defered.resolve(res.data);
                vm.comments = res.data;
            });
            vm.query.pageNumber = 1;
            updateRouteParams();
        }

        function updateRouteParams() {
            $location.search({ searchText: vm.searchText, pageSize: vm.query.pageSize, })
            $state.go($state.current, {
                pageNumber: vm.query.pageNumber,
            }, { reload: false });
        }

    }
})();