using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.instance.removeObj || GameManager.instance.gameActive) {
            UpdatePos();
        }
    }

    void UpdatePos() {
        Vector3 mousePos = Input.mousePosition;
        Vector3 worldPos = Camera.main.ScreenToWorldPoint(mousePos);

        worldPos.z = 0;

        transform.position = worldPos;
    }

    void OnTriggerEnter2D(Collider2D collision) {
        if (GameManager.instance.gameActive && collision.gameObject.tag == "deathObject") {
            GameManager.instance.GameOver(transform.position);
        }
    }
}
