using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using System;
using UnityEngine.SceneManagement;

public class VoteForResponse : MonoBehaviour
{
    [SerializeField] EventSystem eventSystem;
    [SerializeField] TMP_Text prompt;
    [SerializeField] TMP_Text[] responses;

    GameManager gameManager;

    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameManager.gameManager.GetComponent<GameManager>();

        gameManager.votes = new int[GameManager.Players.Count];

        prompt.text = gameManager.sceneObject.Prompts[gameManager.responseIndex];

        for (int i = 0; i < GameManager.Players.Count; i++)
        {
            responses[i].gameObject.SetActive(true);
            responses[i].text = GameManager.Players[i].answers[gameManager.responseIndex];
        }
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
                if (result.gameObject.name.Contains("Response"))
                {
                    int selectedResponse = Convert.ToInt32(result.gameObject.name.Substring(result.gameObject.name.Length - 1)) - 1;

                    gameManager.votes[selectedResponse]++;

                    gameManager.IteratePlayer();

                    if (gameManager.responderIndex != 0)
                    {
                        SceneManager.LoadScene("NewPlayer");
                    }
                    else
                    {
                        // Record the vote
                        gameManager.finalResponses[gameManager.responseIndex] = result.gameObject.GetComponent<TMP_Text>().text;

                        if (gameManager.responseIndex == gameManager.sceneObject.Chunks.Length - 1)
                        {
                            // END THE VOTING SECTION
                        }
                        else
                        {
                            gameManager.responseIndex++;

                            SceneManager.LoadScene("NewPlayer");
                        }
                    }
                }
            }
        }
    }
}