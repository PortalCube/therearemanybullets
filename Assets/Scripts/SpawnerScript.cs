using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerScript : MonoBehaviour
{
    public float startTime = 0f;
    public float endTime = 999f;

    public float intervalOffset = 0;

    private float interval = 0.5f;
    private int count = 1;

    public float minInterval = 0.5f;
    public float maxInterval = 1f;

    public int minCount = 3;
    public int maxCount = 5;

    public MonoBehaviour spawner;
    IBulletSpawner bulletSpawner;

    float time = 0;
    int total = 0;

    // Start is called before the first frame update
    void Start()
    {
        interval = Random.Range(minInterval, maxInterval);
        count = Random.Range(minCount, maxCount);

        bulletSpawner = (IBulletSpawner)spawner;

        time = intervalOffset % interval;
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.instance.Time < startTime) {
            return;
        }

        if (GameManager.instance.Time > endTime) {
            return;
        }

        if (GameManager.instance.gameActive) {
            time += Time.deltaTime;

            if (time >= interval) {
                bulletSpawner.Fire();

                if (count > 0 && ++total >= count) {
                    Destroy(gameObject);
                }

                if (minInterval != maxInterval) {
                    interval = Random.Range(minInterval, maxInterval);
                }

                time = 0;
            }
        }
    }
}
