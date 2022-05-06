using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class DrawManager : MonoBehaviour
{
    [SerializeField] EventSystem eventSystem;
    [SerializeField] GameObject drawer;
    [SerializeField] TMP_Text readyPrompt;
    [SerializeField] TMP_Text timer;
    [SerializeField] TMP_Text showPromptText;
    [SerializeField] GameObject nextButton;

    GameManager gameManager;

    bool isDrawing = false;

    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameManager.gameManager.GetComponent<GameManager>();

        drawer.SetActive(false);

        StartCoroutine(StartDrawing());
    }

    IEnumerator StartDrawing()
    {
        readyPrompt.text = gameManager.currentMadLib.chunks[gameManager.responseIndex];
        readyPrompt.text += "\n";
        readyPrompt.text += gameManager.finalResponses[gameManager.responseIndex];

        yield return new WaitForSeconds(5f);

        readyPrompt.text = "Ready?";

        yield return new WaitForSeconds(1f);

        readyPrompt.text = "DRAW!";

        yield return new WaitForSeconds(1f);

        readyPrompt.gameObject.SetActive(false);
        drawer.SetActive(true);
        isDrawing = true;
        StartCoroutine(StartTimer());
    }

    IEnumerator StartTimer()
    {
        for (int i = 29; i >= 0; i--)
        {
            yield return new WaitForSeconds(1f);

            timer.text = i.ToString();
        }

        StartCoroutine(SubmitResponse());
    }

    // Update is called once per frame
    void Update()
    {
        if (isDrawing)
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
                        StartCoroutine(SubmitResponse());
                    }
                    else if (result.gameObject.name == "Show Prompt")
                    {
                        StartCoroutine(ShowPrompt());
                    }
                }
            }
        }
    }

    IEnumerator ShowPrompt()
    {
        readyPrompt.text = gameManager.currentMadLib.chunks[gameManager.responseIndex];
        readyPrompt.text += "/n";
        readyPrompt.text += gameManager.finalResponses[gameManager.responseIndex];

        readyPrompt.gameObject.SetActive(true);

        yield return new WaitForSeconds(3f);

        readyPrompt.gameObject.SetActive(false);
    }

    IEnumerator SubmitResponse()
    {
        showPromptText.gameObject.SetActive(false);
        timer.gameObject.SetActive(false);
        readyPrompt.gameObject.SetActive(false);
        nextButton.SetActive(false);

        yield return new WaitForSeconds(0.1f);

        string filePath = TakeScreenshot();

        Debug.Log(gameManager.responseIndex + " - " + gameManager.finalResponses.Length);

        if (gameManager.responseIndex < gameManager.finalResponses.Length - 1) // Go to the next response
        {
            gameManager.AddDrawing(gameManager.responderIndex, filePath);
            gameManager.responseIndex++;

            SceneManager.LoadScene("DrawBoard");
        }
        else // End the input section
        {
            gameManager.AddDrawing(gameManager.responderIndex, filePath);
            gameManager.responseIndex = 0;
            gameManager.IteratePlayer();

            if (gameManager.responderIndex != 0)
            {
                SceneManager.LoadScene("NewPlayer");
            }
            else
            {
                gameManager.currentState = GameManager.GameState.DrawVote;
                gameManager.responseIndex = 0;

                SceneManager.LoadScene("NewPlayer");
            }
        }
    }

    string TakeScreenshot()
    {
        string folderPath = Application.persistentDataPath + "/" + "Screenshots/";

        if (!System.IO.Directory.Exists(folderPath))
            System.IO.Directory.CreateDirectory(folderPath);

        var screenshotName =
                                "Screenshot_" +
                                System.DateTime.Now.ToString("dd-MM-yyyy-HH-mm-ss") +
                                ".png";
        
        ScreenCapture.CaptureScreenshot(System.IO.Path.Combine(folderPath, screenshotName));

        string finalPath = folderPath + screenshotName;
        Debug.Log(finalPath);
        return finalPath;
    }
}
