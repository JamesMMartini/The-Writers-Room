using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.IO;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using System;

public class ProgressAnimatic : MonoBehaviour
{

    static private int currScene;

    [SerializeField] Image image;
    [SerializeField] TMP_Text caption;
    [SerializeField] TMP_Text sceneNumber;
    [SerializeField] GameObject nextScene;
    [SerializeField] GameObject lastScene;

    GameManager gameManager;

    void Start()
    {
        currScene = 0;

        lastScene.SetActive(false);

        sceneNumber.text = "Scene: 1";

        gameManager = GameManager.gameManager.GetComponent<GameManager>();

        setImageCaption();

        Debug.Log(gameManager.finalResponses[currScene]);
    }

    public void nextSection() 
    {
        Debug.Log(currScene);
        currScene = currScene + 1;
        int sceneNum = currScene + 1;

        sceneNumber.text = "Scene: " + sceneNum;

        checkHigh();
    }

    public void lastSection() 
    {
        Debug.Log(currScene);
        currScene = currScene - 1;
        int sceneNum = currScene + 1;

        sceneNumber.text = "Scene: " + sceneNum;

        checkLow();
    }

    void setImageCaption() 
    {

        Debug.Log(gameManager.responseIndex);

        image.sprite = CreateSprite(gameManager.finalDrawings[currScene]);

        caption.text = gameManager.currentMadLib.chunks[currScene];
        caption.text += "/n";
        caption.text += gameManager.finalResponses[currScene];
    }

    void checkHigh() 
    {

        setImageCaption();

        if (currScene > 2)
        {
            nextScene.SetActive(false); 
        }
        else 
        {
            if (lastScene.activeSelf == false) { lastScene.SetActive(true); }
        }
    }

    void checkLow() 
    {

        setImageCaption();

        if (currScene < 1)
        {
            lastScene.SetActive(false);
        }
        else 
        {
            if (nextScene.activeSelf == false) { nextScene.SetActive(true); }
        }
    }

    // function that iterates curr scene + 1, then calls the function that sets the corresponding drawing and caption, and the function that checks the number of the scene
    // function that iterates curr scnee - 1, then calls the function that sets the corresponding drawing and caption, and the function that checks the number of the scene

    // function that handles setting the new image, the new drawing, and the new scene number on scene load or on button press

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

}
