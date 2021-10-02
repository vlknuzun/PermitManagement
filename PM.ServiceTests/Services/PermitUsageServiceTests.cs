using Microsoft.VisualStudio.TestTools.UnitTesting;
using PM.Service.Services;
using System;
using System.Collections.Generic;
using System.Text;

namespace PM.Service.Services.Tests
{
    [TestClass()]
    public class PermitUsageServiceTests
    {
        
        [TestMethod()]
        public void PreparePermitTest()
        {
            PermitUsageService permitUsageService = new PermitUsageService();
            permitUsageService.PreparePermit();
        }


    }
}