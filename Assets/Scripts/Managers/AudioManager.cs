using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : Singleton<AudioManager>
{
    [ArrayElementTitle("name")]
    public List<AudioGroup> ambientAudio = new List<AudioGroup>();
    [ArrayElementTitle("name")]
    public List<AudioGroup> effectAudio= new List<AudioGroup>();


}
