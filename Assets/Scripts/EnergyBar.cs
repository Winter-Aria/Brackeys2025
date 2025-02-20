using UnityEngine;
using UnityEngine.UI;

public class EnergyBar : MonoBehaviour
{
    [SerializeField] private Slider energySlider; // Reference to UI Slider

    private void Start()
    {
        
    }

    private void Update()
    {


            float energyPercentage = PlayerController.instance.currentEnergy / PlayerController.instance.maxEnergy;
            energySlider.value = energyPercentage; // Update UI Slider
        
    }
}