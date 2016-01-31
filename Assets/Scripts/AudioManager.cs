using UnityEngine;
using System.Collections;

public class AudioManager : MonoBehaviour {

    public static AudioManager instance;

    public AudioClip[] bgmPool;

    public GameObject yaySfx;
    
    [Range(0, 1)]
    public float bgmVolume;
    [Range(0, 1)]
    public float sfxVolume;

    private GameObject currentAudio;

    void Start() {
        instance = this;
        DontDestroyOnLoad(gameObject);

        currentAudio = new GameObject("BGM");
        currentAudio.transform.parent = transform;

        AudioSource audio = currentAudio.AddComponent<AudioSource>();
        audio.clip = bgmPool[0];
        audio.loop = true;
        audio.volume = bgmVolume;
        audio.Play();
    }

    public static void PlaySoundEffect(GameObject soundEffect) {
        Instantiate(soundEffect);
    }

    public static void PlayYay() {
        Instantiate(instance.yaySfx);
    }

    public static void PlayYay(ref bool played) {
        if (!played) {
            Instantiate(instance.yaySfx);
            played = true;
        }
    }
}
