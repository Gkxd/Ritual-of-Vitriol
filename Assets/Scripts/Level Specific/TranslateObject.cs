using UnityEngine;
using System.Collections;

public class TranslateObject : MonoBehaviour {

    public Vector3 speed;

    void Update() {
        transform.Translate(speed * Time.deltaTime);

        if (transform.position.y < -10) {
            Destroy(gameObject);
        }
        if (transform.position.y > 10) {
            Destroy(gameObject);
        }
    }
}
