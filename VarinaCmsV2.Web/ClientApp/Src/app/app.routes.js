;
(function () {
    'use strict';

    angular.module('app').config(configureRoutes);

    configureRoutes.$inject = ["$stateProvider", "$urlRouterProvider", "$locationProvider"]

    function configureRoutes($stateProvider, $urlRouterProvider, $locationProvider) {
        registerEntitySchemeRoutes();
        registerEntityRoutes();
        registerPageRoutes();
        registerEntityCategoryRoutes();
        registerUserManagementRoutes();
        registerMenuRoutes();
        registerWidgetRoutes();
        registerFormSchemeRoutes();
        registerFormRoutes();
        registerUserProfileRoutes();
        registerCommentRoutes()
        registerProductsRoutes();
        registerOrdersRoutes();
        registerDiscountsRoutes();
        registerDashBoardRoute();
        registerSettingRoute();

        function registerSettingRoute() {
            $stateProvider.state('setting', {
                url: '/:lang/setting',
                templateUrl: "/Src/app/setting/setting.tpl.html",
                controller: 'SettingCtrl',
                controllerAs: 'vm',
                resolve: {
                    settings: ['settingSrv', function (settingSrv) {
                        return settingSrv.get();
                    }]
                }
            });
        }

        function registerDashBoardRoute() {
            $stateProvider.state('dashboard', {
                url: '/:lang/dashboard',
                templateUrl: "/Src/app/dashboard/dashboard.tpl.html",
                controller: 'DashboardCtrl',
                controllerAs: 'vm',
                resolve: {
                    widgetsData: ['dashboardSrv', function (dashboardSrv) {
                        return dashboardSrv.get();
                    }]
                }
            });
        }

        function registerDiscountsRoutes() {
            $stateProvider.state("discounts", {
                url: "/:lang/eshop/discounts/{pageNumber:int}",
                params: {
                    pageNumber: {
                        dynamic: true
                    }
                },
                templateUrl: "/Src/app/eshop/discounts/discountsList.tpl.html",
                controllerAs: 'vm',
                controller: 'DiscountListCtrl',
                resolve: {
                    dicsounts: ["$stateParams", "discountSrv", '$location', function ($stateParams, discountSrv, $location) {
                        var reqParams = {
                            pageNumber: $stateParams.pageNumber
                        };
                        angular.extend(reqParams, $location.search());
                        return discountSrv.get(reqParams);
                    }],
                },
            }).state('discountDetails', {
                url: '/:lang/eshop/discount-details/:id',
                templateUrl: '/Src/app/eshop/discounts/discount-detail.tpl.html',
                controller: 'DiscountDetailCtrl',
                controllerAs: 'vm',
                resolve: {
                    discount: ['$stateParams', 'discountSrv', function ($stateParams, discountSrv) {
                        return discountSrv.getById($stateParams.id);
                    }]
                }
            }).state('discountAdd', {
                url: '/:lang/eshop/add-discount',
                templateUrl: '/Src/app/eshop/discounts/discount-detail.tpl.html',
                controller: 'DiscountAddCtrl',
                controllerAs: 'vm'
            })
        }

        function registerOrdersRoutes() {
            $stateProvider.state("orders", {
                url: "/:lang/eshop/orders/{pageNumber:int}",
                params: {
                    pageNumber: {
                        dynamic: true
                    }
                },
                templateUrl: "/Src/app/eshop/orders/ordersList.tpl.html",
                controllerAs: 'vm',
                controller: 'OrderListCtrl',
                resolve: {
                    orders: ["$stateParams", "orderSrv", '$location', function ($stateParams, orderSrv, $location) {
                        var reqParams = {
                            pageNumber: $stateParams.pageNumber
                        };
                        angular.extend(reqParams, $location.search());
                        return orderSrv.get(reqParams);
                    }],
                    productEditorSetting: ["settingSrv", function (settingSrv) {
                        return settingSrv.getByName("WebSiteProductEditorSetting");
                    }],
                },
            }).state('orderDetails', {
                url: '/:lang/eshop/order-details/:id',
                templateUrl: '/Src/app/eshop/orders/order-detail.tpl.html',
                controller: 'OrderDetailCtrl',
                controllerAs: 'vm',
                resolve: {
                    order: ['$stateParams', 'orderSrv', function ($stateParams, orderSrv) {
                        return orderSrv.getById($stateParams.id);
                    }],
                     productEditorSetting: ["settingSrv", function (settingSrv) {
                        return settingSrv.getByName("WebSiteProductEditorSetting");
                    }],
                }
            })
        }

        function registerProductsRoutes() {
            $stateProvider.state("products", {
                url: "/:lang/eshop/{pageNumber:int}",
                reloadOnSearch : false,
                params: {
                    pageNumber: {
                        dynamic: true
                    }
                },
                templateUrl: "/Src/app/eshop/products/productsList.tpl.html",
                controllerAs: 'vm',
                controller: 'ProductsListCtrl',
                resolve: {
                    products: ["$stateParams", "SchemeTypes", "productSrv", '$location', function ($stateParams, SchemeTypes, productSrv, $location) {
                        var reqParams = {
                            pageNumber: $stateParams.pageNumber
                        };
                        angular.extend(reqParams, $location.search());
                        return productSrv.get(reqParams);
                    }],
                    categories: ['productCategorySrv', '$stateParams', function (productCategorySrv, $stateParams) {
                        var reqParams = {
                            checkByType: false,
                            mapForTreeView: false
                        };
                        return productCategorySrv.get(reqParams)
                    }]
                },
            }).state('prductAdd', {
                url: "/:lang/eshop/add-product",
                controller: 'productAddCtrl',
                controllerAs: 'vm',
                templateUrl: '/Src/app/eshop/products/product.add.tpl.html',
                resolve: {

                    schemes: ['schemeDataService', '$stateParams', function (schemeDataService, $stateParams) {
                        return schemeDataService.get({
                            type: 10
                        });
                    }],
                    editorSetting: ["settingSrv", function (settingSrv) {
                        return settingSrv.getByName("WebSiteProductEditorSetting");
                    }],
                }
            }).state('productEdit', {
                url: '/:lang/eshop/edit-product/:id',
                templateUrl: '/Src/app/eshop/products/product.add.tpl.html',
                controller: 'productEditCtrl',
                controllerAs: 'vm',
                resolve: {
                    product: ['$stateParams', 'productSrv', function ($stateParams, productSrv) {
                        return productSrv.getById($stateParams.id);
                    }],
                    schemes: ['schemeDataService', '$stateParams', function (schemeDataService, $stateParams) {
                        return schemeDataService.get({
                            type: 5
                        });
                    }],
                    editorSetting: ["settingSrv", function (settingSrv) {
                        return settingSrv.getByName("WebSiteProductEditorSetting");
                    }],
                }
            }).state('productCategories', {
                url: '/:lang/eshop/productCategories',
                controller: "ProductCategoriesCtrl",
                controllerAs: 'vm',
                templateUrl: '/Src/app/eshop/productCategory/productCategories-tpl.html',
                resolve: {
                    categories: ['productCategorySrv', 'currentLang', function (productCategorySrv, currentLang) {
                        var reqParams = {
                            checkByType: false,
                            languageName: currentLang.get(),
                            mapForTreeView: true
                        };
                        return productCategorySrv.get(reqParams);
                    }]
                }
            })
        }

        function registerCommentRoutes() {
            $stateProvider.state("comments", {
                url: "/:lang/comments/:pageNumber?",
                params: {
                    pageNumber: {
                        dynamic: true
                    }
                },
                controller: "CommentCtrl",
                controllerAs: 'vm',
                templateUrl: '/Src/app/comments/Comments.tpl.html',
                resolve: {
                    comments: ['commentDataSrv', '$stateParams', '$location', function (commentDataSrv, $stateParams, $location) {
                        var params = angular.copy($stateParams);
                        return commentDataSrv.get(angular.extend(params, $location.search()))
                    }]
                }
            })
        }

        function registerEntitySchemeRoutes() {
            $stateProvider.state('schemes', {
                    url: "/:lang/schemes/:schemeType/{pageNumber:int}",
                    params: {
                        pageNumber: {
                            dynamic: true
                        }
                    },
                    templateUrl: "/Src/app/entitySchemes/schemes-tpl.html",
                    controllerAs: 'vm',
                    controller: 'schemesCtrl',
                    resolve: {
                        schemes: ['schemeDataService', '$stateParams', function (schemeDataService, $stateParams) {
                            return schemeDataService.get({
                                type: $stateParams.schemeType
                            });
                        }]
                    }
                })
                .state('schemeAdd', {
                    url: "/:lang/schemes/:schemeType/add",
                    templateUrl: '/Src/app/entitySchemes/scheme.add.tpl.html',
                    controller: 'schemeAddCtrl',
                    controllerAs: 'vm',
                    resolve: {
                        languages: ['langDataSrv', function (langDataSrv) {
                            return langDataSrv.get();
                        }]
                    }
                }).state('schemeUpdate', {
                    url: "/:lang/schemes/edit/:scheme",
                    templateUrl: '/Src/app/entitySchemes/scheme.update.tpl.html',
                    controller: 'schemeUpdateCtrl',
                    controllerAs: 'vm',
                    resolve: {
                        scheme: ['schemeDataService', '$stateParams', function (schemeDataService, $stateParams) {
                            return schemeDataService.getBySchemeHandle($stateParams.scheme);
                        }],
                        languages: ['langDataSrv', function (langDataSrv) {
                            return langDataSrv.get();
                        }]
                    }
                });
        }

        function registerPageRoutes() {
            $stateProvider.state('pages', {
                //onEnter: [],
                url: '/:lang/pages/{pageNumber:int}',
                templateUrl: "/Src/app/pages/pages-tpl.html",
                controller: "pagesCtrl",
                controllerAs: 'vm',
                params: {
                    pageNumber: {
                        dynamic: true
                    }
                },
                reloadOnSearch: false,
                resolve: {
                    pages: ['pageDataService', '$location', '$stateParams', function (pageDataService, $location, $stateParams) {
                        var reqParams = {
                            languageName: $stateParams.lang,
                            pageNumber: $stateParams.pageNumber
                        }
                        angular.extend(reqParams, $location.search())
                        return pageDataService.get(reqParams)
                    }],
                }

            }).state('updatePage', {
                url: '/:lang/pages/update/:id',
                templateUrl: "/Src/app/pages/page.update.tpl.html",
                controller: "pageUpdateCtrl",
                controllerAs: 'vm',
                resolve: {
                    page: ['pageDataService', '$stateParams', function (pageDataService, $stateParams) {
                        return pageDataService.getById($stateParams.id)
                    }],
                }
            }).state('addPage', {
                url: '/:lang/pages/add',
                templateUrl: "/Src/app/pages/page.add.tpl.html",
                controller: "pageAddCtrl",
                controllerAs: 'vm',
            })
        }

        function registerEntityRoutes() {
            $stateProvider.state('entities', {
                    url: '/:lang/entiry/:scheme/{pageNumber:int}',
                    templateUrl: '/Src/app/entities/entities-tpl.html',
                    params: {
                        pageNumber: {
                            dynamic: true
                        }
                    },
                    controller: 'entitiesCtrl',
                    controllerAs: 'vm',
                    reloadOnSearch: false,
                    resolve: {
                        scheme: ['$stateParams', 'schemeDataService', function ($stateParams, schemeDataService) {
                            return schemeDataService.getBySchemeHandle($stateParams.scheme);
                        }],
                        entities: ["$stateParams", "entityDataService", '$location', function ($stateParams, entityDataService, $location) {
                            var reqParams = {
                                pageNumber: $stateParams.pageNumber,
                                scheme: $stateParams.scheme
                            };
                            reqParams.languageName = $stateParams.lang;
                            angular.extend(reqParams, $location.search());
                            return entityDataService.get(reqParams);
                        }],
                        categories: ['categoryDataService', 'currentLang', '$stateParams', function (categoryDataService, currentLang, $stateParams) {
                            var reqParams = {
                                checkByType: false,
                                languageName: currentLang.get(),
                                mapForTreeView: false
                            };
                            reqParams.scheme = $stateParams.scheme
                            return categoryDataService.get(reqParams)
                        }]
                    }
                })
                .state('entityAdd', {
                    url: '/:lang/entity/:scheme/add',
                    templateUrl: "/Src/app/entities/entity.add.tpl.html",
                    controller: 'entityAddCtrl',
                    controllerAs: "vm",
                    resolve: {
                        scheme: ['$stateParams', 'schemeDataService', function ($stateParams, schemeDataService) {
                            return schemeDataService.getBySchemeHandle($stateParams.scheme);
                        }]
                    }
                })
                .state('entityUpdate', {
                    url: '/:lang/entity/:scheme/update/:id',
                    templateUrl: '/Src/app/entities/entity.update.tpl.html',
                    controllerAs: 'vm',
                    controller: 'entityUpdateCtrl',
                    resolve: {
                        entity: ['$stateParams', 'entityDataService', function ($stateParams, entityDataService) {
                            return entityDataService.getById($stateParams.scheme, $stateParams.id);
                        }],
                        scheme: ['$stateParams', 'schemeDataService', function ($stateParams, schemeDataService) {
                            return schemeDataService.getBySchemeHandle($stateParams.scheme);
                        }]
                    }
                })
        }

        function registerEntityCategoryRoutes() {
            $stateProvider.state('categories', {
                url: '/:lang/categories/:scheme',
                templateUrl: '/Src/app/entityCategories/categories-tpl.html',
                controller: 'EntityCategoriesCtrl',
                reloadOnSearch: false,
                controllerAs: 'vm',
                resolve: {
                    scheme: ['schemeDataService', '$stateParams', function (schemeDataService, $stateParams) {
                        return schemeDataService.getBySchemeHandle($stateParams.scheme);
                    }],
                    categories: ['categoryDataService', '$stateParams', '$location', 'currentLang', function (categoryDataService, $stateParams, $location, currentLang) {
                        var reqParams = angular.copy($stateParams);
                        angular.extend(reqParams, $location.search())
                        angular.extend(reqParams, {
                            checkByType: false,
                            languageName: currentLang.get(),
                            mapForTreeView: true
                        })


                        return categoryDataService.get(reqParams)
                    }]
                }
            })
        }

        function registerUserManagementRoutes() {
            $stateProvider.state('userManagement', {
                url: '/:lang/users/{pageNumber:int}',
                templateUrl: '/Src/app/userManagement/users-tpl.html',
                controller: 'UserManagementCtrl',
                controllerAs: 'vm',
                params: {
                    pageNumber: {
                        dynamic: true
                    }
                },
                resolve: {
                    users: ['userDataSrv', '$stateParams', '$location', function (userDataSrv, $stateParams, $location) {
                        var reqParams = angular.copy($stateParams);
                        angular.extend(reqParams, $location.search())
                        return userDataSrv.get(reqParams);
                    }]
                }
            })
        }

        function registerMenuRoutes() {
            $stateProvider.state('menu', {
                url: "/:lang/manageMenus",
                controller: 'MenusCtrl',
                controllerAs: 'vm',
                templateUrl: "/Src/app/menu/menus-tpl.html",
                resolve: {
                    menus: ['menuDataSrv', 'currentLang', function (menuDataSrv, currentLang) {
                        return menuDataSrv.get({
                            languageName: currentLang.get()
                        });
                    }],
                    pages: ['pageDataService', 'currentLang', function (pageDataService, currentLang) {
                        return pageDataService.get({
                            pageSize: 5,
                            pageNumber: 1,
                            languageName: currentLang.get()
                        });
                    }],
                    categories: ['categoryDataService', 'currentLang', function (categoryDataService, currentLang) {
                        return categoryDataService.get({
                            pageSize: 5,
                            pageNumber: 1,
                            languageName: currentLang.get()
                        });
                    }],
                    entities: ['entityDataService', 'currentLang', function (entityDataService, currentLang) {
                        return entityDataService.get({
                            pageSize: 5,
                            pageNumber: 1,
                            languageName: currentLang.get()
                        });
                    }],
                    userProfiles: ['userDataSrv', 'currentLang', function (userDataSrv, currentLang) {
                        return userDataSrv.get({
                            pageSize: 5,
                            pageNumber: 1,
                        });
                    }],
                    productCategories: ['productCategorySrv', 'currentLang', function (productCategorySrv, currentLang) {
                        return productCategorySrv.get({
                            pageSize: 1,
                            pageNumber: 1,
                        });
                    }],

                }
            })
        }

        function registerWidgetRoutes() {
            $stateProvider.state('widgets', {
                url: '/:lang/widgets',
                controllerAs: 'vm',
                controller: 'widgetCtrl',
                templateUrl: '/Src/app/widgets/widget-tpl.html',
                resolve: {
                    widgets: ['widgetContainerDataSrv', function (widgetContainerDataSrv) {
                        return widgetContainerDataSrv.get();
                    }]
                }
            });
        }

        function registerFormSchemeRoutes() {
            $stateProvider.state('formSchemes', {
                url: '/:lang/formSchemes/{pageNumber:int}',
                templateUrl: '/Src/app/formSchemes/formSchemes-tpl.html',
                controller: 'formSchemesCtrl',
                controllerAs: 'vm',
                params: {
                    pageNumber: {
                        dynamic: true
                    }
                },
                resolve: {
                    formSchemes: ['formSchemeDataSrv', '$stateParams', '$location', function (formSchemeDataSrv, $stateParams, $location) {
                        return formSchemeDataSrv.get({
                            pageNumber: $stateParams.pageNumber,
                            pageSize: ($location.search().pageSize || 15)
                        });
                    }]
                }
            });

            $stateProvider.state('addFormScheme', {
                url: '/:lang/formSchemes/add',
                controller: 'formSchemeAddCtrl',
                controllerAs: 'vm',
                templateUrl: '/Src/app/formSchemes/formSchemeAdd.tpl.html'
            });

            $stateProvider.state('updateFormScheme', {
                url: '/:lang/formSchemes/edit/:handle',
                controller: 'formSchemeUpdateCtrl',
                controllerAs: 'vm',
                templateUrl: '/Src/app/formSchemes/formSchemeUpdate.tpl.html',
                resolve: {
                    formScheme: ['formSchemeDataSrv', '$stateParams', function (formSchemeDataSrv, $stateParams) {
                        return formSchemeDataSrv.getByHandle($stateParams.handle);
                    }]
                }
            });
        }

        function registerFormRoutes() {
            $stateProvider.state('forms', {
                url: '/:lang/forms/:formHandle/{pageNumber:int}',
                params: {
                    pageNumber: {
                        dynamic: true
                    }
                },
                templateUrl: '/Src/app/forms/forms-tpl.html',
                controllerAs: 'vm',
                controller: 'FormsCtrl',
                resolve: {
                    formScheme: ['formSchemeDataSrv', '$stateParams', function (formSchemeDataSrv, $stateParams) {
                        return formSchemeDataSrv.getByHandle($stateParams.formHandle);
                    }],
                    forms: ['formDataSrv', 'currentLang', '$location', '$stateParams', function (formDataSrv, currentLang, $location, $stateParams) {
                        var reqParams = {
                            pageNumber: $stateParams.pageNumber,
                            formSchemeHandle: $stateParams.formHandle
                        }
                        angular.extend(reqParams, $location.search());
                        return formDataSrv.get(reqParams);
                    }],
                }
            });
        }

        function registerUserProfileRoutes() {
            $stateProvider.state('userprofile', {
                url: '/:lang/myprofile',
                templateUrl: '/Src/app/userProfile/userProfile.tpl.html',
                controller: 'userProfileCtrl',
                controllerAs: 'vm',
            })
        }
        //  <base href="/panel/" />  <!-- need to end with => /-->
        $locationProvider.html5Mode({
            enabled: true,
            requireBase: true
        });

        $urlRouterProvider.otherwise('/fa-IR/dashboard');
    }
})();