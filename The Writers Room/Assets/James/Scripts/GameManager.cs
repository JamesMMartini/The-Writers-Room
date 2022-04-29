using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public MadLib currentMadLib;

    public SceneObject sceneObject;

    public int responderIndex;

    public int responseIndex;

    public static GameObject gameManager;

    public string[] finalResponses;

    public int[] votes;

    public enum GameState { Response, ResponseVote, Draw, DrawVote}

    public GameState currentState;

    public class PlayerInfo
    {
        public string name;

        public int score;

        public List<string> answers;
    }

    public static Dictionary<int, PlayerInfo> Players = new Dictionary<int, PlayerInfo>();

    private static int numPlayers;

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

        currentState = GameState.Response;

        finalResponses = new string[sceneObject.Prompts.Length];

        numPlayers = 0;
    }

    public void IteratePlayer() 
    {
        responderIndex++;

        if (responderIndex == Players.Count)
            responderIndex = 0;

        //if (responderIndex == Players.Count)
        //{
        //    responderIndex = 0;
        //}
        //else 
        //{
        //    responderIndex += 1;
        //}
        
    }

    public void AddPlayer(string newName)
    {
        // Add's a player's name and sets their other attributes to defaults

        PlayerInfo newPlayer = new PlayerInfo();

        newPlayer.name = newName;
        newPlayer.score = 0;
        newPlayer.answers = new List<string>();

        Players.Add(numPlayers, newPlayer);
        numPlayers += 1;
    }

    public void AddAnswer(int name, string newAnswer) 
    {
        // Adds the corresponding player's answer to their data structure

        Players[name].answers.Add(newAnswer);
    }
}
