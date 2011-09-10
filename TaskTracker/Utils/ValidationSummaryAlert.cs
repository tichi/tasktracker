using System;
using System.Text;
using System.Collections.Generic;

namespace System.Web.Mvc.Html
{
    public static class ValidationSummaryExtension
    {
        /// <summary>
        /// Creates a validation summary with a container element 
        /// surrounding the summary and error messages.
        /// </summary>        
        public static IHtmlString ValidationSummaryAlert(this HtmlHelper ext, string selector, bool excludeProperties, string message)
        {
            StringBuilder output = new StringBuilder();
            if (ext.ViewData.ModelState.IsValid) {
                return new HtmlString(output.ToString());
            }

            if (excludeProperties)
            {
                output.Append("$('");
                output.Append(selector);
                output.Append("').addAlert('");
                output.Append(message);
                output.AppendLine("');");
            }
            else
            {
                foreach (KeyValuePair<string, ModelState> keyValuePair in ext.ViewData.ModelState)
                {
                    foreach (ModelError modelError in keyValuePair.Value.Errors)
                    {
                        output.Append("$('");
                        output.Append(selector);
                        output.Append("').addAlert('");
                        output.Append(modelError.ErrorMessage);
                        output.AppendLine("');");
                    }
                }
            }

            return new HtmlString(output.ToString());
        }
    }
}