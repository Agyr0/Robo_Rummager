using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using Unity.VisualScripting;
using UnityEngine;

[System.Serializable]
public class AudioController
{
    public AudioClip clip;
    [Range(0, 1)]
    public float volume = 1f;
    [Range(-3, 3)]
    public float pitch = 1f;
    public bool loop = false;

    public string name = "ChangeMe";

}


