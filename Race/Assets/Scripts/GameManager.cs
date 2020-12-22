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
        [SerializeField] private RacetrackBehaviour racetrackBehaviour;
        private List<VehicleBehaviour> vehicleBehaviours;

        private void Awake()
        {
            vehicleBehaviours = new List<VehicleBehaviour>();
        }
        public void AddVehicleBehaviour(VehicleBehaviour behaviour)
        {
            vehicleBehaviours.Add(behaviour);
            for (int i = 0; i < vehicleBehaviours.Count; i++)
            {
                var offset = -racetrackBehaviour.Racetrack.RoadWidth / 2 + (i + 1) * racetrackBehaviour.Racetrack.RoadWidth / (vehicleBehaviours.Count);
                vehicleBehaviours[i].Offset(offset);
                vehicleBehaviours[i].transform.position = racetrackBehaviour.StartPoint.position;
            }
        }
        public void StartLoop()
        {
            vehicleBehaviours.ForEach(x => x.DistanceFromStart = 0f);
            StartCoroutine(LoopCoroutine());
        }
        private IEnumerator LoopCoroutine()
        {
            while (true)
            {
                if (vehicleBehaviours.Any(x => x.DistanceFromStart < racetrackBehaviour.Racetrack.Length))
                {
                    foreach (var behaviour in vehicleBehaviours.Where(x => x.DistanceFromStart < racetrackBehaviour.Racetrack.Length))
                    {
                        if (behaviour.BlowoutTimer > 0)
                        {
                            behaviour.BlowoutTimer -= refreshRate;
                        }
                        else
                        {
                            if (Random.Range(0, 100) <= behaviour.Vehicle.TireBlowoutChance)
                            {
                                behaviour.BlowoutTimer += blowoutTimeout;
                            }
                            else
                            {
                                behaviour.DistanceFromStart += behaviour.Vehicle.GetSpeed();
                                behaviour.transform.position = racetrackBehaviour.TrackToWorldPosition(behaviour.DistanceFromStart);
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
        private void EndLoop() { }
    }
}