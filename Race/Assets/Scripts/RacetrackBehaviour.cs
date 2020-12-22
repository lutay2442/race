using UnityEngine;

namespace Race
{
    public class RacetrackBehaviour : MonoBehaviour
    {
        [SerializeField] private Transform bottomSection;
        [SerializeField] private Transform extendibleSection;
        [SerializeField] private Transform topSection;
        public void UpdateLength(float length)
        {
            extendibleSection.localScale = new Vector3(1f, 1f, length);
            bottomSection.localPosition = new Vector3(.5f, 0f, -.5f - length / 2);
            topSection.localPosition = new Vector3(.5f, 0f, .5f + length / 2);
        }
    }
}