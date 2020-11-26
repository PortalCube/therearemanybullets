using System.Collections.Generic;
using UnityEngine;

public class WallBulletSpawnerScript : MonoBehaviour, IBulletSpawner {

    public int amount = 4;
    public float speed = 3f;
    public bool vertical = false;

    public GameObject movingBullet;

    // Start is called before the first frame update
    void Start() {

    }

    // Update is called once per frame
    void Update() {

    }

    public float GetSpeed() {
        return speed;
    }

    public float GetDirection() {
        return transform.localRotation.eulerAngles.z;
    }

    public void Init(List<float> args) {
        amount = (int)args[0];
        speed = args[1];
        vertical = args[2] == 1;
    }

    public void Fire() {
        Vector3 vector = transform.localPosition;

        // right: 앞
        // up: 왼쪽
        // forward: 위

        vector += transform.right * 0.25f;

        if (!vertical) {
            vector += transform.up * ((amount - 1) / 2f) * 0.25f;
        }

        for (int i = 0; i < amount; i++) {
            GameObject bulletObject = Instantiate(movingBullet, vector, Quaternion.identity);

            var bulletScript = bulletObject.GetComponent<MovingBulletScript>();

            bulletScript.direction = GetDirection();
            bulletScript.speed = GetSpeed();

            if (vertical) {
                vector += transform.right * 0.25f;
            } else {
                vector += transform.up * -0.25f;
            }


        }
    }
}
