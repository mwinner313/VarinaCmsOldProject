; (function () {
    'use strict';

    angular
        .module('app')
        .constant('fieldTypes', [
            {
                title: "محتوای پیشرفته", type: "html",
            },
            {
                title: "متن", type: "text",
            }, {
                title: "عکس", type: "image",
            }, {
                title: "رنگ", type: "color",
            }, {
                title: "تاریخ", type: "date",
            },
            {
                title: "تاریخ و زمان", type: "datetime",
            }, {
                title: "زمان", type: "time",
            },
            {
                title: "عدد", type: "number",
            },
            {
                title: "گزینه تعریف شده (dropdown)", type: "dropdown",
            },
        ]);

    angular
        .module('app')
        .constant('formFieldTypes', [

            {
                typeTitle: "متن", type: "text",
            },
            {
                typeTitle: "تاریخ و زمان", type: "datetime",
            },
            {
                typeTitle: "عدد", type: "number",
            },
            {
                typeTitle: "ایمیل", type: "email",
            },
            {
                typeTitle: "آدرس (url)", type: "url",
            },
            {
                typeTitle: "فایل", type: "file",
            }
        ]);


})();