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
    public int fps = 60;
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
        timeText.text = string.Format("Time: {0:00}:{1:00.00}", min, sec);
    }

    public void GameOver() {
        gameActive = false;
        timeText.text = "Game Over!";
        
        // 음악 느려지며 중지
        StartCoroutine(MusicSlowdown(1f, fps));

        // 카메라 확대
        StartCoroutine(CameraZoomIn(5f, fps));
    }

    public void Reset() {
        StopAllCoroutines();
        time = 0;
        StartCoroutine(ResetGame(3f, fps));
    }

    IEnumerator ResetGame(float time, int fps) {
        gameActive = false;
        removeObj = true;

        music.Stop();

        cam.orthographicSize = 5;
        cam.transform.position = new Vector3(0, 0, -10);

        music.pitch = 1;

        float waitTime = time;

        for (int i = 0; i < time * fps; i++) {
            waitTime -= 1f / fps;
            float sec = waitTime % 60f;
            int min = Mathf.FloorToInt(waitTime / 60);
            timeText.text = string.Format("Start in: {0:00}:{1:00.00}", min, sec);
            yield return new WaitForSecondsRealtime(1f / fps);
        }

        music.Play();

        removeObj = false;
        gameActive = true;
    }

    IEnumerator MusicSlowdown(float time, int fps) {

        for (int i = 0; i < time * fps; i++) {
            music.pitch = 1 - (i / (float)fps);
            yield return new WaitForSecondsRealtime(1f / fps);
        }

        music.Stop();
    }

    IEnumerator CameraZoomIn(float time, int fps) {

        Vector3 target = cam.ScreenToWorldPoint(Input.mousePosition);

        Vector3 v_velocity = Vector3.zero;
        float s_velocity = 0f;

        target.z = -10;

        for (int i = 0; i < time * fps; i++) {
            cam.orthographicSize = Mathf.SmoothDamp(cam.orthographicSize, 2f, ref s_velocity, 0.08f);
            cam.transform.position = Vector3.SmoothDamp(cam.transform.position, target, ref v_velocity, 0.08f);
            yield return new WaitForSecondsRealtime(1f / fps);
        }

    }
}
