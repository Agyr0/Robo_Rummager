using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class AudioGroup
{
    public string name = "ChangeMe";
    [ArrayElementTitle("name")]
    public List<AudioController> myControllers = new List<AudioController>();


}
