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
            
            Vector3 vector = new Vector3(Mathf.Cos(Mathf.Deg2Rad * direction), Mathf.Sin(Mathf.Deg2Rad * direction), 0);
            transform.Translate(vector * speed * Time.deltaTime);
        }
    }


    public void Linear(List<float> args) {
        direction = args[0];
        speed = args[1];
    }

    public void Smooth(List<float> args) {
        direction = args[0];
        StartCoroutine(SmoothCoroutine(args[1], args[2], args[3]));
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
