using jwt_aspnet_core.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace jwt_aspnet_core.ActionsFilters;

public class ValidationFilerAttribute : IActionFilter
{
    public void OnActionExecuting(ActionExecutingContext context)
    {
        var param = context.ActionArguments.SingleOrDefault(p => p.Value is Movie);
        if (param.Value == null)
        {
            context.Result = new BadRequestObjectResult("Object is null");
            return;
        }

        if (!context.ModelState.IsValid)
        {
            context.Result = new BadRequestObjectResult(context.ModelState);
        }
    }

    public void OnActionExecuted(ActionExecutedContext context)
    {
        throw new NotImplementedException();
    }
}