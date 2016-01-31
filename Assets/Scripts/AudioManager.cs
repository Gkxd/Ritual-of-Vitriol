using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class AudioManager : MonoBehaviour {

    public static AudioManager instance;

    public AudioClip[] bgmPool;
    public string[] bgmCredits;

    public GameObject yaySfx;
    public Text bgmCreditUI;
    
    [Range(0, 1)]
    public float bgmVolume;
    [Range(0, 1)]
    public float sfxVolume;

    private GameObject currentAudio;
    private AudioSource bgmAudioSource;
    private int bgmIndex = 0;

    void Start() {
        instance = this;
        DontDestroyOnLoad(gameObject);

        currentAudio = new GameObject("BGM");
        currentAudio.transform.parent = transform;

        bgmAudioSource = currentAudio.AddComponent<AudioSource>();
        bgmAudioSource.volume = bgmVolume;

        bgmAudioSource.clip = bgmPool[bgmIndex];
        bgmCreditUI.text = "BGM\n" + bgmCredits[bgmIndex];

        bgmAudioSource.Play();

        StartCoroutine(switchAudio());
    }

    private IEnumerator switchAudio() {
        while (true) {
            if (!bgmAudioSource.isPlaying) {
                yield return new WaitForSeconds(2);
                bgmIndex = (bgmIndex + Random.Range(1, bgmPool.Length)) % bgmPool.Length;

                bgmAudioSource.clip = bgmPool[bgmIndex];
                bgmCreditUI.text = "BGM\n" + bgmCredits[bgmIndex];

                bgmAudioSource.Play();
            }
            yield return null;
        }
    }

    void Update() {
        if (Input.GetKeyDown(KeyCode.LeftArrow)) {
            bgmIndex = (bgmIndex + bgmPool.Length - 1) % bgmPool.Length;

            bgmAudioSource.clip = bgmPool[bgmIndex];
            bgmCreditUI.text = "BGM\n" + bgmCredits[bgmIndex];

            bgmAudioSource.Play();
        }
        if (Input.GetKeyDown(KeyCode.RightArrow)) {
            bgmIndex = (bgmIndex + bgmPool.Length + 1) % bgmPool.Length;

            bgmAudioSource.clip = bgmPool[bgmIndex];
            bgmCreditUI.text = "BGM\n" + bgmCredits[bgmIndex];

            bgmAudioSource.Play();
        }
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
