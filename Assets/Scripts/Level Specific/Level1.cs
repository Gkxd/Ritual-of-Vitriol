using UnityEngine;
using System.Collections;

public class Level1 : MonoBehaviour {

    public GameObject nextLevel;

    private bool nextLevelActivated;

    void Update() {
        if (GameState.Downvotes > 10) {
            transform.Translate(0, -2 * Time.deltaTime, 0);
            if (transform.position.y < -10) {
                Destroy(gameObject);
            }

            if (!nextLevelActivated) {
                nextLevel.SetActive(true);
                nextLevelActivated = true;
            }
        }
    }
}
