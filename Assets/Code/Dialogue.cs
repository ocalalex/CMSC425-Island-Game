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
        disableComponents();
        StartDialogue();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0)) {
            if (textComponent.text == lines[index]) {
                NextLine();
            } else {
                StopAllCoroutines();
                textComponent.text = lines[index];
            }
        }
    }

    void StartDialogue() {
        index = 0;
        StartCoroutine(TypeLine());
    }

    IEnumerator TypeLine() {
        foreach (char c in lines[index].ToCharArray()) {
            textComponent.text += c;
            yield return new WaitForSeconds(textSpeed);
        }
    }

    void NextLine() {
        if (index < lines.Length - 1) {
            index++;
            textComponent.text = string.Empty;
            StartCoroutine(TypeLine());
        } else {
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

    public void TriggerSpottedLine()
{
    if (!isSpotted)
    {
        hasExitedBuilding = true;
        InsertSpottedLine();
    }
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
    lines = new string[] {"You have been caught by the spotlight!", "Try hiding under the trees next time."};
    index = 0;
    textComponent.text = string.Empty;

    disableComponents();
    gameObject.SetActive(true); // Reactivate the dialogue box
    StartCoroutine(TypeLine());
}
void disableComponents()
{
    foreach (Behaviour component in componentsToDisable) {
        component.enabled = false; //disable all the components in the list
    }
}
void enableComponents()
{
    foreach (Behaviour component in componentsToDisable) {
        component.enabled = true; //enable all the components in the list
    }
}

}


