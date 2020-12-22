namespace Race
{
    public class Car : Vehicle
    {
        /// <summary>
        /// Количество людей в машине
        /// </summary>
        public int NumberOfPeople;
        protected static float SpeedPenaltyPerPerson = 1f;

        public Car(int numberOfPeople) : base(3)
        {
            NumberOfPeople = numberOfPeople;
        }
        protected override float GetSpeedMod()
        {
            return SpeedPenaltyPerPerson * NumberOfPeople;
        }
    }
}