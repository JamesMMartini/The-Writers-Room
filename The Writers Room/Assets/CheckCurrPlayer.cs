using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CheckCurrPlayer : MonoBehaviour
{
    [SerializeField] TMP_Text playerName;
    GameManager gameManager;

    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameManager.gameManager.GetComponent<GameManager>();

        playerName.text = "Are you ready " + GameManager.Players[gameManager.responderIndex].name + "?";
    }

    public void LetsStart()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("EnterResponse");
    }

}
