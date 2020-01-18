namespace CelestialMechanics
{
    public class CelestialPosition
{
    public CelestialDistance X {get; set;}
    public CelestialDistance Y {get; set;}
    public CelestialDistance Z {get; set;}
    public static CelestialPosition operator +(CelestialPosition a, CelestialPosition b) => new CelestialPosition { X = a.X+b.X, Y = a.Y+b.Y, Z = a.Z+a.Z};
    public static CelestialPosition operator -(CelestialPosition a, CelestialPosition b) => new CelestialPosition { X = a.X-b.X, Y = a.Y-b.Y, Z = a.Z-a.Z};

}

public class CelestialDistance
{
    float kilometers = 0;
    public float Kilometers => kilometers;
    public float LightYears => kilometers/10000000000000;
    public float Au => kilometers/147100000;
    public float Parsecs => LightYears/3.262f;
    public CelestialDistance(float kilometers)
    {
        this.kilometers = kilometers;
    }

    public static CelestialDistance operator +(CelestialDistance a, CelestialDistance b) => new CelestialDistance(a.Kilometers+b.Kilometers);
    public static CelestialDistance operator -(CelestialDistance a, CelestialDistance b) => new CelestialDistance(a.Kilometers-b.Kilometers);
    public static bool operator <(CelestialDistance a, CelestialDistance b) => a.Kilometers < b.Kilometers;
    public static bool operator >(CelestialDistance a, CelestialDistance b) => a.Kilometers > b.Kilometers;
    public static bool operator <=(CelestialDistance a, CelestialDistance b) => a.Kilometers <= b.Kilometers;
    public static bool operator >=(CelestialDistance a, CelestialDistance b) => a.Kilometers >= b.Kilometers;

    public static CelestialDistance operator +(CelestialDistance a, float b) => new CelestialDistance(a.Kilometers+b);
    public static CelestialDistance operator -(CelestialDistance a, float b) => new CelestialDistance(a.Kilometers-b);
    public static bool operator <(CelestialDistance a, float b) => a.Kilometers < b;
    public static bool operator >(CelestialDistance a, float b) => a.Kilometers > b;
    public static bool operator <=(CelestialDistance a, float b) => a.Kilometers <= b;
    public static bool operator >=(CelestialDistance a, float b) => a.Kilometers >= b;

    public static CelestialDistance operator +(float a, CelestialDistance b) => new CelestialDistance(a+b.Kilometers);
    public static CelestialDistance operator -(float a, CelestialDistance b) => new CelestialDistance(a-b.Kilometers);
    public static bool operator <(float a, CelestialDistance b) => a < b.Kilometers;
    public static bool operator >(float a, CelestialDistance b) => a > b.Kilometers;
    public static bool operator <=(float a, CelestialDistance b) => a <= b.Kilometers;
    public static bool operator >=(float a, CelestialDistance b) => a >= b.Kilometers;

    public override bool Equals(object obj)
    {
        if (obj is CelestialDistance distance)
        {
            return distance.Kilometers == this.kilometers;
        }

        return false;
    }

    public override int GetHashCode()
    {
        int hashCode = -2074659052;
        hashCode = hashCode * -1521134295 + kilometers.GetHashCode();
        return hashCode;
    }
}
}