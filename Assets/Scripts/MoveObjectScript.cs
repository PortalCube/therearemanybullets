using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveObjScript : MonoBehaviour {

    public float direction = 0f;
    public float speed = 0f;

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


    public void Linear(List<float> args) {
        direction = args[0];
        speed = args[1];
    }

    public void Smooth(List<float> args) {
        direction = args[0];
        StartCoroutine(SmoothCoroutine(args[1], args[2]));
    }

    IEnumerator SmoothCoroutine(float value, float time) {
        float t = 0.0f;
        float start = speed;
        float end = value;
        while (t <= 1.0f) {
            t += Time.deltaTime / time;
            speed = Mathf.Lerp(start, end, Mathf.SmoothStep(0.0f, 1.0f, t));
            yield return null;
        }
    }
}
