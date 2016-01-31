using UnityEngine;
using System.Collections;

public class BackgroundColorChange : MonoBehaviour {

    public Gradient color;

    private Camera backgroundCamera;

    void Start() {
        backgroundCamera = GetComponent<Camera>();
    }

	void Update () {
        backgroundCamera.backgroundColor = color.Evaluate(GameHP.PercentHP);
	}
}
