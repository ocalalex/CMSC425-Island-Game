using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Controls;

public class Mapper : MonoBehaviour
{

    public Inventory inventory;
    public GameObject map;
    public GameObject userMarker;
    public GameObject keyMarkerText;
    public GameObject keyMarkerDot;

    public Key mapKeyNum = Key.M;
    public bool inMapView = false;

    public Camera mainCamera;
    public Camera mapCamera;

    KeyControl mapKey;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        keyMarkerText.GetComponent<Renderer>().enabled = false;
        keyMarkerDot.GetComponent<Renderer>().enabled = false;
        userMarker.GetComponent<Renderer>().enabled = false;
        mapKey = Keyboard.current[mapKeyNum];
        
    }

    // Update is called once per frame
    void Update()
    {
        if (mapKey.wasReleasedThisFrame)
        {
            if (inventory.CheckItem(map)) 
            {
                if (inMapView)
                {
                    inMapView = false;
                    keyMarkerText.GetComponent<Renderer>().enabled = false;
                    keyMarkerDot.GetComponent<Renderer>().enabled = false;
                    userMarker.GetComponent<Renderer>().enabled = false;
                    mainCamera.enabled = true;
                    mapCamera.enabled = false;
                } else {
                    inMapView = true;
                    mainCamera.enabled = false;
                    keyMarkerText.GetComponent<Renderer>().enabled = true;
                    keyMarkerDot.GetComponent<Renderer>().enabled = true;
                    userMarker.GetComponent<Renderer>().enabled = true;
                    mapCamera.enabled = true;
                }
            }
        }
        
    }
}
