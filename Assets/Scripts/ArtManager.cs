using System;
using System.Collections.Generic;
using UnityEngine;

public class ArtManager : MonoBehaviour {

    public static ArtManager instance;
    public TextAsset artJSON;
    public GameObject canvas;
    public List<GameObject> prefabs;

    private List<Command> commands;
    private Dictionary<string, GameObject> arts;

    void Awake() {
        instance = this;
    }

    // Start is called before the first frame update
    void Start() {
        Init();
    }

    public void Init() {
        commands = new List<Command>();
        arts = new Dictionary<string, GameObject>();
        ConstructCommands();
    }

    void ConstructCommands() {
        List<Art> arts = JsonUtility.FromJson<ArtWrapper>(artJSON.text).list;

        foreach (Art art in arts) {
            List<float> args = new List<float>();
            args.Add(art.type);
            args.AddRange(art.position);
            args.AddRange(art.rotation);
            args.Add(art.needCanvas ? 1 : 0);

            commands.Add(new Command(art.id, art.start, args, "Create"));
            commands.Add(new Command(art.id, art.end, args, "Destroy"));
        }

        commands.Sort((a, b) => {
            if (a.time < b.time) {
                return -1;
            } else if (a.time > b.time) {
                return 1;
            } else {
                if (a.command == "Create") {
                    return -1;
                } else if (b.command == "Create") {
                    return 1;
                } else {
                    return 0;
                }
            }
        });
    }

    // Update is called once per frame
    void Update() {
        if (!GameManager.instance.gameActive) {
            return;
        }

        if (commands.Count > 0 && commands[0].time < GameManager.instance.Time) {
            switch (commands[0].command) {
                case "Create":
                    StartArt(commands[0]);
                    break;
                case "Destroy":
                    DestoryArt(commands[0]);
                    break;
            }
            commands.RemoveAt(0);
        }
    }

    private void StartArt(Command command) {
        GameObject obj;
        GameObject prefab = prefabs[Convert.ToInt32(command.args[0])];
        command.args.RemoveAt(0);

        Vector3 vector = new Vector3(command.args[0], command.args[1], command.args[2]);
        command.args.RemoveRange(0, 3);

        Vector3 euler = new Vector3(command.args[0], command.args[1], command.args[2]);
        command.args.RemoveRange(0, 3);

        if (command.args[0] == 1) {
            obj = Instantiate(prefab);
            obj.transform.SetParent(canvas.transform);
            obj.transform.localPosition = vector;
        } else {
            obj = Instantiate(prefab, vector, Quaternion.Euler(euler));
        }

        arts.Add(command.id, obj);
    }

    private void DestoryArt(Command command) {
        Destroy(arts[command.id]);
        arts.Remove(command.id);
    }
}
