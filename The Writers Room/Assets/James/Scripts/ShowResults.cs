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
        for (int i = 0; i < gameManager.currentMadLib.chunks.Length; i++)
        {
            results.text += gameManager.currentMadLib.chunks[i];
            results.text += " ";
            results.text += gameManager.currentMadLib.responses[i];
            results.text += "\n";
        }
    }
}
