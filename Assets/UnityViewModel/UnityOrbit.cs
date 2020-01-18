using UnityEngine;

namespace UnityAdaptors
{		
	public class UnityOrbit
	{
			public string ParentGameObjectName;
			public UnityOrbitType SetOrbit;
			public UnityRotationType SetRotation;
			public bool TidalLock;
			public bool LockOrbit;
			public float OrbitAngle;        
			// Orbit Stats
			public float OrbitalPeriod = 1.0f; // Earth Years
			public float OrbitalDistance = 2;
			public Vector3 OrbitOffset;
			public float OrbitPosOffset;
			public float OrbitStartPos;
			public int OrbitYears;
			public int OrbitDays;
			public int OrbitHours;
			public int OrbitMinutes;
			public float OrbitSeconds;
			public float OrbitalTime;
			public float OrbitalDegSec;
			//Rotation Stats
			public float RotationOffset;
			public float RotationPeriod; // Earth Hours
			public int RotationYears;
			public int RotationDays;
			public int RotationHours;
			public int RotationMinutes;
			public float RotationSeconds;
			public float RotationDegSec;
			public float RotationTime;
	}
}