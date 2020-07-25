using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Sol_Demo.Filters
{
    public class TaskCancelledExceptionFilter : ExceptionFilterAttribute
    {
        private readonly ILogger _logger;

        public TaskCancelledExceptionFilter(ILoggerFactory loggerFactory)
        {
            _logger = loggerFactory.CreateLogger<TaskCancelledExceptionFilter>();
        }

        public override void OnException(ExceptionContext context)
        {
            if (context.Exception is OperationCanceledException)
            {
                _logger.LogError("Request was cancelled");
                context.ExceptionHandled = true;
                context.Result = new StatusCodeResult(400);
            }
        }
    }
}