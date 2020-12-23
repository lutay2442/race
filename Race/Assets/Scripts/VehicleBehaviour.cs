using UnityEngine;

namespace Race
{
    public class VehicleBehaviour : MonoBehaviour
    {
        [HideInInspector] public float DistanceFromStart;
        [HideInInspector] public float BlowoutTimer;
        [HideInInspector] public int Place = -1;
        [SerializeField] private MeshRenderer meshRenderer;
        [SerializeField] private int materialIndex;
        [SerializeField] private Material materialPrefab;
        [SerializeField] private Transform model;
        private Material materialCopy;
        private Vehicle vehicle;
        public Vehicle Vehicle
        {
            set
            {
                vehicle = value;
                if (materialCopy != null)
                {
                    materialCopy.color = vehicle.Color;
                }
            }
            get
            {
                return vehicle;
            }
        }

        void Start()
        {
            materialCopy = new Material(materialPrefab);
            var materials = meshRenderer.materials;
            materials[materialIndex] = materialCopy;
            meshRenderer.materials = materials;
            if (vehicle != null)
            {
                materialCopy.color = vehicle.Color;
            }
        }

        public void Offset(float offset)
        {
            model.localPosition = new Vector3(offset, model.localPosition.y, model.localPosition.z);
        }

        public string GetRichtext()
        {
            var placeText = Place == -1 ? "Пройдено: " + DistanceFromStart.ToString("n2") : "Место: " + Place.ToString();
            var color = ColorUtility.ToHtmlStringRGB(Vehicle.Color);
            var tireBlowout = BlowoutTimer > 0 ? "Прокол!" : string.Empty;
            return $"<color=#{color}>{Vehicle.GetName()} {tireBlowout} {placeText} </color>";
        }
    }
}