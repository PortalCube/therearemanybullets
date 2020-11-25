﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpinnerScript : MonoBehaviour {

    public float speed = 1f;
    private float _speed;

    // Start is called before the first frame update
    void Start() {
        _speed = speed * 10;
    }

    // Update is called once per frame
    void Update() {
        if (GameManager.instance.gameActive) {
            float add = _speed * Time.deltaTime;
            transform.localRotation = Quaternion.Euler(0, 0, transform.localRotation.eulerAngles.z + add);
        }
    }

    public void Linear(List<float> args) {
        _speed = args[0] * 10;
    }

    public void Smooth(List<float> args) {
        StartCoroutine(SmoothCoroutine(args[0], args[1]));
    }

    IEnumerator SmoothCoroutine(float value, float time) {
        float t = 0.0f;
        float start = _speed;
        float end = value * 10;
        while (t <= 1.0f) {
            t += Time.deltaTime / time;
            _speed = Mathf.Lerp(start, end, Mathf.SmoothStep(0.0f, 1.0f, t));
            yield return null;
        }
    }
}
