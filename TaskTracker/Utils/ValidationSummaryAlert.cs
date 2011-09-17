using System;
using System.Text;
using System.Collections.Generic;

namespace System.Web.Mvc.Html
{
    /**
     * \brief Extends the ValidationSummary class.
     * \author Katharine Gillis
     * \date 2011-09-15
     * 
     * Extends the ValidationSummary class.
     */
    public static class ValidationSummaryExtension
    {
        /**
         * \brief Generate a validation summary with alert css.
         * 
         * Generates a validation summary using alert css from the jQuery ui.
         */
        public static IHtmlString ValidationSummaryAlert(this HtmlHelper ext, string selector, bool excludeProperties = false, string message = "")
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