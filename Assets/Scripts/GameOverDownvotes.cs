using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class GameOverDownvotes : MonoBehaviour {
    Text score;
    void OnEnable() {
        (score ?? (score = GetComponent<Text>())).text = "" + GameState.Downvotes;
    }
}
