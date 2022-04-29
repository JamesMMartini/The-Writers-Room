using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerKeeper : MonoBehaviour
{
    // This will call the GameManager functions that set player names and details.

    public GameObject gameManager;
    private GameObject[] nameFields;

    private int currPlayer;

    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameObject.Find("GameManager");

        nameFields = GameObject.FindGameObjectsWithTag("Name Field");

        currPlayer = 0;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public static void nextName() 
    {
        // this is where we check which name we're on and add accordingly
        // this should also set the "start game" button to active when enough names have been entered (at least 3 I'd imagine)
        if (currPlayer == 2)
        {
            Debug.Log("Add in a start game button, and add this player");
        }
        else if (currPlayer == 3)
        {
            Debug.Log("Turn off this button, you're at max players. but also add this player");
        }
        else 
        {
            Debug.Log("Do the normal thing of adding player");
        }
    }
    
}
