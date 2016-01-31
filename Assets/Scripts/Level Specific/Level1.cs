using UnityEngine;
using System.Collections;

public class Level1 : MonoBehaviour {

    public GameObject nextLevel;

    private bool nextLevelActivated;

    void OnEnable() {
        nextLevelActivated = false;
    }

    void Update() {
        if (GameState.Downvotes > 10) {
            transform.Translate(0, -2 * Time.deltaTime, 0);
            if (transform.position.y < -10) {
                gameObject.SetActive(false);
            }

            if (!nextLevelActivated) {
                AudioManager.PlayYay();
                nextLevel.SetActive(true);
                nextLevelActivated = true;
            }
        }
    }
}
