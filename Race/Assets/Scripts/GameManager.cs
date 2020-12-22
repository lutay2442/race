using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Race
{
    public class GameManager : MonoBehaviour
    {
        [SerializeField] private float refreshRate = 0.1f;
        [SerializeField] private float blowoutTimeout = 1f;
        [SerializeField] private Racetrack racetrack;
        private List<Vehicle> vehicles;
        private List<VehicleOnTrack> vehiclesOnTrack;

        private void Awake()
        {
            vehicles = new List<Vehicle>();
        }
        public void AddVehicle(Vehicle vehicle)
        {
            vehicles.Add(vehicle);
        }
        public void StartLoop()
        {
            vehiclesOnTrack = new List<VehicleOnTrack>();
            vehicles.ForEach(x => vehiclesOnTrack.Add(new VehicleOnTrack(x)));
            StartCoroutine(LoopCoroutine());
        }
        private IEnumerator LoopCoroutine()
        {
            while (true)
            {
                if (vehiclesOnTrack.Any(x => x.DistanceFromStart < racetrack.Length))
                {
                    foreach (var vehicleOnTrack in vehiclesOnTrack.Where(x => x.DistanceFromStart < racetrack.Length))
                    {
                        if (vehicleOnTrack.BlowoutTimer > 0)
                        {
                            vehicleOnTrack.BlowoutTimer -= refreshRate;
                        }
                        else
                        {
                            if (Random.Range(0, 100) <= vehicleOnTrack.Vehicle.TireBlowoutChance)
                            {
                                vehicleOnTrack.BlowoutTimer += blowoutTimeout;
                            }
                            else
                            {
                                vehicleOnTrack.DistanceFromStart += vehicleOnTrack.Vehicle.GetSpeed();
                            }
                        }
                    }
                }
                else
                {
                    EndLoop();
                    yield break;
                }
                yield return new WaitForSeconds(refreshRate);
            }
        }
        private void EndLoop()
        {
        }
    }
}