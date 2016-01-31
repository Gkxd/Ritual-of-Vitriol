using UnityEngine;
using System.Collections;
using UsefulThings;

public class BackgroundSquare : MonoBehaviour {
    public int row { get; set; }
    public int col { get; set; }
    public int spectrumChannels { get; set; }

    public Gradient gradient;

    private int frequencyChannel;

    private Material material;

    void Start() {
        frequencyChannel = ((47 * row + 256 * col) % spectrumChannels + spectrumChannels) % spectrumChannels;
        material = GetComponent<Renderer>().material;
    }

    void Update() {
        float scale = BackgroundEffect.SpectrumData[frequencyChannel]*5 + 0.7f;
        transform.localScale = new Vector3(scale, scale, 1);

        material.color = gradient.Evaluate(GameHP.PercentHP);
    }
}
