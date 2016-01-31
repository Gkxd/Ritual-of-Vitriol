using UnityEngine;
using System.Collections;

public class EqualizerUI : MonoBehaviour {

    public static float[] SpectrumData;

    void Start() {
        SpectrumData = new float[512];
        int i = 0;
        foreach (Transform t in transform) {
            t.name = "Bar " + i;
            t.Find("Image").GetComponent<EqualizerBar>().frequencyChannel = i++;
        }
    }

    void Update() {
        AudioListener.GetSpectrumData(SpectrumData, 0, FFTWindow.Rectangular);
    }
}
