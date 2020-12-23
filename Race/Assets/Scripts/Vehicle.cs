using UnityEngine;

namespace Race
{
    public abstract class Vehicle
    {

        /// <summary>
        /// Шанс прокола шины
        /// </summary>
        public int TireBlowoutChance;
        public Color Color;
        protected static float BasicSpeed = .1f;

        public Vehicle(int tireBlowoutChance)
        {
            Color = Random.ColorHSV(0f, 1f, 1f, 1f, 0.5f, 1f);
            TireBlowoutChance = tireBlowoutChance;
        }

        /// <summary>
        /// Скорость транспортного средства
        /// </summary>
        public float GetSpeed()
        {
            return Mathf.Max(0.01f, BasicSpeed + GetSpeedMod());
        }
        public abstract string GetName();

        protected abstract float GetSpeedMod();
    }
}
