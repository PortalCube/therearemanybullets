using System;
using System.Collections.Generic;

[Serializable]
public class Art {
    public string id;
    public float[] position;
    public float[] rotation;
    public int type;
    public bool needCanvas;
    public float start;
    public float end;
}