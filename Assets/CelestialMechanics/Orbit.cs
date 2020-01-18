namespace CelestialMechanics
{
    public class Orbit
    {
        public float OrbitTimeInEarthYears { get; internal set; }
        public float RotationTimeInEarthHours { get; internal set; }
        public int ParentObjectId { get; internal set; }
    }
}
