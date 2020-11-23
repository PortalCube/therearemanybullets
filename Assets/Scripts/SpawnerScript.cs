using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerScript : MonoBehaviour
{
    public float interval = 0.5f;
    public float intervalOffset = 0;

    public int count = 1;

    public MonoBehaviour spawner;
    IBulletSpawner bulletSpawner;

    float time = 0;
    int total = 0;

    // Start is called before the first frame update
    void Start()
    {
        bulletSpawner = (IBulletSpawner)spawner;

        time = intervalOffset % interval;
    }

    // Update is called once per frame
    void Update()
    {
        if (!GameManager.instance.gameActive) {
            Destroy(gameObject);
        }

        time += Time.deltaTime;

        if (time >= interval) {
            bulletSpawner.Fire();

            if (count > 0 && ++total >= count) {
                Destroy(gameObject);
            }

            time = 0;
        }
    }
}
