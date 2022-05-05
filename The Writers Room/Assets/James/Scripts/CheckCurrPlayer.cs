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
        if (gameManager.currentState == GameManager.GameState.Response)
            UnityEngine.SceneManagement.SceneManager.LoadScene("EnterResponse");
        else if (gameManager.currentState == GameManager.GameState.ResponseVote)
            UnityEngine.SceneManagement.SceneManager.LoadScene("VoteForResponse");
        else if (gameManager.currentState == GameManager.GameState.Draw)
            UnityEngine.SceneManagement.SceneManager.LoadScene("DrawBoard");
    }

}
