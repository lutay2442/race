namespace Race
{
    public class Truck: Vehicle
    {
        /// <summary>
        /// Вес груза
        /// </summary>
        public float CargoWeight;
        public static float SpeedPenaltyPerUnitOfWeight = .5f;

        public Truck(float cargoWeight) : base(1)
        {
            CargoWeight = cargoWeight;
        }
        protected override float GetSpeedMod()
        {
            return CargoWeight * SpeedPenaltyPerUnitOfWeight;
        }
    }
}
