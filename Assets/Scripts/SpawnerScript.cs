using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerScript : MonoBehaviour
{
    public float interval = 0.5f;

    public float minInterval = 0.5f;
    public float maxInterval = 1f;

    public float offset = 0;

    public int count = 1;

    public int minCount = 3;
    public int maxCount = 5;

    public bool randomInterval = false;
    public bool randomCount = false;

    public MonoBehaviour spawner;
    IBulletSpawner bulletSpawner;

    float time = 0;
    int total = 0;

    // Start is called before the first frame update
    void Start()
    {
        if (randomInterval) {
            interval = Random.Range(minInterval, maxInterval);
        }

        if (randomCount) {
            count = Random.Range(minCount, maxCount);
        }

        bulletSpawner = (IBulletSpawner)spawner;

        time = offset % interval;
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.instance.gameActive) {
            time += Time.deltaTime;

            if (time >= interval) {
                bulletSpawner.Fire();

                if (count > 0 && ++total >= count) {
                    Destroy(gameObject);
                }

                if (randomInterval) {
                    interval = Random.Range(minInterval, maxInterval);
                }

                time = 0;
            }
        }
    }
}
