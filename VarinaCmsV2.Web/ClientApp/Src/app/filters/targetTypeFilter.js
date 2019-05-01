(function() {
    'use strict';

    angular
        .module('app')
        .filter('targetType', targetType);

    function targetType() {
        return targetTypeFilter;

        ////////////////

        function targetTypeFilter(type) {
          switch (type){
              case 1:return "لینک سفارشی";
              case 2:return "پست";
              case 3:return "صفحه ثابت";
              case 4:return "پروفایل کاربر";
              case 5:return "محصول";
              case 6:return "دسته بندی";
          }
        //   customLink:1,
        //   entity:2,
        //   page:3,
        //   userProfile:4,
        //   product:5,
        }
    }
})();