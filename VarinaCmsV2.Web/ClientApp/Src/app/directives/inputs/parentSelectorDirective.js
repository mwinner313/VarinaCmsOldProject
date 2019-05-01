(function () {
    'use strict';

    angular
        .module('app')
        .directive('parentSelector', parentSelector);

    parentSelector.$inject = [];

    function parentSelector() {

        var directive = {
            bindToController: true,
            controller: Controller,
            controllerAs: 'vm',
            templateUrl: "/Src/app/directives/inputs/parentSelcetorDirective.tpl.html",
            link: link,
            restrict: 'E',
            scope: {
                model: '=',
                inputPlaceholder: "@",
                inputTitle: "@",
                service: "@",
                name: "@",
                params: '=',
            }
        };
        return directive;

        function link(scope, element, attrs) {

        }
    }
    Controller.$inject = ['$injector', '$attrs', 'currentLang', '$scope', "$timeout", '$q']
    /* @ngInject */
    function Controller($injector, $attrs, currentLang, $scope, $timeout, $q) {

        var vm = this;
        vm.selected;
        vm.items = [];
        vm.dataService = $injector.get($attrs.service);
        vm.getItems = getItems;
        vm.setParent = setParent;
        vm.removeParentIfTextIsEmpty = removeParentIfTextIsEmpty;

        function activate() {
            $timeout(function () {
                if (vm.model.parentId) {
                    var parent = {
                        title: vm.model.parentTitle,
                        id: vm.model.parentId,
                        displayName:vm.model.parentTitle
                    }
                    vm.items = [parent];
                    vm.selected = parent;
                }
            });
        }
        activate();

        function getItems(keyword) {
            var defered = $q.defer()
            var params = {
                searchText: keyword,
                languageName: currentLang.get()
            }
            angular.extend(params, vm.params);
            vm.dataService.get(params).then(function (res) {
                if (res.data.length !== undefined)
                    vm.items = removeModelItSelf(removeChilds(res.data));
                else
                    vm.items = removeModelItSelf(removeChilds(res.data.items));
                defered.resolve(vm.items);
            });
            return defered.promise;
        }

        function setParent() {
            vm.model.parentTitle = vm.selected.title;
            vm.model.parentId = vm.selected.id;
        }



        function removeParentIfTextIsEmpty() {
            if (!vm.searchText) {
                vm.model.parentId = undefined;
                vm.model.parentTitle = undefined;
            }
        }

        function removeModelItSelf(items) {
            if (!vm.model.id) return items;
            angular.forEach(items, function (item, idx) {
                if (item.id == vm.model.id) items.splice(idx, 1);
            });
            return items;
        }

        function removeChilds(items) {
            if (!vm.model.id) return items;
            angular.forEach(items, function (item, idx) {
                if (item.parentId == vm.model.id) items.splice(idx, 1);
            });
            return items;
        }
    }
})();