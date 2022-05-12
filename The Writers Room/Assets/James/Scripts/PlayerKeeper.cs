using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class PlayerKeeper : MonoBehaviour
{
    // This will call the GameManager functions that set player names and details.

    GameManager gameManager;
    //public GameObject gameManager;
    private GameObject[] nameFields;

    private int currPlayer;
    private TMP_InputField inputField;

    private float newJoinX = -33.32f;

    public GameObject canv;

    [SerializeField] GameObject startButton;

    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameManager.gameManager.GetComponent<GameManager>();
        //gameManager = GameObject.Find("GameManager");

        nameFields = GameObject.FindGameObjectsWithTag("Name Field");

        currPlayer = 0;

        canv = GameObject.Find("Canvas");
    }

    public void checkReady() 
    {
        if (currPlayer < 4) 
        {
            if (currPlayer == 2)
            {
                addStart();
            }

            nextName();
        }
    }

    public void addStart() 
    {
        // add a start button and then move the join button

        this.transform.localPosition = new Vector3(newJoinX, transform.localPosition.y, transform.localPosition.z);

        Instantiate(startButton, canv.transform);
    }

    public void nextName() 
    {
        inputField = GameObject.Find("InputField (TMP)").GetComponent<TMP_InputField>();
        string name = inputField.text;

        // this is where we check which name we're on and add accordingly
        // this should also set the "start game" button to active when enough names have been entered (at least 3 I'd imagine)

        if (name != "") 
        {
            // add the player to the hierarchy
            gameManager.AddPlayer(name);

            // add the player's name to the correct nameField
            TextMeshProUGUI newText = nameFields[currPlayer].GetComponent<TextMeshProUGUI>();
            newText.text = name;

            currPlayer += 1;

            if (currPlayer == 4) 
            {
                // eventually add in a color change to the button
            }

            inputField.text = "";
        }
        
    }

    public void LetsStart() 
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("SelectMadLib");
    }
    
}
