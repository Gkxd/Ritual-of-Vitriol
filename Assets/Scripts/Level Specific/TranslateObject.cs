using UnityEngine;
using System.Collections;

public class TranslateObject : MonoBehaviour {

    public Vector3 speed;

    void Update() {
        transform.Translate(speed * Time.deltaTime);

        if (transform.position.y < -20) {
            Destroy(gameObject);
        }
        else if (transform.position.y > 20) {
            Destroy(gameObject);
        }
        if (transform.position.x > 30) {
            Destroy(gameObject);
        }
        else if (transform.position.x < -30) {
            Destroy(gameObject);
        }
    }
}
