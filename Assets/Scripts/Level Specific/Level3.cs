using UnityEngine;
using System.Collections;
using UsefulThings;

public class Level3 : MonoBehaviour {

    public GameObject downvotePrefab;
    public GameObject upvotePrefab;
    public GameObject nextLevel;

    public Curve horizontalPositionDistribution;

    public Curve timeBeforeDownvoteSpawnDistribution;
    public Curve verticalDownvoteSpeedDistribution;
    public Curve downvoteScaleDistribution;

    public Curve timeBeforeUpvoteSpawnDistribution;
    public Curve verticalUpvoteSpeedDistribution;
    public Curve upvoteScaleDistribution;

    private float speedMultiplier = 1;
    private float timeMultiplier = 1;

    void Update() {
        if (GameState.Downvotes > 50) {
            speedMultiplier = 2;
            timeMultiplier = 0.5f;
        }
        if (GameState.Downvotes > 70) {
            speedMultiplier = 3;
            timeMultiplier = 0.2f;
        }
        if (GameState.Downvotes > 100) {
            speedMultiplier = 4;
            timeMultiplier = 0.1f;
        }
    }

    void OnEnable() {
        StartCoroutine(spawnDownvotes());
        StartCoroutine(spawnUpvotes());
    }

    private IEnumerator spawnDownvotes() {
        while (true) {
            float x = horizontalPositionDistribution.Evaluate(Random.value);
            float speed = verticalDownvoteSpeedDistribution.Evaluate(Random.value);
            float scale = downvoteScaleDistribution.Evaluate(Random.value);

            GameObject downvote = (GameObject)Instantiate(downvotePrefab, new Vector3(x, 6.3f, 1), Quaternion.identity);
            downvote.transform.localScale = new Vector3(scale, scale, 1);

            TranslateObject movement = downvote.AddComponent<TranslateObject>();
            movement.speed = new Vector3(0, speed * speedMultiplier, 0);

            yield return new WaitForSeconds(timeBeforeDownvoteSpawnDistribution.Evaluate(Random.value) * timeMultiplier);
        }
    }

    private IEnumerator spawnUpvotes() {
        while (true) {
            float x = horizontalPositionDistribution.Evaluate(Random.value);
            float speed = verticalUpvoteSpeedDistribution.Evaluate(Random.value);
            float scale = upvoteScaleDistribution.Evaluate(Random.value);

            GameObject upvote = (GameObject)Instantiate(upvotePrefab, new Vector3(x, -6.3f, 0), Quaternion.identity);
            upvote.transform.localScale = new Vector3(scale, scale, 1);

            TranslateObject movement = upvote.AddComponent<TranslateObject>();
            movement.speed = new Vector3(0, speed * speedMultiplier, 0);

            yield return new WaitForSeconds(timeBeforeUpvoteSpawnDistribution.Evaluate(Random.value) * timeMultiplier);
        }
    }
}
