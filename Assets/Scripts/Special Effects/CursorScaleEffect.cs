using UnityEngine;
using System.Collections;

public class CursorScaleEffect : MonoBehaviour {
    public float targetScale;
    public float clickScale;

    private float scale;

	void Start () {
        scale = targetScale;
	}
	
	void Update () {

        transform.localScale = new Vector3(scale, scale, 1);

        scale = Mathf.Lerp(scale, targetScale, 10 * Time.deltaTime);

        if (Input.GetMouseButtonDown(0)) {
            scale = clickScale;
        }
	}
}
