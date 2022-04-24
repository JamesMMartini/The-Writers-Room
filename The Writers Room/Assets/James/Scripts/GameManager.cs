using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public MadLib currentMadLib;

    public SceneObject sceneObject;

    public int responseIndex;

    public static GameObject gameManager;

    private void Awake()
    {
        if (gameManager != null && gameManager != this)
        {
            Destroy(gameObject);
        }
        else
        {
            gameManager = gameObject;
        }

        DontDestroyOnLoad(gameManager);

        // Set the current MadLib
        currentMadLib = new MadLib(sceneObject);
    }
}
