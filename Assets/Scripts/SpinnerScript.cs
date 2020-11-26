using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpinnerScript : MonoBehaviour {

    public float speed = 1f;

    // Start is called before the first frame update
    void Start() {

    }

    // Update is called once per frame
    void Update() {
        if (GameManager.instance.gameActive) {
            float add = speed * Time.deltaTime * 10;
            transform.localRotation = Quaternion.Euler(0, 0, transform.localRotation.eulerAngles.z + add);
        }
    }

    public void Linear(List<float> args) {
        speed = args[0];
    }

    public void Smooth(List<float> args) {
        StartCoroutine(SmoothCoroutine(args[0], args[1], args[2]));
    }

    IEnumerator SmoothCoroutine(float start, float end, float time) {
        float t = 0.0f;
        while (t <= 1.0f) {
            t += Time.deltaTime / time;
            speed = Mathf.Lerp(start, end, Mathf.SmoothStep(0.0f, 1.0f, t));
            yield return null;
        }
    }
}
