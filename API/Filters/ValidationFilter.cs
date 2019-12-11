using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.Collections.Generic;

namespace API.Filters
{
    public class ValidationFilter : IActionFilter, IOrderedFilter
    {
        public int Order => 0;

        public void OnActionExecuted(ActionExecutedContext context)
        {

        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
            if (context.Controller is APIControllerBase controller)
            {
                if (!context.ModelState.IsValid)
                {
                    context.Result = controller.StatusCode(400, new APIResponse<string>
                    {
                        Payload = null,
                        Errors = GetErrorMessages(context.ModelState.Values)
                    });
                }
            }
        }

        private IEnumerable<string> GetErrorMessages(ModelStateDictionary.ValueEnumerable values)
        {
            List<string> results = new List<string>();

            foreach (var value in values)
            {
                foreach (var error in value.Errors)
                {
                    results.Add(error.ErrorMessage);
                }

                if (value.Children == null)
                {
                    continue;
                }

                foreach (var child in value.Children)
                {
                    results.AddRange(GetErrorMessages(child));
                }
            }

            return results;
        }

        private IEnumerable<string> GetErrorMessages(ModelStateEntry entry)
        {
            List<string> results = new List<string>();

            foreach (var error in entry.Errors)
            {
                results.Add(error.ErrorMessage);
            }

            if (entry.Children == null)
            {
                return results;
            }

            foreach (var child in entry.Children)
            {
                results.AddRange(GetErrorMessages(child));
            }

            return results;
        }
    }
}