using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SetMadlib : MonoBehaviour
{
    public SceneObject thisSceneObject;
    public MadLib thisMadLib;

    GameManager gameManager;

    void Start() 
    {
        gameManager = GameManager.gameManager.GetComponent<GameManager>();

        thisMadLib = new MadLib(thisSceneObject);
    }

    public void SetThisLib()
    {
        gameManager.currentMadLib = thisMadLib;

        UnityEngine.SceneManagement.SceneManager.LoadScene("NewPlayer");
    }
}
