namespace CelestialMechanics
{
using System.Collections.Generic;

public class GalaxyDetails
{
    public Orbit Orbit { get; set; }
    public IEnumerable<CelestialObjectDetails> ObjectDetails { get; set; }
}
}