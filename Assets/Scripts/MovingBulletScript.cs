using UnityEngine;

public class MovingBulletScript : MonoBehaviour {

    public float direction;
    public float speed;

    // Start is called before the first frame update
    void Start() {

    }

    // Update is called once per frame
    void Update() {
        if (GameManager.instance.gameActive) {
            Vector3 vector = new Vector3(speed * Time.deltaTime, 0, 0);

            // 각도 설정
            transform.localRotation = Quaternion.Euler(0, 0, direction);

            // 앞으로 전진
            transform.Translate(vector);
        }
    }


}
