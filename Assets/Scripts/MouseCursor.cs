using UnityEngine;
using System.Collections;

public class MouseCursor : MonoBehaviour {

    public LayerMask clickLayers;
    public GameObject clickEffect;

    private ParticleSystem particleSystem;

    void Start() {
        transform.position = Vector3.Scale(Camera.main.ScreenToWorldPoint(Input.mousePosition), new Vector3(1, 1, 0));
        UnityEngine.Cursor.visible = false;
        //UnityEngine.Cursor.lockState = CursorLockMode.Locked;

        particleSystem = GetComponent<ParticleSystem>();
    }

    void Update() {
        transform.position = transform.position = Vector3.Scale(Camera.main.ScreenToWorldPoint(Input.mousePosition), new Vector3(1, 1, 0));

        float clampX = Mathf.Clamp(transform.position.x, -8.9f, 8.9f);
        float clampY = Mathf.Clamp(transform.position.y, -5, 5);

        transform.position = new Vector3(clampX, clampY, 0);

        if (Input.GetMouseButtonDown(0)) {
            particleSystem.Emit(100);
            Instantiate(clickEffect, transform.position, Quaternion.identity);
            RaycastHit clickInfo;
            if (Physics.Raycast(transform.position - Vector3.forward * 5, Vector3.forward, out clickInfo, 10f, clickLayers)) {
                Downvote downvote = clickInfo.collider.gameObject.GetComponent<Downvote>();
                if (downvote) {
                    downvote.clicked();
                }
                else {
                    Upvote upvote = clickInfo.collider.gameObject.GetComponent<Upvote>();
                    if (upvote) {
                        upvote.clicked();
                    }
                }
            }
        }
    }
}
