using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ShowResults : MonoBehaviour
{
    [SerializeField] TMP_Text results;

    GameManager gameManager;

    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameManager.gameManager.GetComponent<GameManager>();

        DisplayResults();
    }

    void DisplayResults()
    {
        for (int i = 0; i < gameManager.sceneObject.Chunks.Length; i++)
        {
            results.text += gameManager.sceneObject.Chunks[i];
            results.text += " ";
            results.text += gameManager.finalResponses[i];
            results.text += "\r\n";
        }
    }
}
