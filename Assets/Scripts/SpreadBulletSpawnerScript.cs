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

    public void Fire() {
        Vector3 vector = transform.localPosition;

        for (int i = 0; i < amount; i++) {
            GameObject bulletObject = Instantiate(bullet, vector, Quaternion.identity);

            var bulletScript = bulletObject.GetComponent<MovingBulletScript>();

            bulletScript.direction = transform.localRotation.eulerAngles.z - (range / 2);
            bulletScript.speed = Random.Range(minSpeed, maxSpeed);

            if (isEqually && amount > 1) {
                bulletScript.direction += range / (amount - 1) * (i + 1);
            } else {
                bulletScript.direction += Random.Range(0, range);
            }
        }
    }
}
