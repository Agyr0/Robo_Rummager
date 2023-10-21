using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[System.Serializable]
public class AudioController
{
    public AudioClip clip;
    public AudioType type;

    public string name = "ChangeMe";

}

public enum AudioType
{
    Gun,
    Wrench,
    EnemyGrunt
}
