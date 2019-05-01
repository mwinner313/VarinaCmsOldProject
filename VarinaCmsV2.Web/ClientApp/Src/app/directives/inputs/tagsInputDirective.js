(function () {
    'use strict';

    angular
        .module('app')
        .directive('tagInput', tagInput);

    tagInput.$inject = [];
    function tagInput() {
        // Usage:
        //
        // Creates:
        //
        var directive = {
            bindToController: true,
            controller: Controller,
            controllerAs: 'vm',
            templateUrl: "/Src/app/directives/inputs/tagTemplate.html",
            link: link,
            restrict: 'E',
            scope: {
                model: '=',
                scheme: "="
            }
        };
        return directive;

        function link(scope, element, attrs) {
        }
    }
    Controller.$inject = ['tagDataService', '$q', '$timeout', 'lodash', 'currentLang']
    /* @ngInject */
    function Controller(tagDataService, $q, $timeout, lodash, currentLang) {

        var vm = this;
        var schemeId;
        vm.tagTransform = tagTransform;
        vm.refreshTags = refreshTags;
        $timeout(function () {
            vm.model = vm.model || [];
            if (vm.scheme) schemeId = vm.scheme.id;
        })

        function refreshTags(keyword) {
            var defered = $q.defer();
            if (keyword)
                tagDataService.get({ searchText: keyword, schemeId: schemeId }).then(function (res) {
                    vm.items = res.data;
                    vm.items.push(vm.model);
                    defered.resolve(res.data);
                });
            return defered.promise;
        }

        function tagTransform(newTagTitle) {
            var newTag = newTagTitle;
            if (typeof newTagTitle === typeof "") {
                newTag = lodash.find(vm.items, function (t) { return t.title == newTagTitle });
            }
            if (!newTag) {
                newTag = {
                    entitySchemeId: schemeId,
                    title: newTagTitle,
                    languageName: currentLang.get()
                }
            }
            return newTag;
        }
    }
})();