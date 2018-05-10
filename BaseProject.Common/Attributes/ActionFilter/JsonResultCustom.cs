using System;
using System.Web.Mvc;
using Newtonsoft.Json;
namespace Common.Attributes.ActionFilter
{
    public class JsonResultCustom : ActionFilterAttribute
    {
        public override void OnActionExecuted(ActionExecutedContext filterContext)
        {
            if (filterContext.Result is JsonResult == false)
            {
                return;
            }

            filterContext.Result = new JsonNetResult(
                (JsonResult)filterContext.Result);
        }

        private class JsonNetResult : JsonResult
        {
            public JsonNetResult(JsonResult jsonResult)
            {
                ContentEncoding = jsonResult.ContentEncoding;
                ContentType = jsonResult.ContentType;
                Data = jsonResult.Data;
                JsonRequestBehavior = jsonResult.JsonRequestBehavior;
                MaxJsonLength = jsonResult.MaxJsonLength;
                RecursionLimit = jsonResult.RecursionLimit;
            }

            public override void ExecuteResult(ControllerContext context)
            {
                if (context == null)
                {
                    throw new ArgumentNullException($"context");
                }

                var isMethodGet = string.Equals(
                    context.HttpContext.Request.HttpMethod,
                    "GET",
                    StringComparison.OrdinalIgnoreCase);

                if (JsonRequestBehavior == JsonRequestBehavior.DenyGet
                    && isMethodGet)
                {
                    throw new InvalidOperationException(
                        "GET not allowed! Change JsonRequestBehavior to AllowGet.");
                }

                var response = context.HttpContext.Response;

                response.ContentType = string.IsNullOrEmpty(ContentType)
                    ? "application/json"
                    : ContentType;

                if (ContentEncoding != null)
                {
                    response.ContentEncoding = ContentEncoding;
                }

                if (Data != null)
                {
                    response.Write(JsonConvert.SerializeObject(Data));
                }
            }
        }
    }
}
