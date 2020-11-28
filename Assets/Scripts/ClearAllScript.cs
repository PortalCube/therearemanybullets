using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClearAllScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void All() {
        GameManager.instance.removeObj = true;
        StartCoroutine(ClearAndReset());
    }


    IEnumerator ClearAndReset() {
        float t = 0.0f;
        while (t <= 0.005f) {
            t += Time.deltaTime;
            yield return null;
        }
        GameManager.instance.removeObj = false;
    }
}
