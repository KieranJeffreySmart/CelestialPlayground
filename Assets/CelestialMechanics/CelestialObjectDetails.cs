
namespace CelestialMechanics
{
public class CelestialObjectDetails
{
    public string Name { get; set; }
    public double RadiusKm { get; set; }
    public int Id { get; internal set; }
    public Orbit Orbit { get; set; }
    public float Gravity { get; set; }
    public ObjectType Type { get; internal set; }
    public CelestialPosition SolarPosition { get; internal set; }
    public CelestialPosition GalacticPosition { get; internal set; }
}
}