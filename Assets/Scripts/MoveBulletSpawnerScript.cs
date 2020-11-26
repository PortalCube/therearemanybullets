using System.Collections.Generic;
using UnityEngine;

public class MoveBulletSpawnerScript : MonoBehaviour, IBulletSpawner {

    public float speed = 3f;
    public float direction;

    public GameObject bullet;

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
        return direction;
    }

    public void Init(List<float> args) { }

    public void Fire() {
        Vector3 vector = transform.localPosition;
        direction = transform.localRotation.eulerAngles.z;

        GameObject bulletObject = Instantiate(bullet, vector, Quaternion.identity);

        var bulletScript = bulletObject.GetComponent<MovingBulletScript>();

        bulletScript.direction = GetDirection();
        bulletScript.speed = GetSpeed();
        bulletScript.lockToParent = true;
        bulletScript.spawner = this;
    }

}
