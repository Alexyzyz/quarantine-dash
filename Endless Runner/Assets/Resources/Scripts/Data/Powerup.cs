using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Powerup
{
    public enum Type {
        HighJump, LiveUp, Invulnerable
    }

    public Type type;
    public string name;
    public float duration;

    public Powerup(Type type, string name, float duration) {
        this.type = type;
        this.name = name;
        this.duration = duration;
    }
}
