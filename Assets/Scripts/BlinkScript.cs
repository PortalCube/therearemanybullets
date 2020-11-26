using System.Collections.Generic;
using UnityEngine;

public class BlinkScript : MonoBehaviour {
    public float fireTime = 1f;
    public float sleepTime = 0f;
    public float offset = 0;

    private bool isActive = false;
    private float time = 0f;

    // Start is called before the first frame update
    void Start() {

    }

    public void On(List<float> args) {
        fireTime = args[0];
        sleepTime = args[1];
        offset = args[2];
        time = offset;
        isActive = true;
    }

    public void Off() {
        isActive = false;
        gameObject.GetComponent<SpawnerScript>().SetStatus(true);
    }

    // Update is called once per frame
    void Update() {
        if (isActive) {
            time += Time.deltaTime;

            if (time < fireTime) {
                gameObject.GetComponent<SpawnerScript>().SetStatus(true);
            } else if (time < fireTime + sleepTime) {
                gameObject.GetComponent<SpawnerScript>().SetStatus(false);
            } else {
                time = 0;
            }
        }
    }
}
