using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IBulletSpawner {

    float GetSpeed();

    float GetDirection();

    void Fire();

    void Init(List<float> args);
}