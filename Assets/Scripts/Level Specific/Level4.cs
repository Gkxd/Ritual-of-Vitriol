using UnityEngine;
using System.Collections;
using UsefulThings;

public class Level4 : MonoBehaviour {

    public GameObject downvotePrefab;
    public GameObject upvotePrefab;
    public GameObject nextLevel;
    public GameObject partB;

    public Curve horizontalPositionDistribution;
    public Curve horizontalSpeedDistribution;

    public Curve timeBeforeDownvoteSpawnDistribution;
    public Curve verticalDownvoteSpeedDistribution;
    public Curve downvoteScaleDistribution;

    public Curve timeBeforeUpvoteSpawnDistribution;
    public Curve verticalUpvoteSpeedDistribution;
    public Curve upvoteScaleDistribution;

    private float verticalSpeedMultiplier = 1;
    private float timeMultiplier = 1;
    private float horizontalSpeedMultiplier = 1;
    private float scaleMultiplier = 1;

    private bool part1, part2, part3, part4, part5;

    void Update() {
        if (GameState.Downvotes > 200) {
            AudioManager.PlayYay(ref part1);
            verticalSpeedMultiplier = 1.5f;
            timeMultiplier = 0.5f;
        }
        if (GameState.Downvotes > 230) {
            AudioManager.PlayYay(ref part2);
            verticalSpeedMultiplier = 3;
            horizontalSpeedMultiplier = 1.5f;
            scaleMultiplier = 0.3f;
            GameHP.instance.hpDrain = 30;
            GameHP.instance.hpGain = 30;
        }
        if (GameState.Downvotes > 250) {
            AudioManager.PlayYay(ref part3);
            verticalSpeedMultiplier = 4;
            horizontalSpeedMultiplier = 2f;
            scaleMultiplier = 0.6f;
            timeMultiplier = 0.2f;
            GameHP.instance.hpDrain = 10;
            GameHP.instance.hpGain = 10;
        }
        if (GameState.Downvotes > 280) {
            AudioManager.PlayYay(ref part4);
            verticalSpeedMultiplier = 5;
            horizontalSpeedMultiplier = 3;
            scaleMultiplier = 0.8f;
            GameHP.instance.hpDrain = 20;
            partB.SetActive(true);
        }
        if (GameState.Downvotes > 360) {
            AudioManager.PlayYay(ref part5);
            nextLevel.SetActive(true);
            partB.SetActive(false);
            gameObject.SetActive(false);
        }
    }

    void OnEnable() {
        part1 = part2 = part3 = part4 = part5 = false;
        verticalSpeedMultiplier = horizontalSpeedMultiplier = scaleMultiplier = timeMultiplier = 1;
        StartCoroutine(spawnDownvotes());
        StartCoroutine(spawnUpvotes());
    }

    private IEnumerator spawnDownvotes() {
        while (true) {
            float x = horizontalPositionDistribution.Evaluate(Random.value);
            float horizontalSpeed = horizontalSpeedDistribution.Evaluate(Random.value);
            float verticalSpeed = verticalDownvoteSpeedDistribution.Evaluate(Random.value);
            float scale = downvoteScaleDistribution.Evaluate(Random.value) * scaleMultiplier;

            GameObject downvote = (GameObject)Instantiate(downvotePrefab, new Vector3(x, 6.3f, 1), Quaternion.identity);
            downvote.transform.localScale = new Vector3(scale, scale, 1);

            TranslateObject movement = downvote.AddComponent<TranslateObject>();
            movement.speed = new Vector3(horizontalSpeed * horizontalSpeedMultiplier, verticalSpeed * verticalSpeedMultiplier, 0);

            yield return new WaitForSeconds(timeBeforeDownvoteSpawnDistribution.Evaluate(Random.value) * timeMultiplier);
        }
    }

    private IEnumerator spawnUpvotes() {
        while (true) {
            float x = horizontalPositionDistribution.Evaluate(Random.value);
            float horizontalSpeed = horizontalSpeedDistribution.Evaluate(Random.value);
            float verticalSpeed = verticalUpvoteSpeedDistribution.Evaluate(Random.value);
            float scale = upvoteScaleDistribution.Evaluate(Random.value) * scaleMultiplier;

            GameObject upvote = (GameObject)Instantiate(upvotePrefab, new Vector3(x, -6.3f, 0), Quaternion.identity);
            upvote.transform.localScale = new Vector3(scale, scale, 1);

            TranslateObject movement = upvote.AddComponent<TranslateObject>();
            movement.speed = new Vector3(horizontalSpeed * horizontalSpeedMultiplier, verticalSpeed * verticalSpeedMultiplier, 0);

            yield return new WaitForSeconds(timeBeforeUpvoteSpawnDistribution.Evaluate(Random.value) * timeMultiplier);
        }
    }
}
