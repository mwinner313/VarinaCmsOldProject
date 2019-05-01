using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Web.Configuration;
using System.Web.Mvc;
using Newtonsoft.Json;

namespace VarinaCmsV2.Common.MVC
{
    public class ValidateGoogleCaptchaAttribute : ActionFilterAttribute
    {
        private const string ModelStateErrorName = "reCaptcha";
        public static readonly string SecretKey = WebConfigurationManager.AppSettings["reCaptchaSecret"];
        public static readonly string SitetKey = WebConfigurationManager.AppSettings["reCaptchaSiteKey"];
        private readonly bool _captchaEnabled = WebConfigurationManager.AppSettings["captchaEnabled"] == "true";
        public static Dictionary<string, string> GoogleErrorMessages { get; set; } = new Dictionary<string, string>()
        {
            ["missing-input-secret"] = "کد امنیتی جهت بررسی موجود نمیباشد",
            ["invalid-input-secret"] = "کد (secret)امنیتی فرستاده شده کپچا صحیح نمیباشد لطفا دوباره تلاش کنید",
            ["missing-input-response"] = "لطفا دوباره تلاش کنید کپچا خالی مانده است",
            ["invalid-input-response"] = "مقادیر کپچای وارد شده صحیح نمیباشد",
        };


        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            if (!_captchaEnabled)
            {
                base.OnActionExecuting(filterContext);
                return;
            }

                var response = GetGoogleCaptchaValidationResult(filterContext);
            //secret that was generated in key value pair

            var client = new WebClient();
            var reply =
                client.DownloadString(
                    $"https://www.google.com/recaptcha/api/siteverify?secret={SecretKey}&response={response}");

            var captchaResponse = JsonConvert.DeserializeObject<CaptchaResponse>(reply);
            if (!captchaResponse.Success)
                filterContext.Result = new CustomJsonResult() { Data = new { errors = GoogleErrorMessages[captchaResponse.ErrorCodes[0]] }, StatusCode = 400 };

            base.OnActionExecuting(filterContext);
        }

        private string GetGoogleCaptchaValidationResult(ActionExecutingContext filterContext)
        {
            return filterContext.HttpContext.Request["g-recaptcha-response"];
        }
    }
    public class CaptchaResponse
    {

        [JsonProperty("success")]
        public bool Success { get; set; }

        [JsonProperty("error-codes")]
        public List<string> ErrorCodes { get; set; }

    }
}
