namespace Adnet.TestApiMaps.Model
{
    public class Geometry
    {
        public int Id { get; set; }
        public Guid NMObjectExternalId { get; set; }
        public string ExternalName { get; set; }
        public GeometryType GeometryType { get; set; }
        public List<GeometryCoordinate> GeometryCoordinates { get; set; } = new List<GeometryCoordinate>();
    }
}
