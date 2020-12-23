using System.Collections.Generic;
using UnityEngine;

namespace Race
{
    public class UIManager : MonoBehaviour
    {
        [SerializeField] private GameObject vehicleListPanel;
        [SerializeField] private VehiclePanel vehiclePanelPrefab;
        [SerializeField] private GameManager GameManager;
        private List<VehiclePanel> vehiclePanels;

        private void Awake()
        {
            vehiclePanels = new List<VehiclePanel>();
        }
        public void AddVehiclePanel(VehicleBehaviour behaviour)
        {
            var panel = Instantiate(vehiclePanelPrefab, vehicleListPanel.transform);
            if (behaviour.Vehicle is Car)
            {
                panel.ShowCarUI();
            }
            else if (behaviour.Vehicle is Motorcycle)
            {
                panel.ShowMotoUI();
            }
            else if (behaviour.Vehicle is Truck)
            {
                panel.ShowTruckUI();
            }
            vehiclePanels.Add(panel);
            panel.VehicleBehaviour = behaviour;
            panel.OnDelete.AddListener(x => GameManager.DeleteVehicle(x));
        }

        public void EnablePanels()
        {
            vehiclePanels.ForEach(x => x.ToggleEdit(true));
        }

        public void DisablePanels()
        {
            vehiclePanels.ForEach(x => x.ToggleEdit(false));
        }
    }
}