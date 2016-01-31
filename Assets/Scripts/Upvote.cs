using UnityEngine;
using System.Collections;
using UsefulThings;

public class Upvote : MonoBehaviour {

    private float scale = 1;
    private Vector3 referenceScale;

    public GameObject clickEffect;
    public GameObject soundEffect;

    void Start() {
        referenceScale = transform.localScale;
    }

    void Update() {
        if (1 - scale > 0.001) {
            scale = Mathf.Lerp(scale, 1, 5 * Time.deltaTime);
            transform.localScale = referenceScale * scale;
        }
    }

    public void clicked() {
        GameState.ClickUpvote();
        scale = 0.9f;
        GameObject effect = (GameObject)Instantiate(clickEffect);
        effect.transform.parent = transform.parent;
        effect.transform.localPosition = transform.localPosition;
        effect.transform.localRotation = transform.localRotation;
        effect.transform.localScale = transform.localScale;
        effect.transform.parent = null;
        AudioManager.PlaySoundEffect(soundEffect);
    }
}
