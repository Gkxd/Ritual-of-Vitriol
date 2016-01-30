using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class GameScoreUI : MonoBehaviour {

    private static GameScoreUI instance;

    private Text scoreMessage;
    void Start() {
        instance = this;
        scoreMessage = GetComponent<Text>();
    }

    public static void SetDownvotes(int downvotes) {
        instance.scoreMessage.text = "Downvotes: " + downvotes;
    }
}
