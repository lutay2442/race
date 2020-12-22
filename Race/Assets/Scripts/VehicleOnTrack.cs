namespace Race
{
    public class VehicleOnTrack
    {
        public Vehicle Vehicle;
        public float DistanceFromStart;
        public float BlowoutTimer;

        public VehicleOnTrack(Vehicle vehicle)
        {
            Vehicle = vehicle;
            DistanceFromStart = 0f;
            BlowoutTimer = 0f;
        }
    }
}
