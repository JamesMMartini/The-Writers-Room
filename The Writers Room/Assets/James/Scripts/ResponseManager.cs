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

    GameManager gameManager;

    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameManager.gameManager.GetComponent<GameManager>();

        prompt.text = gameManager.currentMadLib.prompts[gameManager.responseIndex];
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

    void SubmitResponse()
    {
        if (gameManager.responseIndex < gameManager.currentMadLib.prompts.Length - 1) // Go to the next response
        {
            gameManager.currentMadLib.responses[gameManager.responseIndex] = response.text;
            gameManager.responseIndex++;

            SceneManager.LoadScene("EnterResponse");
        }
        else // End the input section
        {
            gameManager.currentMadLib.responses[gameManager.responseIndex] = response.text;
            gameManager.responseIndex++;

            SceneManager.LoadScene("ShowResults");
        }
    }
}