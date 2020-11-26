using System;
using System.Collections.Generic;
using UnityEngine;

public class StageManager : MonoBehaviour {

    public static StageManager instance;
    public TextAsset stageJSON;
    public List<GameObject> prefabs;

    private List<Command> commands;
    private Dictionary<string, GameObject> spawners;

    void Awake() {
        instance = this;
    }

    // Start is called before the first frame update
    void Start() {
        Init();
    }

    public void Init() {
        commands = new List<Command>();
        spawners = new Dictionary<string, GameObject>();
        ConstructCommands();
    }

    void ConstructCommands() {
        List<Spawner> spawners = JsonUtility.FromJson<SpawnerWrapper>(stageJSON.text).list;

        foreach (Spawner spawner in spawners) {
            List<float> args = new List<float>();
            args.Add(spawner.type);
            args.AddRange(spawner.position);
            args.AddRange(spawner.rotation);
            args.AddRange(spawner.args);

            commands.Add(new Command(spawner.id, spawner.time[0], args, "Create"));
            commands.Add(new Command(spawner.id, spawner.time[1], args, "Destroy"));

            foreach (Command cmd in spawner.commands) {
                cmd.id = spawner.id;
            }

            commands.AddRange(spawner.commands);
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
                    StartSpawner(commands[0]);
                    break;
                case "Destroy":
                    DestorySpawner(commands[0]);
                    break;
                default:
                    ExcuteCommand(commands[0]);
                    break;
            }
            commands.RemoveAt(0);
        }
    }

    private void StartSpawner(Command command) {
        GameObject prefab = prefabs[Convert.ToInt32(command.args[0])];
        command.args.RemoveAt(0);

        Vector3 vector = new Vector3(command.args[0], command.args[1], command.args[2]);
        command.args.RemoveRange(0, 3);

        Vector3 euler = new Vector3(command.args[0], command.args[1], command.args[2]);
        command.args.RemoveRange(0, 3);

        GameObject obj = Instantiate(prefab, vector, Quaternion.Euler(euler));

        obj.GetComponent<SpawnerScript>().Init(command.args);
        command.args.RemoveRange(0, 2);

        obj.GetComponent<IBulletSpawner>().Init(command.args);

        spawners.Add(command.id, obj);
    }

    private void DestorySpawner(Command command) {
        Destroy(spawners[command.id]);
        spawners.Remove(command.id);
    }

    private void ExcuteCommand(Command command) {
        string header = command.command.Split('.')[0];
        string method = command.command.Split('.')[1];
        MonoBehaviour script;

        switch (header) {
            case "Spin":
                script = spawners[command.id].GetComponent<SpinnerScript>();

                if (script == null) {
                    script = spawners[command.id].AddComponent<SpinnerScript>();
                }
                
                switch (method) {
                    case "Linear":
                        ((SpinnerScript)script).Linear(command.args);
                        break;
                    case "Smooth":
                        ((SpinnerScript)script).Smooth(command.args);
                        break;
                }
                break;
            case "Move":
                script = spawners[command.id].GetComponent<MoveObjScript>();

                if (script == null) {
                    script = spawners[command.id].AddComponent<MoveObjScript>();
                }
                switch (method) {
                    case "Linear":
                        ((MoveObjScript)script).Linear(command.args);
                        break;
                    case "Smooth":
                        ((MoveObjScript)script).Smooth(command.args);
                        break;
                }
                break;
            case "Blink":
                script = spawners[command.id].GetComponent<BlinkScript>();

                if (script == null) {
                    script = spawners[command.id].AddComponent<BlinkScript>();
                }
                switch (method) {
                    case "On":
                        ((BlinkScript)script).On(command.args);
                        break;
                    case "Off":
                        ((BlinkScript)script).Off();
                        break;
                }
                break;
            default:
                break;
        }
    }

}
