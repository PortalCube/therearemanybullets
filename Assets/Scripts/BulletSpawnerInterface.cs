using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IBulletSpawner {
    void Fire();

    void Init(List<float> args);
}