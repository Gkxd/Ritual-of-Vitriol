using UnityEngine;
using System.Collections;
using UsefulThings;

public class RestartGame : MonoBehaviour {

    public GameObject titleScreen;
    public GameObject clickSfx;
    public GameObject gameScoreText;

    private TimeKeeper tk;

    void Start() {
        tk = GetComponent<TimeKeeper>();
    }

    void Update() {
        if (tk.lifeTime > 1) {
            if (Input.GetMouseButtonDown(1)) {
                titleScreen.SetActive(true);
                AudioManager.PlaySoundEffect(clickSfx);
                GameHP.RestartGame();
                GameState.ResetGame();
                gameScoreText.SetActive(false);
                gameObject.SetActive(false);
            }
        }
    }
}
