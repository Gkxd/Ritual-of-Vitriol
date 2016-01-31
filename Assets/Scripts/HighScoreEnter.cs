using UnityEngine;
using System.Collections;

public class HighScoreEnter : MonoBehaviour {

    public GameObject inputField;
    public HighScores highScores;

    void OnEnable() {
        if (highScores.isHighScore()) {
            inputField.SetActive(true);
        }
        else {
            inputField.SetActive(false);
        }
    }
}
