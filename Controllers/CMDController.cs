using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using cmd_service.Services;
using Microsoft.AspNetCore.Mvc;

namespace cmd_service.Controllers
{
    [ApiController]
    public class CMDController : ControllerBase
    {
        private readonly ProcessService _processService;
        public CMDController()
        {
            _processService = new ProcessService();
        }

        [HttpGet]
        [Route("kUBECTLGETPODS")]
        public ContentResult GetPods()
        {
            var table  = _processService.commandExec("kubectl get pods");
            return base.Content(table, "text/html");
        }

        [HttpGet]
        [Route("kUBECTLGETDEPLOY")]
        public ContentResult  GetDeployment()
        {
            var table  = _processService.commandExec("kubectl get deploy");
            return base.Content(table, "text/html");
        }

        [HttpGet]
        [Route("kUBECTLGETSERVICE")]
        public ContentResult  GetService()
        {
            var table  = _processService.commandExec("kubectl get service");
            return base.Content(table, "text/html");
        }
    }
}
