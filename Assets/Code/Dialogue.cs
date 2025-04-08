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

    public GameObject player; //reference to the player
    public GameObject playerCamera; //reference to the camera of the player 

    public Collider buildingTrigger; //reference to the building 
    private bool hasExitedBuilding = false; //see if user has exited building
   
   
    void Start()
    {
        textComponent.text = string.Empty;
        player.GetComponent<Mover>().enabled = false; //disable movement of the player 
        playerCamera.GetComponent<Looker>().enabled = false;
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
            player.GetComponent<Mover>().enabled = true; //allows player to move again
            playerCamera.GetComponent<Looker>().enabled = true;
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

void InsertExitLine()
{
    // Replace lines entirely with only the new one
    lines = new string[] { "You step out of the building...Now find the boat and avoid being caught by the spotlight. Good luck!" };
    index = 0;
    textComponent.text = string.Empty;

    player.GetComponent<Mover>().enabled = false;
    playerCamera.GetComponent<Looker>().enabled = false;

    gameObject.SetActive(true); // Reactivate the dialogue box
    StartCoroutine(TypeLine());
}
}
