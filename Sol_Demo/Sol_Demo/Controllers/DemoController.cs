using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Sol_Demo.Filters;
using Sol_Demo.Models;

namespace Sol_Demo.Controllers
{
    public class DemoController : Controller
    {
        private readonly ILogger logger = null;

        public DemoController(ILogger<DemoController> logger)
        {
            this.logger = logger;
            Process = new ProcessModel();
        }

        [BindProperty]
        public ProcessModel Process { get; set; }

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet("longtask", Name = "longtask")]
        public async Task<IActionResult> LongRunningTaskAsync(CancellationToken cancellationToken)
        {
            try
            {
                logger.LogInformation("Proces Start");
                // Long Running Task
                for (int i = 0; i <= 10; i++)
                {
                    cancellationToken.ThrowIfCancellationRequested();
                    await Task.Delay(1000, cancellationToken);
                }

                logger.LogInformation("Process Complete");
            }
            catch
            {
                throw;
            }
            //catch (Exception ex) when (ex is TaskCanceledException || ex is OperationCanceledException)
            //{
            //    logger.LogError("Operation Cancelled");
            //}

            Process.Status = "Process Complete";
            return View("Index", Process);
        }
    }
}