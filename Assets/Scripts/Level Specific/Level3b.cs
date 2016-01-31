using UnityEngine;
using System.Collections;
using UsefulThings;

public class Level3b : MonoBehaviour {

    public GameObject upvotePrefab;

    public Curve horizontalPositionDistribution;

    public Curve timeBeforeUpvoteSpawnDistribution;
    public Curve verticalUpvoteSpeedDistribution;
    public Curve upvoteScaleDistribution;

    public float scaleMultiplier = 1;

    private bool part1;

    void Update() {
        if (GameState.Downvotes > 320) {
            AudioManager.PlayYay(ref part1);
            scaleMultiplier = 2;
            GameHP.instance.hpPenalty = 30;
        }
    }

    void OnEnable() {
        part1 = false;
        scaleMultiplier = 1;
        StartCoroutine(spawnUpvotes());
    }

    private IEnumerator spawnUpvotes() {
        while (true) {
            float x = horizontalPositionDistribution.Evaluate(Random.value);
            float speed = verticalUpvoteSpeedDistribution.Evaluate(Random.value);
            float scale = upvoteScaleDistribution.Evaluate(Random.value) * scaleMultiplier;

            GameObject upvote = (GameObject)Instantiate(upvotePrefab, new Vector3(x, -15f, 2), Quaternion.identity);
            upvote.transform.localScale = new Vector3(scale, scale, 1);

            TranslateObject movement = upvote.AddComponent<TranslateObject>();
            movement.speed = new Vector3(0, speed, 0);

            yield return new WaitForSeconds(timeBeforeUpvoteSpawnDistribution.Evaluate(Random.value));
        }
    }
}
