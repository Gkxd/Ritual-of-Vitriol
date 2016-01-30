using UnityEngine;
using System.Collections;

public class AudioManager : MonoBehaviour {

    public AudioClip[] bgmPool;

    private GameObject currentAudio;

    void Start() {
        DontDestroyOnLoad(gameObject);

        currentAudio = new GameObject("BGM");
        currentAudio.transform.parent = transform;

        AudioSource audio = currentAudio.AddComponent<AudioSource>();
        audio.clip = bgmPool[0];
        audio.Play();
    }

    public static void PlaySoundEffect(GameObject soundEffect) {
        Instantiate(soundEffect);
    }
}
