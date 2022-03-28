using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelGenerator : MonoBehaviour
{
    // Start is called before the first frame update
    private const float DISTANCE_TO_SPAWN_LEVEL = 75f;
    [SerializeField] private Transform startLevel;
    [SerializeField] private List<Transform> levelList;
    [SerializeField] private Transform player;

    private Vector3 lastEndPosition;
    void Awake()
    {
        lastEndPosition.x = startLevel.Find("EndPosition").position.x;
        lastEndPosition.y = -36.4f;

        // spawn ahead 3 level
        for (int i = 0; i < 3; i++)
        {
            SpawnLevel();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Vector3.Distance(player.position,lastEndPosition) < DISTANCE_TO_SPAWN_LEVEL)
        {
            SpawnLevel();
        }
    }

    private void SpawnLevel()
    {
        //grasshill y should be -36.4 to -32.0f to prevent higher platform reach far top camera
        int rng = Random.Range(0, levelList.Count);
        if (rng== 3 || rng == 8 || rng == 9 || rng ==10)
        {
            lastEndPosition.y = Random.Range(-36.4f,-32.0f);
        }
        else  lastEndPosition.y = Random.Range(-33.5f, -23.5f); // anything else should have more freedom y axis as being low platform

        lastEndPosition.x += Random.Range(0f, 4f);
        Transform randomLevel = levelList[rng];
        Transform lastRandomLevel = SpawnLevel(randomLevel,lastEndPosition);

        lastEndPosition.x = lastRandomLevel.Find("EndPosition").position.x;

        lastEndPosition.y = lastRandomLevel.transform.position.y;
    }

    private Transform SpawnLevel(Transform level, Vector3 spawnPosition)
    {
        Transform newLevel = Instantiate(level, spawnPosition, Quaternion.identity);
        return newLevel;
    }
}
