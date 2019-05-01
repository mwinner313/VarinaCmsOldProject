using System;
using System.Collections.Generic;

using VarinaCmsV2.Core.Contracts.Widget;
using VarinaCmsV2.Core.Logic.Widgets.Category;
using VarinaCmsV2.Core.Logic.Widgets.Entity;
using VarinaCmsV2.Core.Logic.Widgets.Menu;
using VarinaCmsV2.Core.Logic.Widgets.Page;
using VarinaCmsV2.Core.Logic.Widgets.SocialLink;
using VarinaCmsV2.Core.Logic.Widgets.StaticHtml;
using VarinaCmsV2.Core.Logic.Widgets.UserProfile;

namespace VarinaCmsV2.Core.Logic.Widgets
{
    public class WidgetFactory : IWidgetFactory
    {
        //TODo what the hell is these if statements -- 
        //because of object creation using reflection that costs memory and cpu cunsumption, i have to write it like this i promise i will fix it later

        public IWidget Get(string type)
        {
            if (type == "menu")
                return new MenuWidget();

            if (type == "html")
                return new StaticHtmlWidget();

            if (type == "page")
                return new PageWidget();

            if (type == "entity")
                return new EntityWidget();

            if (type == "user-profile")
                return new UserProfileWidget();

            if (type == "category")
                return new CategoryWidget();

            if (type == "calendar")
                return new CalendarWidget();

            if (type == "product")
                return new ProductWidget();

            if (type == "google-analytics")
                return new GoogleAnalyticsWidget();

            if (type == "social_link")
                return new SocialLinkWidget();
            return null;
        }

        public List<AvailibleWidget> GetAvailibleWidgets()
        {
            return new List<AvailibleWidget>()
            {
                new AvailibleWidget()
                {
                    Title = "فهرست",
                    Type = "menu"
                },
                 new AvailibleWidget()
                {
                    Title = "دسته بندی",
                    Type = "category"
                } ,
                new AvailibleWidget()
                {
                    Title = "دسته بندی محصولات",
                    Type = "productCategory"
                },
                new AvailibleWidget()
                {
                    Title = "محصولات",
                    Type = "product"
                },
                  new AvailibleWidget()
                {
                    Title = "صفحه ثابت",
                    Type = "page"
                },
                   new AvailibleWidget()
                {
                    Title = "اخرین پست ها",
                    Type = "entity"
                },
                    new AvailibleWidget()
                {
                    Title = "محتوای html",
                    Type = "html"
                },
                     new AvailibleWidget()
                {
                    Title = "پروفایل کاربر",
                    Type = "userProfile"
                },
                     new AvailibleWidget()
                {
                    Title = "لینک شبکه های اجتماعی",
                    Type = "social_link"
                }

            };
        }
    }
}
