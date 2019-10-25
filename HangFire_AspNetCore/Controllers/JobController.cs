using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Hangfire;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HangFire_AspNetCore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class JobController : ControllerBase
    {

        [HttpGet]
        public IActionResult Get()
        {
            var jobFireForget = BackgroundJob.Enqueue(() => Console.WriteLine($"Disparar agora: {DateTime.Now}"));
            var jobDelayed = BackgroundJob.Schedule(() => Console.WriteLine($"Com Delay: {DateTime.Now}"), TimeSpan.FromSeconds(10));
            BackgroundJob.ContinueJobWith(jobDelayed, () => Console.WriteLine($"Continuação: {DateTime.Now}"));
            RecurringJob.AddOrUpdate(() => Console.WriteLine($"Job Recorrente: {DateTime.Now}"), Cron.Minutely);

            return Ok("Jobs criados com sucesso");
        }
    }
}