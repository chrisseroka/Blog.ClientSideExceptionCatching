using System.Collections.Generic;
using System.IO;
using System.Web;
using System.Web.Routing;
using System.Web.Script.Serialization;

namespace Blog.ClientSideExceptionCatching.Infrastructure
{
    public class JavaScriptErrorHandler: IRouteHandler, IHttpHandler
    {
        public JavaScriptErrorHandler(bool isReusable)
        {
            IsReusable = isReusable;
        }

        public IHttpHandler GetHttpHandler(RequestContext requestContext)
        {
            return this;
        }

        public void ProcessRequest(HttpContext context)
        {
            var jsonString = new StreamReader(context.Request.InputStream).ReadToEnd();
            var jsonData = new JavaScriptSerializer().Deserialize<Dictionary<string, string>>(jsonString);

            var jsException = new JavaScriptException(
                message: jsonData["message"],
                url: jsonData["url"],
                line: jsonData["line"],
                column: jsonData["column"],
                stack: jsonData["stack"]);
            Elmah.ErrorSignal.FromCurrentContext().Raise(jsException);
        }

        public bool IsReusable { get; private set; }
    }
}