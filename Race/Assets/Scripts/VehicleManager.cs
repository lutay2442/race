using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Race
{
    public class VehicleManager : MonoBehaviour
    {
        [SerializeField] private VehicleEvent onVehicleGenerated;
        [SerializeField] private VehicleBehaviour carPrefab;
        [SerializeField] private VehicleBehaviour motorcyclePrefab;
        [SerializeField] private VehicleBehaviour truckPrefab;
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
            var behaviour = Instantiate(prefab);
            behaviour.Vehicle = vehicle;
            if (onVehicleGenerated != null)
            {
                onVehicleGenerated.Invoke(behaviour);
            }
        }
    }
}