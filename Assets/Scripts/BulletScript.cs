using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.instance.removeObj) {
            Destroy(gameObject);
        }
    }

    void OnTriggerExit2D(Collider2D collision) {
        // 게임 구역을 나가는 경우, 제거
        if (collision.gameObject.tag == "playArea") {
            Destroy(gameObject);
        }
    }
}
