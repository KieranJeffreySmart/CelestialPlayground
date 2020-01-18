using UnityEngine;
namespace UnityAdaptors
{
    using CelestialMechanics;
    
    public static class UnityExtensions
    {
        public static Vector3 Copy(this Vector3 self, float? x = null, float? y = null, float? z = null)
        {
            return new Vector3(x ?? self.x, y ?? self.y, z ?? self.z);
        }
        
        public static Vector2 Copy(this Vector2 self, float? x = null, float? y = null)
        {
            return new Vector2(x ?? self.x, y ?? self.y);
        }

        public static UnityOrbit ToUnityOrbit(this Orbit orbit)
        {
            return new UnityOrbit
            {
                OrbitalPeriod = orbit.OrbitTimeInEarthYears,
                RotationPeriod = orbit.RotationTimeInEarthHours
            };
        }
    }
}