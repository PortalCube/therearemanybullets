using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {

    public static GameManager instance;

    float time = 0;
    public Text timeText;
    public bool gameActive = true;
    public bool removeObj = false;
    public AudioSource music;
    Camera cam;

    void Awake() {
        instance = this;
    }

    // Start is called before the first frame update
    void Start() {
        // 카메라 세팅
        cam = Camera.main;

        // 마우스 세팅
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Confined;

        // 음악 재생 -- disabled
        // music.Play();
    }

    // Update is called once per frame
    void Update() {
        if (gameActive) {
            UpdateTime();
        }

        if (Input.GetKey(KeyCode.R) && !removeObj) {
            Reset();
        }
    }

    void UpdateTime() {
        time += Time.deltaTime;
        float sec = time % 60f;
        int min = Mathf.FloorToInt(time / 60);
        timeText.text = string.Format("Time: {0:00}:{1:00.00}\nPress [Alt+F4] to exit game", min, sec);
    }

    public void GameOver(Vector3 position) {
        gameActive = false;
        timeText.text = "Game Over! Press [R] to restart";

        // 음악 느려지며 중지 -- disabled
        //StartCoroutine(MusicSlowdown(1f));

        // 카메라 확대
        StartCoroutine(CameraZoomIn(5f, position));
    }

    public void Reset() {
        StopAllCoroutines();
        time = 0;
        StartCoroutine(ResetGame(3f));
    }

    IEnumerator ResetGame(float time) {
        gameActive = false;
        removeObj = true;

        music.Stop();

        cam.orthographicSize = 5;
        cam.transform.position = new Vector3(0, 0, -10);

        music.pitch = 1;

        float waitTime = time;

        while (waitTime > 0) {
            waitTime -= Time.deltaTime;
            float sec = waitTime % 60f;
            int min = Mathf.FloorToInt(waitTime / 60);
            timeText.text = string.Format("Start in: {0:00}:{1:00.00}", min, sec);
            yield return null;
        }

        music.Play();

        removeObj = false;
        gameActive = true;
    }

    IEnumerator MusicSlowdown(float time) {
        float waitTime = time;

        while (waitTime > 0) {
            waitTime -= Time.deltaTime;
            music.pitch = waitTime / time;
            yield return null;
        }

        music.Stop();
    }

    IEnumerator CameraZoomIn(float time, Vector3 position) {

        Vector3 target = position;

        Vector3 v_velocity = Vector3.zero;
        float s_velocity = 0f;

        target.z = -10;

        float waitTime = time;

        while (waitTime > 0) {
            waitTime -= Time.deltaTime;
            cam.orthographicSize = Mathf.SmoothDamp(cam.orthographicSize, 1.5f, ref s_velocity, 0.5f);
            cam.transform.position = Vector3.SmoothDamp(cam.transform.position, target, ref v_velocity, 0.5f);
            yield return null;
        }

    }
}
