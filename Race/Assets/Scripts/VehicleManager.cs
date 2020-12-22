using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Race
{
    public class VehicleManager : MonoBehaviour
    {
        [SerializeField] private VehicleEvent onVehicleGenerated;
        [SerializeField] private Transform startPoint;
        [SerializeField] private VehicleBehaviour carPrefab;
        [SerializeField] private VehicleBehaviour motorcyclePrefab;
        [SerializeField] private VehicleBehaviour truckPrefab;

        [SerializeField] private float roadWidth;

        private List<VehicleBehaviour> vehicleBehaviours;
        private void Awake()
        {
            vehicleBehaviours = new List<VehicleBehaviour>();
        }
        private void Start()
        {
            GenerateMotorcycle();
            GenerateCar();
            GenerateTruck();
            GenerateTruck();
            GenerateMotorcycle();
            GenerateTruck();
        }
        public void GenerateCar()
        {
            GenerateVehicle(carPrefab, new Car(1));
        }
        public void GenerateMotorcycle()
        {
            GenerateVehicle(motorcyclePrefab, new Motorcycle(false));
        }

        public void GenerateTruck()
        {
            GenerateVehicle(truckPrefab, new Truck(1));
        }
        private void GenerateVehicle(VehicleBehaviour prefab, Vehicle vehicle)
        {
            var behaviour = Instantiate(prefab, startPoint.localPosition, Quaternion.identity);
            behaviour.Vehicle = vehicle;
            vehicleBehaviours.Add(behaviour);
            for (int i = 0; i < vehicleBehaviours.Count; i++)
            {
                vehicleBehaviours[i].Offset(-roadWidth / 2 + (i + 1) * roadWidth / (vehicleBehaviours.Count));
            }
            if (onVehicleGenerated != null)
            {
                onVehicleGenerated.Invoke(vehicle);
            }
        }
    }
}