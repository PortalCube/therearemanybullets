using UnityEngine;

public class WallBulletSpawnerScript : MonoBehaviour, IBulletSpawner {

    public int amount = 4;
    public float speed = 3f;
    public float direction = 0;
    public bool vertical = false;

    public GameObject movingBullet;

    // Start is called before the first frame update
    void Start() {
        transform.localRotation = Quaternion.Euler(0, 0, direction);
    }

    // Update is called once per frame
    void Update() {

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

            bulletScript.direction = direction;
            bulletScript.speed = speed;

            if (vertical) {
                vector += transform.right * 0.25f;
            } else {
                vector += transform.up * -0.25f;
            }


        }
    }
}
