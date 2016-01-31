using UnityEngine;
using System.Collections;

public class EqualizerBar : MonoBehaviour {

    public int frequencyChannel { get; set; }
	
	void Update () {
        transform.localScale = new Vector3(1, Mathf.Clamp(EqualizerUI.SpectrumData[frequencyChannel], 0.1f, 1), 1);
	}
}
