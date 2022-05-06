using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.IO;

public class VoteForDrawing : MonoBehaviour
{
    [SerializeField] TMP_Text promptText;

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

        promptText.text = gameManager.finalResponses[gameManager.responseIndex];

        for (int i = 0; i < images.Length; i++)
        {
            // Set the sprite
            images[i].sprite = CreateSprite(GameManager.Players[i].drawings[gameManager.responseIndex]);
            //images[i].sprite = CreateSprite(tempFiles[i]);
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
        
    }
}
