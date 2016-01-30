﻿using UnityEngine;
using System.Collections;

public class GameState : MonoBehaviour {

    private static GameState s_instance;
    public static GameState Instance { get { return s_instance ?? (s_instance = createInstance()); } }
    public static int Downvotes { get { return s_instance ? s_instance.downvotesClicked : 0; } }
    private static GameState createInstance() {
        GameObject stateObject = new GameObject("Game State");
        DontDestroyOnLoad(stateObject);
        GameState gameState = stateObject.AddComponent<GameState>();
        return gameState;
    }

    private int downvotesClicked;
    private int upvotesClicked;
    private int totalDownvotes;
    private int totalUpvotes;

    public static void AddDownvote() {
        Instance.totalDownvotes++;
    }

    public static void AddUpvote() {
        Instance.totalUpvotes++;
    }

    public static void ClickDownvote() {
        Instance.downvotesClicked++;
        GameHP.AddHp();
        GameScoreUI.SetDownvotes(Instance.downvotesClicked);
    }

    public static void ClickUpvote() {
        Instance.upvotesClicked++;
        GameHP.RemoveHp();
    }
}
