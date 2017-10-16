using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PusherServer;

namespace HeyChat.Controllers
{
    public class ChatController : Controller
    {
        public ActionResult Index()
        {
			if (Session["user"] == null) {
				return Redirect("/");
			}

            ViewBag.currentUser = Session["user"];

            return View ();
        }

        [HttpPost]
        public ActionResult Typing()
        {
            string typer        = Request.Form["typer"];
            string socket_id    = Request.Form["socket_id"];

			var options = new PusherOptions();
			options.Cluster = "mt1";

			var pusher = new Pusher(
			"344451",
	        "913866e45b4b1e9daf1a",
	        "6342ac2220f239d94d94", options);

			pusher.TriggerAsync(
			"chat",
			"typing",
                new { typer = typer },
                new TriggerOptions() { SocketId = socket_id });

            return new HttpStatusCodeResult(200);
		}

        public JsonResult SendMessage(string user)
        {
            return Json(""); 
        }
    }
}
