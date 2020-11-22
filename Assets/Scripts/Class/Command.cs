using System;
using System.Collections.Generic;

[Serializable]
public class Command{
    public string id;
    public float time;
    public string command;
    public List<float> args;

    public Command() { }

    public Command(string id, float time, string command) {
        this.id = id;
        this.time = time;
        this.command = command;
        args = new List<float>();
    }
}