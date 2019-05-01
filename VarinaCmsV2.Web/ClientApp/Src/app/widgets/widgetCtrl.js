(function () {
    'use strict';

    angular
        .module('app')
        .controller('widgetCtrl', widgetCtrl);

    widgetCtrl.$inject = ['$mdToast', 'widgetContainerDataSrv', '$timeout', '$mdExpansionPanel', '$mdDialog', 'guid', 'lodash', 'widgets', 'widgetDataSrv', 'permalinkFilter', 'currentLang','$rootScope'];
    function widgetCtrl($mdToast, widgetContainerDataSrv, $timeout, $mdExpansionPanel, $mdDialog, guid, lodash, widgets, widgetDataSrv, permalinkFilter, currentLang,$rootScope) {
        var vm = this;
        vm.widgets = {
            availibleWidgets: angular.copy(widgets.data.availibleWidgets),
            widgetContainers: widgets.data.widgetContainers
        }
        vm.selectedCnt;
        vm.showRemoveWidgetDialog = showRemoveWidgetDialog;
        vm.showEditWidgetDialog = showEditWidgetDialog;
        vm.addWidgetContainer = addWidgetContainer;
        vm.resetAvailibleWidgets = resetAvailibleWidgets;
        vm.changeSelectedCnt = changeSelectedCnt;
        vm.ShowWidgetForm = ShowWidgetForm;
        vm.saveNewOrder = saveNewOrder;
        vm.saveNewCnt = saveNewCnt;
        vm.removeContainer = removeContainer;

        activate();

        ////////////////

        function activate() { 
            $rootScope.title='مدیریت ابزارک ها';
        }

        function saveNewCnt() {
            widgetContainerDataSrv.post(vm.selectedCnt).then(function (res) {
                vm.selectedCnt.id = res.data.id;
                vm.unSavedWidgetContainerExists = false;
                $mdToast.showSimple('انجام شد !');
            });
        }

        function removeContainer() {
            var confirm = $mdDialog
            .confirm()
            .title('از حذف این مورد اطمینان دارید')
            .textContent(vm.selectedCnt.title)
            .ok('بلی')
            .cancel('خیر');

            $mdDialog.show(confirm).then(function (res) {
                widgetContainerDataSrv.delete(vm.selectedCnt.id).then(function (res) {
                    var idx = vm.widgets.widgetContainers.indexOf(vm.selectedCnt);
                    vm.widgets.widgetContainers.splice(idx, 1);
                    $mdToast.showSimple('انجام شد !');
                });
            });
        }

        function resetAvailibleWidgets() {
            vm.widgets.availibleWidgets = angular.copy(widgets.data.availibleWidgets);
        }

        function saveNewOrder() {
            angular.forEach(vm.selectedCnt.widgets, function (widget, idx) {
                vm.selectedCnt.widgets[idx].order = idx;
            });

            widgetContainerDataSrv.put(vm.selectedCnt).then(function (res) {
                $mdToast.showSimple('انجام شد !');
                vm.collectionChanged = false;
            });
        }
        function addWidgetContainer() {
            vm.unSavedWidgetContainerExists = true;
            var newCnt = { widgets: [], title: '', index: guid.new() };
            vm.widgets.widgetContainers.splice(0, 0, newCnt);
            vm.selectedCnt = newCnt;
            $mdExpansionPanel().waitFor('cnt' + newCnt.index).then(function (instance) {
                instance.expand();
            });
        }

        function changeSelectedCnt(cnt) {
            if (vm.unSavedWidgetContainerExists) {
                vm.unSavedWidgetContainerExists = false;
                vm.widgets.widgetContainers.splice(0, 1);
            }
            vm.selectedCnt = cnt;
        }

        function ShowWidgetForm(event, widget, index) {
            if (!widget.id) {
                $mdDialog.show({
                    templateUrl: '/Src/app/widgets/availibleWidgets/' + widget.type + 'Widget.tpl.html',
                    controller: widget.type + 'WidgetCtrl',
                    controllerAs: 'vm',
                    clickOutsideToClose: true,
                    bindToController: true,
                    fullscreen: true,
                }).then(function (newWidget) {
                    angular.extend(newWidget,{
                        languageName: currentLang.get(),containerId : vm.selectedCnt.id
                    })
                    if (["", null, undefined].indexOf(newWidget.handle) !== -1)
                        newWidget.handle = permalinkFilter(newWidget.title);
                    widgetDataSrv.post(newWidget).then(function (res) {
                        vm.selectedCnt.widgets.splice(index, 0, res.data);
                        saveNewOrder();
                    });
                });
                return true;
            } else {
                vm.collectionChanged = true;
                return widget;
            }
        }

        function showRemoveWidgetDialog(widget) {
            var confirm = $mdDialog
                .confirm()
                .title('از حذف این ابزارک اطمینان دارید')
                .textContent(widget.title)
                .ok('بلی')
                .cancel('خیر');
            $mdDialog.show(confirm).then(function (res) {
                widgetDataSrv.delete(widget.id).then(function (serverRes) {
                    vm.selectedCnt.widgets.splice(vm.selectedCnt.widgets.indexOf(widget), 1);
                    $mdToast.showSimple('انجام شد !');
                });
            });
        }

        function showEditWidgetDialog(widget) {
            $mdDialog.show({
                templateUrl: '/Src/app/widgets/availibleWidgets/' + widget.type + 'Widget.tpl.html',
                controller: widget.type + 'WidgetCtrl',
                controllerAs: 'vm',
                clickOutsideToClose: true,
                bindToController: true,
                fullscreen: true,
                locals: {
                    widget: widget
                }
            }).then(function (edited) {
                widgetDataSrv.put(edited).then(function (res) {
                    vm.selectedCnt.widgets.splice(vm.selectedCnt.widgets.indexOf(widget), 1, res.data);
                    $mdToast.showSimple('انجام شد !');
                });
            });
        }
    }
})();