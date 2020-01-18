using CelestialMechanics;
using UnityEngine;

namespace UnityAdaptors
{
    public class CelestialUnityObject
    {
        public Vector3 Position { get; set; }
        public Vector3 Size { get; set; }
        public string Name { get; set; }
        public int CelestialObjectId { get; set; }
        public ObjectType Type { get; set; }
        public UnityOrbit Orbit { get; internal set; }
    }
}
