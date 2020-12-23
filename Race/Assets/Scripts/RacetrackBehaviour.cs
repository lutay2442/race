using UnityEngine;

namespace Race
{
    public class RacetrackBehaviour : MonoBehaviour
    {
        public Racetrack Racetrack;
        public Transform StartPoint;
        [SerializeField] private Transform bottomSection;
        [SerializeField] private Transform extendibleSection;
        [SerializeField] private Transform topSection;

        [SerializeField] private Transform bottomEnterPoint;
        [SerializeField] private Transform bottomMiddlePoint;
        [SerializeField] private Transform bottomExitPoint;

        [SerializeField] private Transform topEnterPoint;
        [SerializeField] private Transform topMiddlePoint;
        [SerializeField] private Transform topExitPoint;
        private void Awake()
        {
            UpdateLength(Racetrack.Length);
        }
        public void UpdateLength(float length)
        {
            Racetrack.Length = length;
            var l = (length - (4f * TurnDistance()));
            extendibleSection.localScale = new Vector3(1f, 1f, l);
            bottomSection.localPosition = new Vector3(.5f, -0.2f, -.5f - l / 2f);
            topSection.localPosition = new Vector3(.5f, -0.2f, .5f + l / 2f);
        }
        public float TurnDistance()
        {
            return Vector3.Distance(bottomEnterPoint.position, bottomMiddlePoint.position);
        }
        public float StraightDistance()
        {
            return Vector3.Distance(bottomExitPoint.position, topEnterPoint.position);
        }
        public void SetToWorldPosition(float distanceFromStart, Transform vehicleTransform)
        {
            var straightDistance = StraightDistance();
            var turnDistance = TurnDistance();
            var sum = straightDistance + turnDistance * 2;
            var progress = distanceFromStart / Racetrack.Length;
            var firstSection = (straightDistance / sum) / 2;
            var secondSecton = firstSection + turnDistance / sum / 2;
            var thirdSecton = secondSecton + turnDistance / sum / 2;

            if (progress <= firstSection)
            {
                vehicleTransform.rotation = Quaternion.Lerp(bottomExitPoint.rotation, topEnterPoint.rotation, progress / firstSection);
                vehicleTransform.position = Vector3.Lerp(bottomExitPoint.position, topEnterPoint.position, progress / firstSection);
                return;
            }
            else if (progress <= secondSecton)
            {
                vehicleTransform.rotation = Quaternion.Lerp(topEnterPoint.rotation, topMiddlePoint.rotation, progress / firstSection);
                vehicleTransform.position = Vector3.Lerp(topEnterPoint.position, topMiddlePoint.position, (progress - firstSection) / (secondSecton - firstSection));
                return;
            }
            else if (progress <= thirdSecton)
            {
                vehicleTransform.rotation = Quaternion.Lerp(topMiddlePoint.rotation, topExitPoint.rotation, progress / firstSection);
                vehicleTransform.position = Vector3.Lerp(topMiddlePoint.position, topExitPoint.position, (progress - secondSecton) / (thirdSecton - secondSecton));
                return;
            }
            else if (progress <= firstSection * 2)
            {
                vehicleTransform.rotation = Quaternion.Lerp(topExitPoint.rotation, bottomEnterPoint.rotation, progress / firstSection);
                vehicleTransform.position = Vector3.Lerp(topExitPoint.position, bottomEnterPoint.position, (progress - thirdSecton) / (firstSection * 2 - thirdSecton));
                return;
            }
            else if (progress <= secondSecton * 2)
            {
                vehicleTransform.rotation = Quaternion.Lerp(bottomEnterPoint.rotation, bottomMiddlePoint.rotation, progress / firstSection);
                vehicleTransform.position = Vector3.Lerp(bottomEnterPoint.position, bottomMiddlePoint.position, (progress - firstSection * 2) / (secondSecton * 2 - firstSection * 2));
                return;
            }
            else if (progress <= thirdSecton * 2)
            {
                vehicleTransform.rotation = Quaternion.Lerp(bottomMiddlePoint.rotation, bottomExitPoint.rotation, progress / firstSection);
                vehicleTransform.position = Vector3.Lerp(bottomMiddlePoint.position, bottomExitPoint.position, (progress - secondSecton * 2) / (thirdSecton * 2 - secondSecton * 2));
                return;
            }
        }
    }
}