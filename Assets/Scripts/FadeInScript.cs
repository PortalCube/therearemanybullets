using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadeInScript : MonoBehaviour
{
    public float start = 0f;
    public float end = 0f;
    public float time = 1f;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(SmoothCoroutine(start, end, time));
    }

    // Update is called once per frame
    void Update()
    {

    }

    IEnumerator SmoothCoroutine(float start, float end, float time) {
        float t = 0.0f;
        SpriteRenderer spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        Color color = spriteRenderer.color;
        while (t <= 1.0f) {
            t += Time.deltaTime / time;
            color.a = Mathf.Lerp(start, end, Mathf.SmoothStep(0.0f, 1.0f, t));
            spriteRenderer.color = color;
            yield return null;
        }
    }
}
