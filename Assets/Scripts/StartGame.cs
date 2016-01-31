using UnityEngine;
using System.Collections;

public class StartGame : MonoBehaviour {

    public GameObject gameScoreText;
    public GameObject level1;
    public GameObject clickSfx;

    void Update() {
        if (Input.GetMouseButtonDown(0)) {
            GameHP.StartGame();
            AudioManager.PlaySoundEffect(clickSfx);
            gameScoreText.SetActive(true);
            level1.transform.localPosition = Vector3.zero;
            level1.SetActive(true);
            gameObject.SetActive(false);
        }
    }
}
