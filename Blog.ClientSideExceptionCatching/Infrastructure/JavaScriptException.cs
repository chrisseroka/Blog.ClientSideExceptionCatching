using System;
using System.Text;

namespace Blog.ClientSideExceptionCatching.Infrastructure
{
    public class JavaScriptException : Exception
    {
        public JavaScriptException(
            string message,
            string url,
            string line,
            string column,
            string stack)
            : base(message)
        {
            this.JsUrl = url;
            this.JsStackTrace = stack;
            this.JsLineNumber = line;
            this.JsColumnNumber = column;
        }

        public override string ToString()
        {
            var stringBuilder = new StringBuilder();
            stringBuilder.AppendLine(
                this.Message.Length <= 0 ? this.GetType().Name : this.GetType().Name + ": " + this.Message);
            if (this.JsStackTrace == null ||
                this.JsStackTrace.Length <= 0)
            {
                stringBuilder.AppendLine(String.Format("{0} [{1}:{2}]", this.JsUrl, this.JsLineNumber,
                                                       this.JsColumnNumber));
            }
            else
            {
                stringBuilder.AppendLine(this.JsStackTrace);
            }
            return stringBuilder.ToString();
        }

        private string JsStackTrace { get; set; }
        private string JsLineNumber { get; set; }
        private string JsColumnNumber { get; set; }
        private string JsUrl { get; set; }
    }
}