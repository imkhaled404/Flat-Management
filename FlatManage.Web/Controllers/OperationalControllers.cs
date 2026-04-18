using Microsoft.AspNetCore.Mvc;

namespace FlatManage.Web.Controllers
{
    public class TenantController : Controller
    {
        public IActionResult Index() => View();
        public IActionResult Details(int id) => View();
    }

    public class BillController : Controller
    {
        public IActionResult Index() => View();
    }

    public class TicketController : Controller
    {
        public IActionResult Index() => View();
    }

    public class VisitorController : Controller
    {
        public IActionResult Index() => View();
    }

    public class ReportController : Controller
    {
        public IActionResult Index() => View();
    }
}
