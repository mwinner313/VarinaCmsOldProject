; (function () {
    'use strict';
    angular
        .module('app')
        .constant('api',
        {
            currentUser: "/api/currentUser",
            page: "/api/page",
            tag: "/api/tag",
            entity: "/api/entity",
            scheme: '/api/entityScheme',
            fieldDefenition: '/api/fieldDefenition',
            language: '/api/language',
            account: '/api/account',
            user: '/api/userManagement',
            category: '/api/category',
            menu: '/api/menu',
            widget: '/api/widget',
            widgetContainer: '/api/widgetContainer',
            formScheme: '/api/formScheme',
            form: '/api/form',
            file: '/api/file',
            comment: '/api/comment',
            product:'/api/product',
            productCategory:'/api/productCategory',
            order:'/api/order',
            discount:'/api/discount',
            setting:'/api/setting',
            dashboard:'/api/dashboard',
            role:'/api/roleManagement'
            
        });
})();