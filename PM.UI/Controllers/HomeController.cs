using Microsoft.AspNetCore.Mvc;
using PM.Data.Entity;
using PM.Service.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PM.UI.Controllers
{
    public class HomeController : Controller
    {
        private readonly IMemberService _memberService;
        public HomeController(IMemberService memberService)
        {
            _memberService = memberService;
        }

        [HttpGet]
        public IEnumerable<Member> Index()
        
        {
            return _memberService.GetMembers();
        }
    }
}
