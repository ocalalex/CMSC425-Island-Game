using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using TMPro;

public class Dialogue : MonoBehaviour
{
    public TextMeshProUGUI textComponent; //reference to the TextMeshPro object
    public string[] lines; //stores the lines 
    public float textSpeed; //speed that the lines will be displayed
    private int index; //curr index that we are at in lines
    public Collider buildingTrigger; //reference to the building 
    public List<Behaviour> componentsToDisable = new List<Behaviour>(); //list of components to disable when the dialogue starts
    private bool hasExitedBuilding = false; //see if user has exited building
    public bool isSpotted = false;


    void Start()
    {
        textComponent.text = string.Empty;
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (textComponent.text == lines[index])
            {
                NextLine();
            }
            else
            {
                StopAllCoroutines();
                textComponent.text = lines[index];
            }
        }
    }

    public void StartDialogue()
    {
        disableComponents();
        index = 0;
        textComponent.text = string.Empty;
        StartCoroutine(TypeLine());
    }

    IEnumerator TypeLine()
    {
        yield return null;
        foreach (char c in lines[index].ToCharArray())
        {
            textComponent.text += c;
            yield return new WaitForSeconds(textSpeed);
        }
    }

    void NextLine()
    {
        if (index < lines.Length - 1)
        {
            index++;
            textComponent.text = string.Empty;
            StartCoroutine(TypeLine());
        }
        else
        {
            enableComponents();
            gameObject.SetActive(false);
        }
    }

    public void TriggerExitLine()
    {
        if (!hasExitedBuilding)
        {
            hasExitedBuilding = true;
            InsertExitLine();
        }
    }

    //called when player is first spotted by the spotlight
    public void TriggerSpottedLine()
    {
        if (!isSpotted)
        {
            hasExitedBuilding = true;
            InsertSpottedLine();
        }
    }

    public void TriggerBoatLine() 
    {
        InsertBoatLine();
    }

    void InsertExitLine()
    {
        // Replace lines entirely with only the new one
        lines = new string[] { "You step out of the building...Now find the boat and avoid being caught by the spotlight. Good luck!" };
        index = 0;
        textComponent.text = string.Empty;

        disableComponents();

        gameObject.SetActive(true); // Reactivate the dialogue box
        StartCoroutine(TypeLine());
    }

    void InsertSpottedLine()
    {
        lines = new string[] { "You have been caught by the spotlight!", "Try hiding under the trees next time." };
        index = 0;
        textComponent.text = string.Empty;

        disableComponents();
        gameObject.SetActive(true); // Reactivate the dialogue box
        StartCoroutine(TypeLine());
    }

    void InsertBoatLine()
    {
        lines = new string[] { "It looks like this boat is the only way off the island.", "It's missing a few parts though..." };
        index = 0;
        textComponent.text = string.Empty;

        disableComponents();
        gameObject.SetActive(true); // Reactivate the dialogue box
        StartCoroutine(TypeLine());
    }

    // called when player intially picks up a gun
    public void InsertPickupGunLine()
    {
        lines = new string[] {"You picked up a gun!", "Press 'E' to equip it.", "It may come in handy later."};
        index = 0;
        textComponent.text = string.Empty;

        disableComponents();
        gameObject.SetActive(true);
        StartCoroutine(TypeLine());
    }

    // called when player intiially picks up a map
    public void InsertPickupMapLine()
    {
        lines = new string[] {"You found a map!", "Press 'M' to see the entire island."};
        index = 0;
        textComponent.text = string.Empty;

        disableComponents();
        gameObject.SetActive(true);
        StartCoroutine(TypeLine());
    }

    //disables all the components in the list when the dialogue starts
    void disableComponents()
    {
        foreach (Behaviour component in componentsToDisable)
        {
            component.enabled = false; //disable all the components in the list
        }
    }

    //enables all the components in the list when the dialogue ends
    void enableComponents()
    {
        foreach (Behaviour component in componentsToDisable)
        {
            component.enabled = true; //enable all the components in the list
        }
    }

}


