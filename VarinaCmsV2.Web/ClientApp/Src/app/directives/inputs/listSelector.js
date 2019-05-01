(function () {
    'use strict';

    angular
        .module('app')
        .directive('listSelector', listSelector);

    listSelector.$inject = [];

    function listSelector() {
        // Usage:
        //
        // Creates:
        //
        var directive = {
            bindToController: true,
            controller: ListSelectorController,
            controllerAs: 'vm',
            templateUrl: '/Src/app/directives/inputs/list-slector-tpl.html',
            link: link,
            restrict: 'E',
            scope: {
                reqParams: "=",
                model: "=",
                prop: "@",
                placeHolder: '@'
            }
        };
        return directive;

        function link(scope, element, attrs) {}
    }
    ListSelectorController.$inject = ['$injector', 'currentLang', '$timeout', '$attrs', '$q', 'lodash']

    function ListSelectorController($injector, currentLang, $timeout, $attrs, $q, lodash) {
        var vm = this;
        vm.selectedItem;
        vm.searchText;
        vm.refreshItems = refreshItems;
        vm.transFormIfExists = transFormIfExists;
        activate()

        function activate() {
            $timeout(function () {
                vm.model = vm.model || [];
                vm.reqParams.languageName = currentLang.get();
                vm.dataService = $injector.get($attrs.service);
            });
        }

        function refreshItems(keyword) {
            var defered = $q.defer();
            vm.reqParams.searchText = keyword
            vm.dataService.get(vm.reqParams).then(function (res) {
                vm.items = lodash.differenceBy(res.data.items||res.data, vm.model, 'id');
              $timeout(function(){  defered.resolve(vm.items);},200)
            });
            return defered.promise;
        }

        function transFormIfExists(cat) {
            return lodash.findIndex(vm.items, cat) !== -1 ? cat : null;
        }

    }
})();