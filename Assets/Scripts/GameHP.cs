using UnityEngine;
using System.Collections;

public class GameHP : MonoBehaviour {

    public static GameHP instance;
    public static float PercentHP { get { return instance ? instance.hp / instance.maxHp : 1; } }

    private float hp;
    public float maxHp = 100;
    public float hpDrain = 10;
    public float hpGain = 10;
    public float hpPenalty = 10;

    public GameObject levels;
    public GameObject gameOverScreen;

    private bool gameActive;

    void Start() {
        instance = this;
        hp = maxHp;
    }

    void Update() {
        if (gameActive) {
            hp = Mathf.Max(hp - hpDrain * Time.deltaTime, 0);
            if (hp == 0) {
                foreach (Transform t in levels.transform) {
                    t.gameObject.SetActive(false);
                    gameOverScreen.SetActive(true);
                }
            }
        }

        float scale = transform.localScale.x;
        float targetScale = 1.03f * hp / maxHp;

        transform.localScale = new Vector3(Mathf.Lerp(scale, targetScale, 10 * Time.deltaTime), 1, 1);
    }

    private void addHp() {
        if (gameActive) hp = Mathf.Min(hp + hpGain, maxHp);
    }

    private void removeHp() {
        if (gameActive) hp = Mathf.Max(hp - hpGain, 0);
    }

    public static void AddHp() {
        instance.addHp();
    }

    public static void RemoveHp() {
        instance.removeHp();
    }

    public static void RestartGame() {
        instance.hp = instance.maxHp;
        instance.gameActive = false;
    }

    public static void StartGame() {
        instance.gameActive = true;
    }
}
