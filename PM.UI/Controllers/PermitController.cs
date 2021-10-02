using Microsoft.AspNetCore.Mvc;
using PM.Data.Entity;
using PM.Service.Models;
using PM.Service.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PM.UI.Controllers
{
    public class PermitController : Controller
    {
        private readonly IPermitUsageService _permitUsageService;
        private readonly IMemberService _memberService;

        public PermitController(IPermitUsageService permitUsageService, IMemberService memberService)
        {
            _permitUsageService = permitUsageService;
            _memberService = memberService;
        }
        [HttpGet]
        public IActionResult GetPermits()
        {
           
            return Json(_permitUsageService.GetCurretPermits());
        }
        [HttpPost]
        public IActionResult GetDisributePermits()
        {
            

            return Json(_permitUsageService.DistributeLeaves());
        }

    }
}
