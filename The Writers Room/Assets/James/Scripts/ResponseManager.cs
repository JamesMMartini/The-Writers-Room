using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class ResponseManager : MonoBehaviour
{
    [SerializeField] EventSystem eventSystem;
    [SerializeField] TMP_InputField response;
    [SerializeField] TMP_Text prompt;
    [SerializeField] TMP_Text timer;
    [SerializeField] int time;

    // time should be the amount of time you want minus 1

    GameManager gameManager;

    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameManager.gameManager.GetComponent<GameManager>();

        string holder = gameManager.currentMadLib.prompts[gameManager.responseIndex];

        for (int i = 1; i < GameManager.Players.Count + 1; i++) 
        {
            string helper = "PLAYER " + i;

            holder = holder.Replace(helper, GameManager.Players[i-1].name);
        }

        prompt.text = holder;

        StartCoroutine(Timer());
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyUp(KeyCode.Mouse0))
        {
            //Set up the new Pointer Event
            PointerEventData mouseEventData = new PointerEventData(eventSystem);
            //Set the Pointer Event Position to that of the mouse position
            mouseEventData.position = Input.mousePosition;

            //Create a list of Raycast Results
            List<RaycastResult> results = new List<RaycastResult>();

            //Raycast using the Graphics Raycaster and mouse click position
            GraphicRaycaster rayCaster = GetComponentInParent<GraphicRaycaster>();
            rayCaster.Raycast(mouseEventData, results);

            //For every result returned, output the name of the GameObject on the Canvas hit by the Ray
            foreach (RaycastResult result in results)
            {
                if (result.gameObject.name == "ButtonBackground")
                {
                    SubmitResponse();
                }
            }
        }
    }

    IEnumerator Timer()
    {
        for (int i = time; i >= 0; i--)
        {
            yield return new WaitForSeconds(1f);

            timer.text = i.ToString();
        }

        SubmitResponse();
    }

    void SubmitResponse()
    {
        if (gameManager.responseIndex < gameManager.currentMadLib.prompts.Length - 1) // Go to the next response
        {
            gameManager.AddAnswer(gameManager.responderIndex, response.text);
            gameManager.currentMadLib.responses[gameManager.responseIndex] = response.text;
            gameManager.responseIndex++;

            SceneManager.LoadScene("EnterResponse");
        }
        else // End the input section
        {
            gameManager.AddAnswer(gameManager.responderIndex, response.text);
            gameManager.currentMadLib.responses[gameManager.responseIndex] = response.text;
            gameManager.responseIndex = 0;
            gameManager.IteratePlayer();

            if (gameManager.responderIndex != 0)
            {
                SceneManager.LoadScene("NewPlayer");
            }
            else
            {
                gameManager.currentState = GameManager.GameState.ResponseVote;
                gameManager.responseIndex = 0;

                SceneManager.LoadScene("NewPlayer");
            }
        }
    }
}