using UnityEngine;
using System.Collections;

public class SwitchUI : MonoBehaviour {

    public GameObject nextUI;
    public GameObject clickSfx;

    void Update() {
        if (Input.GetMouseButtonDown(0)) {
            nextUI.SetActive(true);
            AudioManager.PlaySoundEffect(clickSfx);
            gameObject.SetActive(false);
        }
    }
}
