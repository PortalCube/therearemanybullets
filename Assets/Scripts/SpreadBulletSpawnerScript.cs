using UnityEngine;

public class SpreadBulletSpawnerScript : MonoBehaviour, IBulletSpawner {

    public int amount = 5;
    public float minSpeed = 3f;
    public float maxSpeed = 3f;
    public float direction = 0;
    public float range = 0;
    public bool isEqually = true;

    public GameObject bullet;

    // Start is called before the first frame update
    void Start() {
        transform.localRotation = Quaternion.Euler(0, 0, direction);
    }

    // Update is called once per frame
    void Update() {
    }

    public void Fire() {
        Vector3 vector = transform.localPosition;

        for (int i = 0; i < amount; i++) {
            GameObject bulletObject = Instantiate(bullet, vector, Quaternion.identity);

            var bulletScript = bulletObject.GetComponent<MovingBulletScript>();

            bulletScript.direction = direction - (range / 2);
            bulletScript.speed = Random.Range(minSpeed, maxSpeed);

            if (isEqually && amount > 1) {
                bulletScript.direction += range / (amount - 1) * i;
            } else {
                bulletScript.direction += Random.Range(0, range);
            }
        }
    }
}
