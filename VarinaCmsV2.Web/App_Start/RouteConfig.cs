using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Routing;
using StructureMap;
using VarinaCmsV2.Common.Routing;
using VarinaCmsV2.Core.Web;
using VarinaCmsV2.Web.RouteConstraints;

namespace VarinaCmsV2.Web
{
    public class RouteConfig
    {
        private const string NumberConstraint = @"\d+";

        public static void RegisterRoutes(RouteCollection routes)
        {
            IgnoreFiles(routes);

            routes.MapMvcAttributeRoutes();

            RegisterPageRoute(routes);
            RegisterUserProfileRoute(routes);
            RegisterTagRoute(routes);
            RegisterEntityRoute(routes);
            RegisterEntityCategoryRoute(routes);
            RegisterFormRoute(routes);
            RegisterUserEntityRoutes(routes);
            RegisterCommentRoutes(routes);
            RegisterProductRoutes(routes);
            RegisterProductCategoryRoutes(routes);
            RegisterCartRoute(routes);
            RegisterOrderRoute(routes);
            RegisterCheckoutRoute(routes);
            //call this at the end 
            RegisterDefaults(routes);
        }

        private static void RegisterCheckoutRoute(RouteCollection routes)
        {
            routes.MapRouteWithName(
                name: "Checkout",
                url: "Checkout/{action}",
                defaults: new { controller = "Checkout", action = "Index" }
            );
        }

        private static void RegisterOrderRoute(RouteCollection routes)
        {
            routes.MapRouteWithName(
                name: "Order",
                url: "Order/{action}",
                defaults: new { controller = "Order", action = "Index" }
            );
        }

        private static void RegisterCartRoute(RouteCollection routes)
        {
            routes.MapRouteWithName(
                name: "Cart",
                url: "cart/{action}",
                defaults: new { controller = "Cart", action = "Index" }
            );
        }

        private static void RegisterProductCategoryRoutes(RouteCollection routes)
        {
            routes.MapGreedyRouteWithName(
                name: Routes.ProductCategoryDefault,
                url: "eshop/category/{*productCategoryUrl}/page/{pageNumber}",
                defaults: new { controller = "Product", action = "ListByCategoryAsync", pageNumber = 1 },
                constraints: new
                {
                    productCategoryUrl = new ProductCategoryRouteConstraint(),
                    pageNumber = NumberConstraint
                }
            );

            routes.MapGreedyRouteWithName(
                name: Routes.ProductCategoryWithLang,
                url: "{lang}/eshop/category/{*productCategoryUrl}/page/{pageNumber}",
                defaults: new { controller = "Product", action = "ListByCategoryAsync", pageNumber = 1 },
                constraints: new { productCategoryUrl = new ProductCategoryRouteConstraint(), pageNumber = NumberConstraint, lang = new LanguageRouteConstraint() }
            );
        }

        private static void RegisterProductRoutes(RouteCollection routes)
        {
            routes.MapRouteWithName(
                name: Routes.ProductDefault,
                url: "product/{productUrl}",
                defaults: new { controller = "Product", action = "ShowItemAsync" },
                constraints: new { productUrl = new ProductRouteConstraint() }
            );

            routes.MapRouteWithName(
                name: Routes.ProductWithLang,
                url: "{lang}/product/{productUrl}",
                defaults: new { controller = "Product", action = "ShowItemAsync" },
                constraints: new { lang = new LanguageRouteConstraint() }
            );

            routes.MapRouteWithName(
                name: Routes.ProductSearchWithLang,
                url: "{lang}/eshop/search",
                defaults: new { controller = "Product", action = "SearchAsync" },
                constraints: new { lang = new LanguageRouteConstraint() }
            );
            routes.MapRouteWithName(
                name: Routes.ProductSearch,
                url: "eshop/search",
                defaults: new { controller = "Product", action = "SearchAsync" }
            );

            routes.MapRouteWithName(
                name: Routes.ProductCompare,
                url: "eshop/compare/{product1}/{product2}/{product3}/{product4}",
                defaults: new
                {
                    controller = "Product",
                    action = "CompareAsync",
                    product2 = UrlParameter.Optional,
                    product3 = UrlParameter.Optional,
                    product4 = UrlParameter.Optional,
                }
            );

            routes.MapRouteWithName(
                name: Routes.ProductDownload,
                url: "eshop/product/download/{orderId}/{productId}",
                defaults: new
                {
                    controller = "Product",
                    action = "DownloadAsync",
                }
            );
            routes.MapRouteWithName(
                name: Routes.ProductSampleFileDownload,
                url: "eshop/product/download/sample/{productId}",
                defaults: new
                {
                    controller = "Product",
                    action = "DownloadSampleAsync",
                }
            );
        }


        private static void RegisterCommentRoutes(RouteCollection routes)
        {
            routes.MapRouteWithName(
                name: Routes.CommentRoute,
                url: "Comment/{action}",
                defaults: new { controller = "Comment" }
            );

        }

        private static void RegisterUserEntityRoutes(RouteCollection routes)
        {
            routes.MapRouteWithName(
                name: Routes.UserEntityRoute,
                url: "author/{userUrl}/{schemeUrl}/page/{pageNumber}",
                defaults: new { controller = "Entity", action = "ShowUserCreatedEntitiesAsync" },
                constraints: new
                {
                    userUrl = new UserProfileRouteConstraint(),
                    pageNumber = NumberConstraint,
                    schemeUrl = new EntitySchemeUrlConstraint()
                }
            );

            routes.MapRouteWithName(
                name: Routes.UserEntityRouteWithLang,
                url: "{lang}/author/{userUrl}/{schemeUrl}/page/{pageNumber}",
                defaults: new { controller = "Entity", action = "ShowUserCreatedEntitiesAsync" },
                constraints: new
                {
                    userUrl = new UserProfileRouteConstraint(),
                    pageNumber = NumberConstraint,
                    schemeUrl = new EntitySchemeUrlConstraint(),
                    lang = new LanguageRouteConstraint()
                }
            );
        }


        private static void IgnoreFiles(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
            routes.IgnoreRoute("{resource}.jpeg");
            routes.IgnoreRoute("{resource}.jpg");
            routes.IgnoreRoute("{resource}.ico");
            routes.IgnoreRoute("{resource}.js");
            routes.IgnoreRoute("{resource}.png");
            routes.IgnoreRoute("{resource}.woff");
            routes.IgnoreRoute("{resource}.eot");
            routes.IgnoreRoute("{resource}.ttf");
            routes.IgnoreRoute("{resource}.svg");
            routes.IgnoreRoute("ClientApp/{*pathInfo}");
            routes.IgnoreRoute("Content/{*pathInfo}");
            routes.IgnoreRoute("Scripts/{*pathInfo}");
            routes.IgnoreRoute("Uploads/{*pathInfo}");
            routes.IgnoreRoute("fonts/{*pathInfo}");
        }

        private static void RegisterFormRoute(RouteCollection routes)
        {
            routes.MapRouteWithName(
               name: Routes.Forms,
               url: "form/{formHandle}",
               defaults: new { controller = "Forms", action = "Submit" },
               constraints: null
             );
        }

        private static void RegisterUserProfileRoute(RouteCollection routes)
        {
            routes.MapRouteWithName(
              name: Routes.UserProfile,
              url: "author/{userUrl}",
              defaults: new { controller = "UserProfile", action = "ShowUserProfileAsync" },
              constraints: new { userUrl = new UserProfileRouteConstraint() }
            );
            routes.MapRouteWithName(
                name: Routes.UserProfileWithLang,
                url: "{lang}/author/{userUrl}",
                defaults: new { controller = "UserProfile", action = "ShowUserProfileAsync" },
                constraints: new
                {
                    userUrl = new UserProfileRouteConstraint(),
                    lang = new LanguageRouteConstraint(),
                }
            );


        }

        private static void RegisterTagRoute(RouteCollection routes)
        {
            routes.MapRouteWithName(
                name: Routes.EntityTag,
                url: "{schemeUrl}/tag/{tagUrl}/page/{pageNumber}",
                defaults: new { controller = "Entity", action = "ListByTagAsync" },
                constraints: new { tagUrl = new TagRouteConstraint(), pageNumber = NumberConstraint }
            );
            routes.MapRouteWithName(
                name: Routes.EntityTagWithLang,
                url: "{lang}/{schemeUrl}/tag/{tagUrl}/page/{pageNumber}",
                defaults: new { controller = "Entity", action = "ListByTagAsync" },
                constraints: new { tagUrl = new TagRouteConstraint(), pageNumber = NumberConstraint }
            );

            routes.MapRouteWithName(
               name: Routes.Tag,
               url: "tag/{tagUrl}",
               defaults: new { controller = "Tag", action = "ListByTagAsync" },
               constraints: new { tagUrl = new TagRouteConstraint() }
           );
            routes.MapRouteWithName(
                name: Routes.TagWithLang,
                url: "{lang}/tag/{tagUrl}",
                defaults: new { controller = "Tag", action = "ListByTagAsync" },
                constraints: new { tagUrl = new TagRouteConstraint() }
            );

            routes.MapRouteWithName(
                name: Routes.ProductTag,
                url: "eshop/tag/{tagUrl}",
                defaults: new { controller = "Product", action = "ListByTagAsync" },
                constraints: new { tagUrl = new TagRouteConstraint() }
            );

            routes.MapRouteWithName(
                name: Routes.ProductTagWithLang,
                url: "{lang}/eshop/tag/{tagUrl}",
                defaults: new { controller = "Product", action = "ListByTagAsync" },
                constraints: new { tagUrl = new TagRouteConstraint() }
            );

        }

        private static void RegisterPageRoute(RouteCollection routes)
        {
            routes.MapRouteWithName(
                name: Routes.Page,
                url: "{*pageUrl}",
                defaults: new { controller = "Page", action = "ShowPageAsync" },
                constraints: new { pageUrl = new PageRouteConstraint() }
            );
            routes.MapRouteWithName(
              name: Routes.PageWithLang,
              url: "{lang}/{*pageUrl}",
              defaults: new { controller = "Page", action = "ShowPageAsync" },
              constraints: new { pageUrl = new PageRouteConstraint() }
          );
        }

        private static void RegisterEntityRoute(RouteCollection routes)
        {
            routes.MapRouteWithName(
                name: Routes.EntityByScheme,
                url: "{schemeUrl}",
                defaults: new { controller = "Entity", action = "ListBySchemeAsync" },
                constraints: new { schemeUrl = new SchemeRouteConstraint() }
            );
            routes.MapGreedyRouteWithName(
                name: Routes.EntityByScheme + "Pagination",
                url: "{schemeUrl}/page/{pageNumber}",
                defaults: new { controller = "Entity", action = "ListBySchemeAsync" },
                constraints: new { schemeUrl = new SchemeRouteConstraint(), pageNumber = NumberConstraint }
            );
            routes.MapRouteWithName(
                name: Routes.EntityBySchemeWithLang,
                url: "{lang}/{schemeUrl}",
                defaults: new { controller = "Entity", action = "ListBySchemeAsync" },
                constraints: new { schemeUrl = new SchemeRouteConstraint(), lang = new LanguageRouteConstraint() }
            );

            routes.MapGreedyRouteWithName(
                name: Routes.EntityBySchemeWithLang + "Pagination",
                url: "{lang}/{schemeUrl}/page/{pageNumber}",
                defaults: new { controller = "Entity", action = "ListBySchemeAsync" },
                constraints: new { schemeUrl = new SchemeRouteConstraint(), pageNumber = NumberConstraint, lang = new LanguageRouteConstraint() }
            );

            routes.MapRouteWithName(
                name: Routes.EntityDefault,
                url: "{schemeUrl}/{entityUrl}",
                defaults: new { controller = "Entity", action = "ShowItemAsync" },
                constraints: new { entityUrl = new EntityRouteConstraint() }
            );
            routes.MapRouteWithName(
                name: Routes.EntitySearch,
                url: "search/{schemeUrl}/{searchText}",
                defaults: new { controller = "Entity", action = "SearchAsync", searchText = UrlParameter.Optional },
                constraints: new { schemeUrl = new SchemeRouteConstraint() }
            );
            routes.MapRouteWithName(
               name: Routes.EntityWithLang,
               url: "{lang}/{schemeUrl}/{entityUrl}",
               defaults: new { controller = "Entity", action = "ShowItemAsync" },
               constraints: new { entityUrl = new EntityRouteConstraint() }
             );
            routes.MapRouteWithName(
             name: Routes.EntityWithDate,
             url: "{schemeUrl}/{year}/{month}/{day}/{entityUrl}",
             defaults: new { controller = "Entity", action = "ShowItemAsync" },
             constraints: new { entityUrl = new EntityDateRouteConstraint() }
           );
            routes.MapRouteWithName(
              name: Routes.EntityWithLangAndDate,
              url: "{lang}/{schemeUrl}/{year}/{month}/{day}/{entityUrl}",
              defaults: new { controller = "Entity", action = "ShowItemAsync" },
              constraints: new { entityUrl = new EntityLangDateRouteConstraint() }
           );


        }

        private static void RegisterEntityCategoryRoute(RouteCollection routes)
        {

            routes.MapGreedyRouteWithName(
              name: Routes.EntityCategory,
              url: "{schemeUrl}/category/{*categoryUrl}/page/{pageNumber}",
              defaults: new { controller = "Entity", action = "ListByCategoryAsync", pageNumber = 1 },
              constraints: new
              {
                  categoryUrl = new EntityCategoryRouteConstraint(),
                  pageNumber = NumberConstraint
              }
            );

            routes.MapGreedyRouteWithName(
            name: Routes.EntityCategoryWithLang,
            url: "{lang}/{schemeUrl}/category/{*categoryUrl}/page/{pageNumber}",
            defaults: new { controller = "Entity", action = "ListByCategoryAsync", pageNumber = 1 },
            constraints: new { categoryUrl = new EntityCategoryRouteConstraint(), pageNumber = NumberConstraint }
         );
        }

        private static void RegisterDefaults(RouteCollection routes)
        {
            //routes.MapRouteWithName(
            //  name: "Default",
            //  url: "{controller}/{action}/{id}",
            //  defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            //);

            routes.MapRouteWithName(
              name: "Login",
              url: "Login",
              defaults: new { controller = "Account", action = "Login", id = UrlParameter.Optional }
            );
            routes.MapRouteWithName(
                name: "Account",
                url: "Account/{action}",
                defaults: new { controller = "Account", action = "Login" }
            );
            routes.MapRouteWithName(
               name: "HomePage",
               url: "",
               defaults: new { controller = "Home", action = "Index" }
            );
            routes.MapRouteWithName(
                name: "HomePageWithPagination",
                url: "page/{pageNumber}",
                defaults: new { controller = "Home", action = "Index", pageNumber = UrlParameter.Optional }
             , constraints: new { pageNumber = NumberConstraint }
            );
            routes.MapRouteWithName(
                name: "404",
                url: "{*url}",
                defaults: new { controller = "Home", action = "NotFound", url = UrlParameter.Optional },
                constraints: null

            );
        }

    }
}
