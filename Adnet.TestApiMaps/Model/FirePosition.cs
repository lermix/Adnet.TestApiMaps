namespace Adnet.TestApiMaps.Model
{
	public class FirePosition
	{
		public FirePosition( DangerLevel dangerLevel, double latitude, double longitude )
		{
			DangerLevel=dangerLevel;
			Latitude=latitude;
			Longitude=longitude;
		}

		public DangerLevel DangerLevel { get; set; }
		public double Latitude { get; set; }
		public double Longitude { get; set; }
	}
}
