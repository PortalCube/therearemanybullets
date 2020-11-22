using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {

    public static GameManager instance;

    public int bulletCount = 0;
    public Text timeText;
    public Text bulletText;
    public bool gameActive = true;
    public bool removeObj = false;
    public AudioSource music;
    Camera cam;

    public float Time { get { return music.time; } }

    void Awake() {
        instance = this;
    }

    // Start is called before the first frame update
    void Start() {
        // 카메라 세팅
        cam = Camera.main;

        Init();
    }

    void Init(bool isReset = false) {
        StopAllCoroutines();

        // 마우스 세팅
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Confined;

        // 게임 상태 세팅
        gameActive = false;
        removeObj = true;

        // 음악 초기화
        music.Stop();
        music.time = 0;
        music.pitch = 1;

        // 카메라 초기화
        cam.orthographicSize = 5;
        cam.transform.position = new Vector3(0, 0, -10);

        StartCoroutine(ResumeGame(1f));
    }

    // Update is called once per frame
    void Update() {
        if (gameActive) {
            UpdateTime();
        }

        if (Input.GetKey(KeyCode.R) && !removeObj) {
            Init(true);
        }
    }

    void UpdateTime() {
        if (gameActive) {
            float sec = Time % 60f;
            int min = Mathf.FloorToInt(Time / 60);
            timeText.text = string.Format("Time: {0:00}:{1:00.00}", min, sec);
        }

        bulletText.text = string.Format("Bullets: {0}", bulletCount);
    }

    public void GameOver(Vector3 position) {
        gameActive = false;
        timeText.text = "Game Over! Press [R] to restart";

        // 음악 느려지며 중지 -- disabled
        StartCoroutine(MusicSlowdown(1f));

        // 카메라 확대
        StartCoroutine(CameraZoomIn(5f, position));
    }

    IEnumerator ResumeGame(float time) {
        while (time > 0) {
            time -= UnityEngine.Time.deltaTime;
            float sec = time % 60f;
            int min = Mathf.FloorToInt(time / 60);
            timeText.text = string.Format("Start in: {0:00}:{1:00.00}", min, sec);
            yield return null;
        }

        // 게임 상태 세팅
        removeObj = false;
        gameActive = true;

        // 음악 재생
        music.Play();
    }

    IEnumerator MusicSlowdown(float time) {
        float waitTime = time;

        while (waitTime > 0) {
            waitTime -= UnityEngine.Time.deltaTime;
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
            waitTime -= UnityEngine.Time.deltaTime;
            cam.orthographicSize = Mathf.SmoothDamp(cam.orthographicSize, 1.5f, ref s_velocity, 0.5f);
            cam.transform.position = Vector3.SmoothDamp(cam.transform.position, target, ref v_velocity, 0.5f);
            yield return null;
        }

    }
}
