using System.Collections.Generic;
using CelestialMechanics;

namespace StaticData
{
    public static class DefaultGalaxies
    {
        #region Planets
        public static CelestialObjectDetails Mercury = new CelestialObjectDetails
        {
            Id = 2,
            Name = "Mercury",
            RadiusKm = 2439.7f,
            Type = ObjectType.Planet,
            Orbit = new Orbit
            {
                OrbitTimeInEarthYears = 365f,
                RotationTimeInEarthHours = 24f
            }
        };

        public static CelestialObjectDetails Venus = new CelestialObjectDetails
        {
            Id = 3,
            Name = "Venus",
            RadiusKm = 6051.8f,
            Type = ObjectType.Planet,
            Orbit = new Orbit
            {
                OrbitTimeInEarthYears = 365f,
                RotationTimeInEarthHours = 24f
            }
        };
        public static CelestialObjectDetails Earth = new CelestialObjectDetails
        {
            Id = 4,
            Name = "Earth",
            RadiusKm = 6371f,
            Type = ObjectType.Planet,
            Orbit = new Orbit
            {
                OrbitTimeInEarthYears = 365f,
                RotationTimeInEarthHours = 24f
            }
        };
        public static CelestialObjectDetails Mars = new CelestialObjectDetails
        {
            Id = 5,
            Name = "Mars",
            RadiusKm = 3389.5f,
            Type = ObjectType.Planet,
            Orbit = new Orbit
            {
                OrbitTimeInEarthYears = 365f,
                RotationTimeInEarthHours = 24f
            }
        };
        public static CelestialObjectDetails Jupiter = new CelestialObjectDetails
        {
            Id = 6,
            Name = "Jupiter",
            RadiusKm = 69911,
            Type = ObjectType.Planet,
            Orbit = new Orbit
            {
                OrbitTimeInEarthYears = 365f,
                RotationTimeInEarthHours = 24f
            }
        };
        public static CelestialObjectDetails Saturn = new CelestialObjectDetails
        {
            Id = 7,
            Name = "Saturn",
            RadiusKm = 58232,
            Type = ObjectType.Planet,
            Orbit = new Orbit
            {
                OrbitTimeInEarthYears = 365f,
                RotationTimeInEarthHours = 24f
            }
        };
        public static CelestialObjectDetails Uranus = new CelestialObjectDetails
        {
            Id = 8,
            Name = "Uranus",
            RadiusKm = 25362,
            Type = ObjectType.Planet,
            Orbit = new Orbit
            {
                OrbitTimeInEarthYears = 365f,
                RotationTimeInEarthHours = 24f
            }
        };
        public static CelestialObjectDetails Neptune = new CelestialObjectDetails
        {
            Id = 9,
            Name = "Neptune",
            RadiusKm = 24622,
            Type = ObjectType.Planet,
            Orbit = new Orbit
            {
                OrbitTimeInEarthYears = 365f,
                RotationTimeInEarthHours = 24f
            }
        };

        #endregion

        #region Super Asteroids
        public static CelestialObjectDetails Pluto = new CelestialObjectDetails
        {
            Id = 10,
            Name = "Pluto",
            RadiusKm = 1188.3f,
            Type = ObjectType.SuperAsteroid,
            Orbit = new Orbit
            {
                OrbitTimeInEarthYears = 365f,
                RotationTimeInEarthHours = 24f
            }
        };
        #endregion

        #region stars
        public static CelestialObjectDetails Sol = new CelestialObjectDetails
        {
            Id = 1,
            Name = "Sol",
            RadiusKm = 695510,
            Type = ObjectType.Star,
            Orbit = new Orbit
            {
                OrbitTimeInEarthYears = 365f,
                RotationTimeInEarthHours = 24f
            },
            GalacticPosition = new CelestialPosition
            {
                X = new CelestialDistance(2.6096e+17f),
                Y = new CelestialDistance(0),
                Z = new CelestialDistance(0)
            }
        };
        #endregion

        #region galaxies
        public static GalaxyDetails TheMilkyWay = new GalaxyDetails
        {
            ObjectDetails = new List<CelestialObjectDetails>
        {
            Sol,
            Mercury,
            Venus,
            Earth,
            Mars,
            Jupiter,
            Saturn,
            Uranus,
            Neptune,
            Pluto
        }
        };
        #endregion
    }
}
