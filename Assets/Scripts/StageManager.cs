using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StageManager : MonoBehaviour {

    public static StageManager instance;
    public TextAsset stageJSON;
    public List<GameObject> prefabs;

    private List<Command> commands;
    private List<GameObject> spawners;
    private Dictionary<string, Action<List<int>>> functions;

    void Awake() {
        instance = this;
    }

    // Start is called before the first frame update
    void Start() {
        Init();
    }

    void Init() {
        commands = new List<Command>();
        spawners = new List<GameObject>();
        functions = new Dictionary<string, Action<List<int>>>();
        ConstructCommands();
    }

    void ConstructCommands() {
        List<Spawner> spawners = JsonUtility.FromJson<SpawnerWrapper>(stageJSON.text).list;

        foreach (Spawner spawner in spawners) {
            commands.Add(new Command(spawner.id, spawner.time[0], "Start"));
            commands.Add(new Command(spawner.id, spawner.time[1], "Destroy"));

            foreach (Command cmd in spawner.commands) {
                cmd.id = spawner.id;
            }

            commands.AddRange(spawner.commands);
        }

        commands.Sort((a, b) => (int)(a.time - b.time));
    }

    void ConstructDictionary() {

    }

    //public void RegisterAction(string key, Action<List<int>> action) {
    //    functions.Add(key, action);
    //}

    // Update is called once per frame
    void Update() {


    }

}
