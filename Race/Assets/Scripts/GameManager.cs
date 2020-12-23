using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

namespace Race
{
    public class GameManager : MonoBehaviour
    {
        [SerializeField] UnityEvent OnStarted;
        [SerializeField] UnityEvent OnFinished;
        [SerializeField] VehicleEvent onTireBlowout;
        [SerializeField] private float refreshRate = 0.1f;
        [SerializeField] private float blowoutTimeout = 1f;
        [SerializeField] private RacetrackBehaviour racetrackBehaviour;
        private List<VehicleBehaviour> vehicleBehaviours;
        private int lastPlace = 1;
        private void Awake()
        {
            vehicleBehaviours = new List<VehicleBehaviour>();
        }
        public void AddVehicleBehaviour(VehicleBehaviour behaviour)
        {
            vehicleBehaviours.Add(behaviour);
            behaviour.Place = vehicleBehaviours.Count;
            UpdateVehiclePositions();
        }

        public void DeleteVehicle(VehicleBehaviour behaviour)
        {
            vehicleBehaviours.Remove(behaviour);
            Destroy(behaviour.gameObject);
            UpdateVehiclePositions();
        }

        public void UpdateVehiclePositions()
        {
            for (int i = 0; i < vehicleBehaviours.Count; i++)
            {
                var offset = -racetrackBehaviour.Racetrack.RoadWidth / 2 + (i + 1) * racetrackBehaviour.Racetrack.RoadWidth / (vehicleBehaviours.Count);
                vehicleBehaviours[i].Offset(offset);
            }
            vehicleBehaviours.ForEach(x => x.transform.position = racetrackBehaviour.StartPoint.position);
        }
        public void StartLoop()
        {
            vehicleBehaviours.ForEach(x => { x.DistanceFromStart = 0f; x.Place = -1; });
            lastPlace = 1;
            if (OnStarted != null)
            {
                OnStarted.Invoke();
            }
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
                                if (onTireBlowout != null)
                                {
                                    onTireBlowout.Invoke(behaviour);
                                }
                            }
                            else
                            {
                                behaviour.DistanceFromStart += behaviour.Vehicle.GetSpeed();
                                racetrackBehaviour.SetToWorldPosition(behaviour.DistanceFromStart, behaviour.transform);
                                if (behaviour.DistanceFromStart >= racetrackBehaviour.Racetrack.Length)
                                {
                                    behaviour.Place = lastPlace;
                                    lastPlace++;
                                }
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
            if (OnFinished != null)
            {
                OnFinished.Invoke();
            }
        }
    }
}