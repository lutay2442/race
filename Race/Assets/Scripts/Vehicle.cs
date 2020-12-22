using UnityEngine;

namespace Race
{
    public abstract class Vehicle
    {

        /// <summary>
        /// Шанс прокола шины
        /// </summary>
        public int TireBlowoutChance;

        protected static float BasicSpeed = 1f;

        public Vehicle(int tireBlowoutChance)
        {
            TireBlowoutChance = tireBlowoutChance;
        }

        /// <summary>
        /// Скорость транспортного средства
        /// </summary>
        public float GetSpeed()
        {
            return BasicSpeed + GetSpeedMod();
        }

        protected abstract float GetSpeedMod();
    }
}
