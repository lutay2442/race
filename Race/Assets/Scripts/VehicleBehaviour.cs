using UnityEngine;

namespace Race
{
    public class VehicleBehaviour : MonoBehaviour
    {
        [HideInInspector] public float DistanceFromStart;
        [HideInInspector] public float BlowoutTimer;
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
    }
}