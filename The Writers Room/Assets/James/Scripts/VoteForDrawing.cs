using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.IO;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using System;

public class VoteForDrawing : MonoBehaviour
{
    [SerializeField] TMP_Text promptText;
    [SerializeField] EventSystem eventSystem;

    [SerializeField] Image[] images;

    GameManager gameManager;

    //string[] tempFiles = new string[4];

    // Start is called before the first frame update
    void Start()
    {
        // Use these to test the drawing importing without having to play the whole game
        //tempFiles[0] = "C:/Users/james/AppData/LocalLow/DefaultCompany/The Writers Room/Screenshots/Screenshot_05-05-2022-19-39-51.png";
        //tempFiles[1] = "C:/Users/james/AppData/LocalLow/DefaultCompany/The Writers Room/Screenshots/Screenshot_05-05-2022-19-40-00.png";
        //tempFiles[2] = "C:/Users/james/AppData/LocalLow/DefaultCompany/The Writers Room/Screenshots/Screenshot_05-05-2022-19-40-10.png";
        //tempFiles[3] = "C:/Users/james/AppData/LocalLow/DefaultCompany/The Writers Room/Screenshots/Screenshot_05-05-2022-19-40-19.png";

        gameManager = GameManager.gameManager.GetComponent<GameManager>();

        if (gameManager.votes == null)
            gameManager.votes = new int[GameManager.Players.Count];

        string holder = gameManager.finalResponses[gameManager.responseIndex];

        for (int i = 1; i < GameManager.Players.Count + 1; i++)
        {
            string helper = "PLAYER " + i;

            holder = holder.Replace(helper, GameManager.Players[i-1].name);
        }

        promptText.text = holder;

        for (int i = 0; i < images.Length; i++)
        {
            if (i == 3 && GameManager.Players.Count < 4) 
            {
                images[i].transform.gameObject.SetActive(false);
            }
            else 
            {
                // Set the sprite
                images[i].sprite = CreateSprite(GameManager.Players[i].drawings[gameManager.responseIndex]);
                //images[i].sprite = CreateSprite(tempFiles[i]);
            }

        }
    }

    Sprite CreateSprite(string filePath)
    {
        Sprite NewSprite;
        Texture2D SpriteTexture = LoadTexture(filePath);
        NewSprite = Sprite.Create(SpriteTexture, new Rect(0, 0, SpriteTexture.width, SpriteTexture.height), new Vector2(0, 0), 100.0f);

        return NewSprite;
    }
    Texture2D LoadTexture(string FilePath)
    {

        // Load a PNG or JPG file from disk to a Texture2D
        // Returns null if load fails

        Texture2D Tex2D;
        byte[] FileData;

        if (File.Exists(FilePath))
        {
            FileData = File.ReadAllBytes(FilePath);
            Tex2D = new Texture2D(2, 2);           // Create new "empty" texture
            if (Tex2D.LoadImage(FileData))           // Load the imagedata into the texture (size is set automatically)
                return Tex2D;                 // If data = readable -> return texture
        }
        return null;                     // Return null if load failed
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
                if (result.gameObject.name.Contains("Image"))
                {
                    // Get the index of the response that the player selected
                    int selectedResponse = Convert.ToInt32(result.gameObject.name.Substring(result.gameObject.name.Length - 1)) - 1;

                    // Increment the amount of votes that option received
                    gameManager.votes[selectedResponse]++;

                    // Move the game manager to the next player
                    gameManager.IteratePlayer();

                    // If the responder is not the first player, go to the next player
                    if (gameManager.responderIndex != 0)
                    {
                        SceneManager.LoadScene("NewPlayer");
                    }
                    else // If we have cycled through the players, record the vote
                    {
                        // Record the vote
                        //gameManager.finalDrawings[gameManager.responseIndex] = GameManager.Players[selectedResponse].drawings[gameManager.responseIndex];

                        // Save the final vote to the finalResponses object
                        int finalIndex = 0;
                        for (int i = 1; i < gameManager.votes.Length; i++)
                            if (gameManager.votes[i] > finalIndex)
                                finalIndex = i;

                        gameManager.finalDrawings[gameManager.responseIndex] = GameManager.Players[finalIndex].drawings[gameManager.responseIndex];

                        gameManager.votes = null;

                        if (gameManager.responseIndex == gameManager.sceneObject.Chunks.Length - 1)
                        {
                            // END THE VOTING SECTION
                            gameManager.currentState = GameManager.GameState.Animatic;

                            SceneManager.LoadScene("Animatic");
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
