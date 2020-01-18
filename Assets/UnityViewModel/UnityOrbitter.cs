

using UnityEngine;

namespace UnityAdaptors
{
    public class UnityOrbitter : MonoBehaviour
    {
        public UnityOrbit OrbitDetails = new UnityOrbit();
        private Transform Parent;
        public int TimeMultiplier = 200000;
        public string Name = "";
        public enum Season { Auto, Manual }
        public Season SetSeason;
        public bool KeepTime;
        private Transform ThisTransform;
        private float EarthDays = 365.242199f;

        // Planetary Stats
        public float AxialTilt;
        public int HoursInDay;
        public int RotInOrbit;

        //Planet Counters
        public int CounterYear;
        public int CounterDay;
        public int CounterHour;
        public int CounterMinute;
        public float CounterSecond;
        public float CurrentOrbitPos;
        public bool OrbitOffSetYear;
        private float RotCounter;
        private float OrbCounter;

        //Draw Orbit
        public bool _DrawOrbit = false;
        public float DisplaySize = 0.05f;
        public Color DisplayColor = Color.blue;
        public int Segments = 100;
        public Texture2D DisplayTexture;
        int DisplayTiling = 50;
        public bool UseTexture;
        private Transform ThisOrbit;
        private LineRenderer LR = new LineRenderer();

        private Transform OrbitCenter;

        public UnityOrbitter()
        {

        }

        public void Start()
        {
            ThisTransform = transform;
            Parent = GameObject.Find(OrbitDetails.ParentGameObjectName)?.transform;
            var z = ThisTransform.localEulerAngles.z;
            ThisTransform.localEulerAngles = ThisTransform.localEulerAngles.Copy(z: AxialTilt);

            OrbitCenter = new GameObject("OrbitCenter").transform;
            OrbitCenter.position = Parent.position;
            ThisTransform.parent = OrbitCenter;
            ThisTransform.localPosition = new Vector3(0, 0, OrbitDetails.OrbitalDistance);
            OrbitCenter.eulerAngles = ThisTransform.localEulerAngles.Copy(OrbitDetails.OrbitAngle);
            OrbitCenter.Rotate(0, OrbitDetails.OrbitPosOffset, 0);

            if (_DrawOrbit)
            {
                if (GameObject.Find("Orbits") == null)
                {
                    var Orbits = new GameObject("Orbits");
                    Orbits.transform.position = Vector3.zero;
                    Orbits.transform.eulerAngles = Vector3.zero;
                }
            }

            SetupPlanet();

            if (OrbitOffSetYear)
            {
                OrbCounter = OrbitDetails.OrbitPosOffset;
            }
            if (OrbitDetails.LockOrbit)
            {
                KeepTime = false;
            }
            if (_DrawOrbit)
            {
                SetupDrawOrbit();
            }
        }

        public void SetupDrawOrbit()
        {
            var Orbit = new GameObject("Orbit_Path");
            Orbit.transform.eulerAngles.Copy(OrbitDetails.OrbitAngle);
            Orbit.transform.parent = GameObject.Find("Orbits").transform;
            Orbit.transform.position = Parent.position;
            Orbit.AddComponent<LineRenderer>();
            LR = Orbit.GetComponent<LineRenderer>();
            LR.startWidth = DisplaySize;
            LR.material.shader = Shader.Find("Particles/Additive");
            LR.material.SetColor("_TintColor", DisplayColor);
            LR.useWorldSpace = false;
            LR.positionCount = Segments + 1;

            if (DisplayTexture != null)
            {
                LR.material.mainTexture = DisplayTexture;
                LR.material.mainTextureScale = LR.material.mainTextureScale.Copy(DisplayTiling);
            }

            ThisOrbit = Orbit.transform;

            float Angle = 0;
            for (var i = 0; i < Segments + 1; i++)
            {
                var NewRadius = new Vector2(Mathf.Sin(Mathf.Deg2Rad * Angle) * OrbitDetails.OrbitalDistance,
                                                                    Mathf.Cos(Mathf.Deg2Rad * Angle) * OrbitDetails.OrbitalDistance);

                LR.SetPosition(i, new Vector3(NewRadius.y, 0, NewRadius.x));
                Angle += 360.0f / Segments;
            }
        }

        public void SetupPlanet()
        {
            //Setup Orbit Time
            if (OrbitDetails.SetOrbit == UnityOrbitType.Auto)
            {
                OrbitDetails.OrbitalTime = EarthDays * OrbitDetails.OrbitalPeriod * 24 * 60 * 60;
                OrbitDetails.OrbitalDegSec = 360 / OrbitDetails.OrbitalTime * TimeMultiplier;
            }
            else
            {
                OrbitDetails.OrbitalPeriod = 0;
                OrbitDetails.OrbitalTime = (((OrbitDetails.OrbitYears * EarthDays + OrbitDetails.OrbitDays) * 24 + OrbitDetails.OrbitHours) * 60 + OrbitDetails.OrbitMinutes) * 60 + OrbitDetails.OrbitSeconds;
                OrbitDetails.OrbitalDegSec = 360 / OrbitDetails.OrbitalTime * TimeMultiplier;
            }

            //Setup Rotation Time
            if (!OrbitDetails.TidalLock)
            {
                if (OrbitDetails.SetRotation == UnityRotationType.Auto)
                {
                    OrbitDetails.RotationTime = 24 * OrbitDetails.RotationPeriod * 60 * 60;
                    OrbitDetails.RotationDegSec = 360 / OrbitDetails.OrbitalTime * TimeMultiplier;
                }
                else
                {
                    OrbitDetails.RotationPeriod = 0;
                    OrbitDetails.RotationTime = (((OrbitDetails.RotationYears * EarthDays + OrbitDetails.RotationDays) * 24 + OrbitDetails.RotationHours) * 60 + OrbitDetails.RotationMinutes) * 60 + OrbitDetails.RotationSeconds;
                }
                OrbitDetails.RotationDegSec = 360 / OrbitDetails.RotationTime * TimeMultiplier;
                RotInOrbit = (int)System.Math.Round(OrbitDetails.OrbitalTime / OrbitDetails.RotationTime);
                HoursInDay = (int)(OrbitDetails.RotationTime / 60 / 60);
            }
        }

        public void Update()
        {
            if (!OrbitDetails.LockOrbit)
            {
                var ODS = OrbitDetails.OrbitalDegSec * Time.deltaTime;
                OrbitDetails.OrbitStartPos += ODS;
                OrbitCenter.Rotate(0, ODS, 0);

                // Update Rotation
                if (OrbitDetails.TidalLock)
                {
                    ThisTransform.LookAt(Parent);
                    if (KeepTime)
                    {
                        UpdateCounters(0, ODS);
                    }
                }
                else
                {
                    var RotDegSec = OrbitDetails.RotationDegSec * Time.deltaTime;
                    if (KeepTime)
                    {
                        UpdateCounters(RotDegSec, ODS);
                    }
                    ThisTransform.Rotate(0, RotDegSec, 0, Space.Self);
                }
            }
        }

        public void UpdateCounters(float RotDegSec, float ODS)
        {

            //Count Orbits / Years
            if (OrbCounter + ODS >= 360)
            {
                CounterYear += 1;
                CounterDay = 0;
                OrbCounter = OrbCounter + ODS - 360;
            }
            else
            {
                OrbCounter += ODS;
            }

            CurrentOrbitPos = OrbCounter;

            //Count Days	
            if (RotCounter + RotDegSec >= 360)
            {
                CounterDay += 1;
                RotCounter = RotCounter + RotDegSec - 360;
            }
            else
            {
                RotCounter += RotDegSec;
            }

            var CurrentTime = RotCounter / 360 * OrbitDetails.RotationTime;

            //Count Hours
            CounterHour = (int)(CurrentTime / 60) / 60;

            //Count Minutes	
            if (CounterHour > 0)
            {
                CounterMinute = (int)(CurrentTime / 60) - CounterHour * 60;
            }
            else
            {
                CounterMinute = (int)(CurrentTime / 60);
            }

            //Count Seconds
            if (CounterHour > 0 && CounterMinute > 0)
            {
                CounterSecond = CurrentTime - (CounterMinute + CounterHour * 60) * 60;
            }
            else if (CounterHour > 0 && CounterMinute == 0)
            {
                CounterSecond = CurrentTime - CounterHour * 60 * 60;
            }
            else if (CounterHour == 0 && CounterMinute > 0)
            {
                CounterSecond = CurrentTime - CounterMinute * 60;
            }
            else if (CounterHour == 0 && CounterMinute == 0)
            {
                CounterSecond = CurrentTime;
            }
        }

        public void LateUpdate()
        {
            var CurPos = Parent.position + new Vector3(OrbitDetails.OrbitOffset.x, OrbitDetails.OrbitOffset.y, OrbitDetails.OrbitOffset.z);
            OrbitCenter.position = CurPos;
            if (_DrawOrbit)
            {
                ThisOrbit.position = CurPos;
            }
        }
    }
}