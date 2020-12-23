namespace Race
{
    public class Motorcycle : Vehicle
    {
        /// <summary>
        /// Наличие коляски
        /// </summary>
        public bool HasSidecar;
        protected static float SpeedPenaltyForSidecar = -.2f;

        public Motorcycle(bool hasSidecar) : base(7)
        {
            HasSidecar = hasSidecar;
        }
        protected override float GetSpeedMod()
        {
            return HasSidecar? SpeedPenaltyForSidecar : 0f;
        }

        public override string GetName()
        {
            return "Мотоцикл";
        }
    }
}