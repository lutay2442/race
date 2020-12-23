using UnityEngine;
using UnityEngine.UI;

namespace Race
{
    public class VehiclePanel : MonoBehaviour
    {
        public VehicleEvent OnDelete;
        [SerializeField] private Text label;
        [SerializeField] private Toggle toggle;
        [SerializeField] private Slider weightSlider;
        [SerializeField] private Slider numberOfPeopleSlider;
        [SerializeField] private Button deleteButton;

        public VehicleBehaviour VehicleBehaviour;

        public void ToggleEdit(bool value)
        {
            toggle.enabled = value;
            weightSlider.enabled = value;
            numberOfPeopleSlider.enabled = value;
            deleteButton.gameObject.SetActive(value);
        }
        public void ToggleSidecar(bool value)
        {
            if (VehicleBehaviour != null && VehicleBehaviour.Vehicle is Motorcycle)
            {
                var motorcycle = (Motorcycle) VehicleBehaviour.Vehicle;
                motorcycle.HasSidecar = value;
            }
        }
        public void SetWeight(float value)
        {
            if (VehicleBehaviour != null && VehicleBehaviour.Vehicle is Truck)
            {
                var truck = (Truck) VehicleBehaviour.Vehicle;
                truck.CargoWeight = value;
            }
        }
        public void SetNumberOfPeople(float value)
        {
            if (VehicleBehaviour != null && VehicleBehaviour.Vehicle is Car)
            {
                var car = (Car) VehicleBehaviour.Vehicle;
                car.NumberOfPeople = (int) value;
            }
        }
        public void ShowMotoUI()
        {
            toggle.gameObject.SetActive(true);
            weightSlider.gameObject.SetActive(false);
            numberOfPeopleSlider.gameObject.SetActive(false);
        }
        public void ShowCarUI()
        {
            toggle.gameObject.SetActive(false);
            weightSlider.gameObject.SetActive(false);
            numberOfPeopleSlider.gameObject.SetActive(true);
        }
        public void ShowTruckUI()
        {
            toggle.gameObject.SetActive(false);
            weightSlider.gameObject.SetActive(true);
            numberOfPeopleSlider.gameObject.SetActive(false);
        }
        void Update()
        {
            if (VehicleBehaviour == null)
            {
                return;
            }
            label.text = VehicleBehaviour.GetRichtext();
        }
        public void Delete()
        {
            if (OnDelete != null)
            {
                OnDelete.Invoke(VehicleBehaviour);
            }
            Destroy(gameObject);
        }
    }
}