using Adnet.TestApiMaps.Model;
using Microsoft.AspNetCore.Mvc;
using System.Reflection;

namespace Adnet.TestApiMaps.Controllers
{
	[ApiController]
	[Route( "[controller]/[action]" )]
	public class HomeController : ControllerBase
	{
		private readonly PositionService positionService;

		public HomeController( PositionService positionService )
		{
			this.positionService=positionService;
		}

		[HttpGet]
		public IActionResult GetNetworkObjects()
		{
			return Ok( positionService.geometries );
		}

		[HttpGet]
		public IActionResult GetFireLocations()
		{
			return Ok( new FirePosition[]
			{
				new FirePosition(DangerLevel.EXTREME,44.251516,15.752443),
				new FirePosition(DangerLevel.VERY_HIGH,44.318368,15.534135),
				new FirePosition(DangerLevel.HIGH,44.704626,15.217626),
				new FirePosition(DangerLevel.MODERATE,45.085975,14.591322),
				new FirePosition(DangerLevel.LOW,45.351029,14.965107),
				new FirePosition(DangerLevel.VERY_LOW,45.562952,14.176541),

				new FirePosition(DangerLevel.EXTREME,42.951240,17.085390),
				new FirePosition(DangerLevel.VERY_HIGH,43.300041,16.550391),
				new FirePosition(DangerLevel.HIGH,43.310034,16.624598),
				new FirePosition(DangerLevel.MODERATE,45.226244,18.090191),
				new FirePosition(DangerLevel.LOW,45.454050,17.524017),
				new FirePosition(DangerLevel.VERY_LOW,45.910735,16.039873),
			} );
		}
	}
}