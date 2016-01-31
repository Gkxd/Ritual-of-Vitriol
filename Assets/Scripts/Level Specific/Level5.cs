using UnityEngine;
using System.Collections;
using UsefulThings;

public class Level5 : MonoBehaviour {

    public GameObject downvotePrefab;
    public GameObject upvotePrefab;
    public GameObject nextLevel;
    public GameObject partB;


    public Curve timeBeforeSpawnDistribution;
    public Curve horizontalPositionDistribution;
    public Curve verticalPositionDistribution;

    public Curve scaleDistribution;
    public Curve mainSpeedDistribution;
    public Curve secondarySpeedDistribution;

    private float timeMultiplier = 1;
    private float scaleMultiplier = 1;
    private float mainSpeedMultiplier = 1;
    private float secondarySpeedMultiplier = 1;

    private bool part1, part2, part3, part4;

    void Update() {
        if (GameState.Downvotes > 400) {
            AudioManager.PlayYay(ref part1);
            timeMultiplier = 0.7f;
            scaleMultiplier = 0.9f;
            mainSpeedMultiplier = 2;
            secondarySpeedMultiplier = 1.5f;
        }
        if (GameState.Downvotes > 450) {
            AudioManager.PlayYay(ref part2);
            timeMultiplier = 0.5f;
            scaleMultiplier = 0.6f;
            mainSpeedMultiplier = 2.5f;
        }
        if (GameState.Downvotes > 500) {
            AudioManager.PlayYay(ref part3);
            timeMultiplier = 0.3f;
            scaleMultiplier = 0.4f;
            mainSpeedMultiplier = 5;
            GameHP.instance.hpDrain = 30;
            GameHP.instance.hpPenalty = 60;
        }
    }

    void OnEnable() {
        part1 = part2 = part3 = part4 = false;
        mainSpeedMultiplier = secondarySpeedMultiplier = scaleMultiplier = timeMultiplier = 1;
        StartCoroutine(spawnVotes());
    }

    private IEnumerator spawnVotes() {
        while (true) {

            float mainSpeed = mainSpeedDistribution.Evaluate(Random.value) * mainSpeedMultiplier;
            float secondarySpeed = secondarySpeedDistribution.Evaluate(Random.value) * secondarySpeedMultiplier;

            float horizontalSpawn = horizontalPositionDistribution.Evaluate(Random.value);
            float verticalSpawn = verticalPositionDistribution.Evaluate(Random.value);


            Vector3 spawnPosition;
            Vector3 speed;
            float x = Random.value;
            if (x < 0.25) { // Spawn from top
                spawnPosition = new Vector3(horizontalSpawn, 6.3f, 1);
                speed = new Vector3(secondarySpeed, -mainSpeed, 0);
            }
            else if (x < 0.5) { // Spawn from bottom
                spawnPosition = new Vector3(horizontalSpawn, -6.3f, 1);
                speed = new Vector3(secondarySpeed, mainSpeed, 0);
            }
            else if (x < 0.75) { // Spawn from left
                spawnPosition = new Vector3(-10.3f, verticalSpawn, 1);
                speed = new Vector3(mainSpeed, secondarySpeed, 0);
            }
            else { // Spawn from right
                spawnPosition = new Vector3(10.3f, verticalSpawn, 1);
                speed = new Vector3(-mainSpeed, secondarySpeed, 0);
            }

            x = Random.value;
            if (x < 0.5) {
                spawnPosition.z = 0;
            }
            GameObject vote = (GameObject)Instantiate((x < 0.5) ? downvotePrefab : upvotePrefab, spawnPosition, Quaternion.identity);

            float scale = scaleDistribution.Evaluate(Random.value) * scaleMultiplier;
            vote.transform.localScale = new Vector3(scale, scale, 1);
            TranslateObject movement = vote.AddComponent<TranslateObject>();

            movement.speed = speed;
            yield return new WaitForSeconds(timeBeforeSpawnDistribution.Evaluate(Random.value) * timeMultiplier);
        }
    }
}
