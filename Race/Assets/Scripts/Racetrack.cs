using UnityEngine;

namespace Race
{
    [System.Serializable]
    public class Racetrack
    {
        /// <summary>
        /// Длина трассы
        /// </summary>
        [Range(3f, 10f)] public float Length;
        public float RoadWidth;
    }
}