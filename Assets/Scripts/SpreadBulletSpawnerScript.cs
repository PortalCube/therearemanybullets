using System.Collections.Generic;
using UnityEngine;

public class SpreadBulletSpawnerScript : MonoBehaviour, IBulletSpawner {

    public int amount = 5;
    public float minSpeed = 3f;
    public float maxSpeed = 3f;
    public float range = 0;
    public bool isEqually = true;

    public GameObject bullet;

    // Start is called before the first frame update
    void Start() {

    }

    // Update is called once per frame
    void Update() {
    }

    public void Init(List<float> args) {
        this.amount = (int)args[0];
        this.minSpeed = args[1];
        this.maxSpeed = args[2];
        this.range = args[3];
        this.isEqually = args[4] == 1;
    }

    public void Fire() {
        Vector3 vector = transform.localPosition;
        float direction = transform.localRotation.eulerAngles.z - (range / 2);

        for (int i = 1; i <= amount; i++) {
            GameObject bulletObject = Instantiate(bullet, vector, Quaternion.identity);

            var bulletScript = bulletObject.GetComponent<MovingBulletScript>();

            bulletScript.direction = direction;
            bulletScript.speed = Random.Range(minSpeed, maxSpeed);

            if (isEqually && amount > 1) {
                direction += range / (amount - 1);
            } else {
                bulletScript.direction += Random.Range(0, range);
            }
        }
    }
}
