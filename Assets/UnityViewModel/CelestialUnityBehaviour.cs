using System;
using System.Collections.Generic;
using System.Linq;
using CelestialMechanics;
using UnityEngine;

namespace UnityAdaptors
{
    public class CelestialUnityBehaviour : MonoBehaviour
{
    public GameObject StarPrefab;
    public GameObject PlanetPrefab;
    public GameObject SuperAsteroidPrefab;
    public int ObjectScale = 1;
    private int oldObjectScale = 0;
    public int KilometersToUnit = 50000;
    public float MaxKilometersDrawn = 5000000;
    private int oldKilometersToUnit = 0;

    // Start is called before the first frame update
    void Start()
    {
    }

    void Update()
    {
        if(ObjectScale != oldObjectScale || KilometersToUnit != oldKilometersToUnit)
        {
            var solarSystem = CreateOurSolarSystem();
            var gameObjects = new List<GameObject>();

            foreach (var celestialObject in solarSystem.Objects)
            {
                var instance = GetOrCreateInstance(celestialObject);
                TransformInstance(celestialObject, instance);
            }
            

            oldObjectScale = ObjectScale;
            oldKilometersToUnit = KilometersToUnit;
        }
    }

    private GameObject GetOrCreateInstance(CelestialUnityObject celestialObject)
    {
        return GameObject.Find(celestialObject.Name) ?? CreateGameObjectInGame(celestialObject);
    }

    private GameObject CreateGameObjectInGame(CelestialUnityObject celestialObject)
    {
        switch (celestialObject.Type)
        {
            case ObjectType.Star : return Instantiate(StarPrefab);
            case ObjectType.Planet : return Instantiate(PlanetPrefab);
            case ObjectType.SuperAsteroid : return Instantiate(SuperAsteroidPrefab);
            default: return null;
        }
    }

    private void TransformInstance(CelestialUnityObject celestialObject, GameObject instance)
    {
        instance.name = celestialObject.Name;
        SetOrbit(celestialObject.Orbit, instance);
        instance.transform.position = celestialObject.Position;
        instance.transform.localScale = celestialObject.Size;
        
        var flameEffect = instance.GetComponentsInChildren<Component>().FirstOrDefault(c => c.name == "FlameEffect");
        if (flameEffect != null)
        {
            flameEffect.transform.localScale = celestialObject.Size;
        }
    }

    private void SetOrbit(UnityOrbit orbit, GameObject instance)
    {
        var script = instance.GetComponentInChildren<UnityOrbitter>();
        script.OrbitDetails = orbit;
    }

    public CelestialUnityScene CreateOurSolarSystem()
    {
        var centreObject = StaticData.DefaultGalaxies.TheMilkyWay.ObjectDetails.FirstOrDefault(o => o.Id == 1);
        var celestialObjects = CreateCelestialObjects(centreObject, StaticData.DefaultGalaxies.TheMilkyWay.ObjectDetails).ToList();        

        return new CelestialUnityScene
        {
            Objects = celestialObjects
        };
    }

    private IEnumerable<CelestialUnityObject> CreateCelestialObjects(CelestialObjectDetails centreObject, IEnumerable<CelestialObjectDetails> objectDetails)
    {
        if (centreObject == null)
        {
            return new List<CelestialUnityObject>();
        }

        var maxXPosition = centreObject.GalacticPosition.X + MaxKilometersDrawn;
        var maxYPosition = centreObject.GalacticPosition.Y + MaxKilometersDrawn;
        var maxZPosition = centreObject.GalacticPosition.Z + MaxKilometersDrawn;
        var objectsToRender = objectDetails.Where(od => 
            od.GalacticPosition.X <= maxXPosition
            && od.GalacticPosition.Y <= maxYPosition
            && od.GalacticPosition.Z <= maxZPosition);
            
        if (!objectsToRender.Any())
        {
            return new List<CelestialUnityObject>();
        }

        var orbitParentIds = objectsToRender.Select(o => o.Orbit.ParentObjectId);
        objectsToRender = objectsToRender.Union(objectDetails.Where(od => orbitParentIds.Contains(od.Id) && !objectsToRender.Any(otr => otr.Id == od.Id)));
        var unityObjects = objectsToRender.Select(o => CreateObject(o, centreObject, KilometersToUnit));

        foreach(var orbittingobject in objectsToRender.Where(o => o.Orbit?.ParentObjectId > 0))
        {
            var unityObject = unityObjects.FirstOrDefault(uo => uo.CelestialObjectId == orbittingobject.Id);
            var unityParentObject = unityObjects.FirstOrDefault(uo => uo.CelestialObjectId == orbittingobject.Orbit.ParentObjectId);
            
            if (unityObject != null && unityParentObject != null && unityObject.Orbit != null)
            {
                unityObject.Orbit.ParentGameObjectName = unityParentObject.Name;
            }
        }

        return unityObjects.ToList();
    }

    private CelestialUnityObject CreateObject(CelestialObjectDetails objectDetails, float kmToUnit, Vector3 position)
    {
        var diameterAsScale = ScaleObject(objectDetails, kmToUnit);
        var size = new Vector3(diameterAsScale, diameterAsScale, diameterAsScale);

        return new CelestialUnityObject
        {
            Name = objectDetails.Name,
            CelestialObjectId = objectDetails.Id,
            Position = position,
            Size = size,
            Type = objectDetails.Type
        };
    }

    private float ScaleObject(CelestialObjectDetails objectDetails, float kmToUnit)
    {
        return (float)((objectDetails.RadiusKm/kmToUnit)*2)*ObjectScale;
    }

    private CelestialUnityObject CreateObject(CelestialObjectDetails childObjectDetails, CelestialObjectDetails centreObject, float kmToUnit)
    {
        var relativeToCentre = centreObject.GalacticPosition-childObjectDetails.GalacticPosition;
        var position = GetPositionInSelectedUnits(relativeToCentre);
         
        return CreateObject(childObjectDetails, kmToUnit, position);
    }

    private Vector3 GetPositionInSelectedUnits(CelestialPosition relativeToCentre)
    {
        throw new NotImplementedException();
    }

    private float CalculateX(double distanceFromSurfaceKm, double radiusKm1, double radiusKm2, double kmToUnit)
    {
        return (float)((distanceFromSurfaceKm+radiusKm1+radiusKm2)/kmToUnit);
    }
}
}