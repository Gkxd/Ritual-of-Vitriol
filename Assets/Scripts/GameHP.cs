using UnityEngine;
using System.Collections;

public class GameHP : MonoBehaviour {

    public static GameHP instance;

    private float hp;
    public float maxHp = 100;
    public float hpDrain = 10;
    public float hpGain = 10;
    public float hpPenalty = 10;

    void Start() {
        instance = this;
        hp = maxHp;
    }

    void Update() {
        hp = Mathf.Max(hp - hpDrain * Time.deltaTime, 0);

        float scale = transform.localScale.x;
        float targetScale = 1.03f * hp / 100;

        transform.localScale = new Vector3(Mathf.Lerp(scale, targetScale, 10 * Time.deltaTime), 1, 1);
    }

    private void addHp() {
        hp = Mathf.Min(hp + hpGain, maxHp);
    }

    private void removeHp() {
        hp = Mathf.Max(hp - hpGain, 0);
    }

    public static void AddHp() {
        instance.addHp();
    }

    public static void RemoveHp() {
        instance.removeHp();
    }
}
