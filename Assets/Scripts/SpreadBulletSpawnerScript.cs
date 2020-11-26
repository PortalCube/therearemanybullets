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

    public float GetSpeed() {
        return Random.Range(minSpeed, maxSpeed);
    }

    public float GetDirection() {
        return transform.localRotation.eulerAngles.z - (range / 2);
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

        for (int i = 1; i <= amount; i++) {
            GameObject bulletObject = Instantiate(bullet, vector, Quaternion.identity);

            var bulletScript = bulletObject.GetComponent<MovingBulletScript>();

            bulletScript.direction = GetDirection();
            bulletScript.speed = GetSpeed();

            if (isEqually && amount > 1) {
                bulletScript.direction += range / (amount - 1) * i;
            } else {
                bulletScript.direction += Random.Range(0, range);
            }
        }
    }
}
