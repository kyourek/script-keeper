using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Ajax;

using Keeper.OfScripts.Example.Models;

namespace Keeper.OfScripts.Example.Controllers
{
	[HandleError]
	public class HomeController : Controller
	{
		public ActionResult Index()
		{
			var model = new List<VideoModel>();
			
			model.Add(new VideoModel
			{
				Id = "video1",
				Source = Url.Content("~/Content/Videos/20051210-w50s.flv")
			});
			
			model.Add(new VideoModel
			{
				Id = "video2",
				Source = Url.Content("~/Content/Videos/barsandtone.flv")
			});
			
			model.Add(new VideoModel
			{
				Id = "video3",
				Source = Url.Content("~/Content/Videos/flowplayer-700.flv")
			});			
			
			return View(model);
		}
	}
}

