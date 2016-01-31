using UnityEngine;
using System.Collections;

public class GameState : MonoBehaviour {

    private static GameState s_instance;
    public static GameState Instance { get { return s_instance ?? (s_instance = createInstance()); } }
    public static int Downvotes { get { return s_instance ? s_instance.downvotesClicked : 0; } }
    public static int Upvotes { get { return s_instance ? s_instance.upvotesClicked : 0; } }
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
        if (GameHP.instance.gameActive) {
            Instance.downvotesClicked++;
            GameHP.AddHp();
            GameScoreUI.SetDownvotes(Instance.downvotesClicked);
        }
    }

    public static void ClickUpvote() {
        if (GameHP.instance.gameActive) {
            Instance.upvotesClicked++;
            GameHP.RemoveHp();
        }
    }

    private void resetGame() {
        downvotesClicked = upvotesClicked = totalDownvotes = totalUpvotes = 0;
    }

    public static void ResetGame() {
        Instance.resetGame();
    }
}
