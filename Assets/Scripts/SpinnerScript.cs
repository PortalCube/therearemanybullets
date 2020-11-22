using System.Collections;
using UnityEngine;

public class SpinnerScript : MonoBehaviour {

    public float speed = 1f;
    private float _speed;

    // Start is called before the first frame update
    void Start() {
        _speed = speed;
        StartCoroutine(SmoothChange(-50, 8));
    }

    // Update is called once per frame
    void Update() {
        if (GameManager.instance.gameActive) {
            float add = _speed * Time.deltaTime * 10;
            transform.localRotation = Quaternion.Euler(0, 0, transform.localRotation.eulerAngles.z + add);
        }
    }

    IEnumerator SmoothChange(float end, float time) {
        float t = 0.0f;
        float start = _speed;
        while (t <= 1.0f) {
            t += Time.deltaTime / time;
            _speed = Mathf.Lerp(start, end, Mathf.SmoothStep(0.0f, 1.0f, t));
            yield return null;
        }
    }
}
