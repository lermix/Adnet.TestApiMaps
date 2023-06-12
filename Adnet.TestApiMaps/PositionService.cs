using Adnet.TestApiMaps.Model;
using System.Reflection;

namespace Adnet.TestApiMaps
{
    public class PositionService : IHostedService
	{
		public List<Geometry>? geometries;

		public Task StartAsync( CancellationToken cancellationToken )
		{
			var assembly = Assembly.GetExecutingAssembly();
			var resourceName = "Adnet.TestApiMaps.Data.EES_postrojenja_i_vodovi.csv";

			using ( Stream stream = assembly.GetManifestResourceStream( resourceName ) )
			using ( StreamReader reader = new StreamReader( stream ) )
			{
				GeometryCsvReader geometryCsvReader = new GeometryCsvReader();
				geometries = geometryCsvReader.Read( reader );		
			}

			return Task.CompletedTask;
		}

		public Task StopAsync( CancellationToken cancellationToken )
		{
			return Task.CompletedTask;
		}
	}
}
