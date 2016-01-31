using UnityEngine;
using System.Collections;
using UsefulThings;

public class Level2 : MonoBehaviour {

    public GameObject downvotePrefab;
    public GameObject nextLevel;

    public Curve timeBeforeSpawnDistribution;
    public Curve horizontalPositionDistribution;
    public Curve verticalSpeedDistribution;
    public Curve scaleDistribution;

    void Update() {
        if (GameState.Downvotes > 25) {
            nextLevel.SetActive(true);
            AudioManager.PlayYay();
            gameObject.SetActive(false);
        }
    }

    void OnEnable() {
        StartCoroutine(spawnDownvotes());
    }

    private IEnumerator spawnDownvotes() {
        while (true) {
            float x = horizontalPositionDistribution.Evaluate(Random.value);
            float speed = verticalSpeedDistribution.Evaluate(Random.value);
            float scale = scaleDistribution.Evaluate(Random.value);

            GameObject downvote = (GameObject)Instantiate(downvotePrefab, new Vector3(x, 6.3f, 1), Quaternion.identity);
            downvote.transform.localScale = new Vector3(scale, scale, 1);

            TranslateObject movement = downvote.AddComponent<TranslateObject>();
            movement.speed = new Vector3(0, speed, 0);

            yield return new WaitForSeconds(timeBeforeSpawnDistribution.Evaluate(Random.value));
        }
    }
}
