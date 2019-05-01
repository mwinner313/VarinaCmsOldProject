(function () {
    'use strict';

    angular
        .module('app')
        .component('webSiteUserRegisterChart', {
            templateUrl: '/Src/app/dashboard/components/WebSiteUserRegisterChart.tpl.html',
            controller: ControllerController,
            controllerAs: 'vm',
            bindings: {
                widgetDatas: "<"
            },
        });

    ControllerController.$inject = ['lodash'];

    function ControllerController(lodash) {
        var vm = this;
        vm.series = ['Series A'];
        vm.datasetOverride = [{
            label: "تعداد ثبت نامی",
            fill: true,
            backgroundColor: "rgb(106, 252, 127)",
            pointBorderColor: "rgba(75,192,192,1)",
        }];
        vm.options = {
            title: {
                display: true,
                fontSize: 16,
                fontFamily: "'IRANSansUlt'"
                
            },
            scales: {
                yAxes: [{
                    ticks: {
                        beginAtZero: false
                    }
                }],
                
            },
            label: {
                font: {
                    family: "IRANSansUlt"
                }
            }
        };
        vm.changeChartMode = changeChartMode;
        vm.onClick = onClick
        
        
        ////////////////

        vm.$onInit = function () {
            vm.data = lodash.find(vm.widgetDatas, function (widgetData) {
                return widgetData.name === "WebSiteUserRegisterChart";
            }).data;
            changeChartMode(Object.keys(vm.data)[0])
        };

        function changeChartMode(mode) {
            vm.labels = [];
            vm.chartData = [[]];
            vm.selectedChartMode = mode;
            angular.forEach(vm.data[mode], function (x) {
                vm.labels.push(x.range)
                vm.chartData[0].push(x.count)
            });
        }
        function onClick(points, evt) {

        };
    }
})();