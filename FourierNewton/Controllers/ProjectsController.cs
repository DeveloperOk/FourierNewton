using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FourierNewton.Common;
using FourierNewton.Models;
using Microsoft.AspNetCore.Mvc;

namespace FourierNewton.Controllers
{
    public class ProjectsController : Controller
    {

        public IActionResult Info()
        {

            var projectsInfoViewModel = new ProjectsInfoViewModel();
            projectsInfoViewModel.ApplicationLocationUrl = ProjectConstants.ApplicationLocationUrl;
            projectsInfoViewModel.ApplicationLocationFreeUrl = ProjectConstants.ApplicationLocationFreeUrl;

            return View(projectsInfoViewModel);

        }

    }
}
