using TMPro;
using UnityEngine;

public class SensitivityController : MonoBehaviour
{
    public int minSensitivity = 1;
    public int maxSensitivity = 500;
    public int defaultSensitivity = 200;
    public TMP_InputField inputField;
    public GameObject camHolder;
    Looker looker;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (inputField == null){
            Debug.LogError("Input field is not assigned in the inspector.");
        }
        if (camHolder == null){
            Debug.LogError("Camera holder is not assigned in the inspector.");
        }
        looker = camHolder.GetComponent<Looker>();
        if (looker == null){
            Debug.LogError("Camera holder doesn't contain Looker component.");
        }
    }

    public void updateSensitivity(string input){
        int sensitivityValue;
        
        //check if input is valid integer
        if (!int.TryParse(input, out sensitivityValue))
        {
            Debug.LogWarning("Invalid input. Setting to default value.");
            sensitivityValue = defaultSensitivity;
        }

         int clampedValue = Mathf.Clamp(sensitivityValue, minSensitivity, maxSensitivity);
         inputField.text = clampedValue.ToString(); //sets the input field to the clamped value
         looker.mouseSensitivity = clampedValue;
        Debug.Log("Sensitivity set to: " + clampedValue);
    }
}
