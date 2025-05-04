using UnityEngine;

public class DifficultyChanger : MonoBehaviour
{
    public GameObject treeManager;
    public GameObject user;
    public GameObject spotlight;
    
    private Mover userMover;
    private TreeSpawner treeSpawner;
    private Rotator spotlightRotater;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (treeManager == null)
        {
            Debug.LogError("Tree Manager is not assigned in the inspector.");
        }
        if (user == null)
        {
            Debug.LogError("User is not assigned in the inspector.");
        }
        if (spotlight == null)
        {
            Debug.LogError("Spotlight is not assigned in the inspector.");
        }
        userMover = user.GetComponent<Mover>();
        if (userMover == null)
        {
            Debug.LogError("User doesn't container Looker component.");
        }
        treeSpawner = treeManager.GetComponent<TreeSpawner>();
        if (treeSpawner == null)
        {
            Debug.LogError("Tree Manager doesn't container TreeSpawner component.");
        }
        spotlightRotater = spotlight.GetComponent<Rotator>();
        if (spotlightRotater == null)
        {
            Debug.LogError("Spotlight doesn't container SpotlightRotater component.");
        }
    }

    // Adjusts the movement speed, tree count, and rotation speed of the spotlight based on the difficulty level
    public void changeDifficulty(int difficulty)
    {
        if (difficulty == 0)    //easy
        {
            userMover.speed = 18f;
            treeSpawner.treeCount = 30;
            spotlightRotater.ChangeRotationPeriod(12f);
        }
        else if (difficulty == 1)   //medium
        {
            userMover.speed = 15f;
            treeSpawner.treeCount = 20;
            spotlightRotater.ChangeRotationPeriod(10f);
        }
        else if (difficulty == 2)   //hard
        {
            userMover.speed = 12f;
            treeSpawner.treeCount = 15;
            spotlightRotater.ChangeRotationPeriod(8f);
        }
        
        //respawns trees to match the new difficulty level
        treeSpawner.respawnTrees();
    }
}
