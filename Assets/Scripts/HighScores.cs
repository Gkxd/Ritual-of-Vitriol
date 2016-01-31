using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class HighScores : MonoBehaviour {
    private struct HighScorePair {
        public int score;
        public string name;

        public HighScorePair(int score, string name) {
            this.score = score;
            this.name = name;
        }
    }

    private List<HighScorePair> highScoreList;

    public GameObject highScoreInput;

    void Start() {
        readFromString();
    }

    void Update() {
        if (Input.GetKeyDown(KeyCode.Q)) {
            highScoreList.Clear();
        }
    }

    void OnApplicationQuit() {
        saveToString();
    }

    public bool isHighScore() {
        if (highScoreList == null) {
            readFromString();
        }

        if (highScoreList.Count < 5) {
            return GameState.Downvotes > 0;
        }

        int minScore = highScoreList.Aggregate((x, y) => Mathf.Min(x.score, y.score) == x.score ? x : y).score;
        return GameState.Downvotes > minScore;
    }

    public void addHighScore(InputField nameField) {
        highScoreList.Add(new HighScorePair(GameState.Downvotes, nameField.text));
        highScoreList.Sort((x, y) => y.score - x.score);

        if (highScoreList.Count > 5) {
            highScoreList.RemoveAt(5);
        }

        nameField.text = "";

        refreshDisplay();
        highScoreInput.SetActive(false);
    }

    public void saveToString() {
        string highScores = "";
        for (int i = 0; i < highScoreList.Count; i++) {
            HighScorePair h = highScoreList[i];
            highScores += h.name + "\t" + h.score + (i == highScoreList.Count - 1 ? "" : "\n");
        }
        PlayerPrefs.SetString("HighScores", highScores);
    }

    public void readFromString() {
        highScoreList = highScoreList ?? new List<HighScorePair>();
        highScoreList.Clear();

        if (!PlayerPrefs.HasKey("HighScores")) {
            return;
        }
        string highScores = PlayerPrefs.GetString("HighScores");
        string[] entries = highScores.Split('\n');

        foreach (string s in entries) {
            if (s == "") continue;
            string[] tokens = s.Split('\t');
            highScoreList.Add(new HighScorePair(int.Parse(tokens[1]), tokens[0]));
        }

        highScoreList.Sort((x, y) => y.score - x.score);
        refreshDisplay();
    }

    private void refreshDisplay() {
        int i = 0;
        foreach (Transform t in transform) {
            if (i < highScoreList.Count) {
                t.FindChild("PlayerName").GetComponent<Text>().text = highScoreList[i].name;
                t.FindChild("Score").GetComponent<Text>().text = "" + highScoreList[i].score;
            }
            else {
                t.FindChild("PlayerName").GetComponent<Text>().text = "";
                t.FindChild("Score").GetComponent<Text>().text = "";
            }
            i++;
        }
    }
}
