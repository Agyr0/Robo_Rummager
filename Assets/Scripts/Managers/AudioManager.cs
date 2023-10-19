using System.Collections;
using System.Collections.Generic;
using UnityEditor.ShaderGraph.Drawing.Inspector.PropertyDrawers;
using UnityEngine;

public class AudioManager : Singleton<AudioManager>
{
    [ArrayElementTitle("type")]
    public List<AudioGroup> ambientAudio = new List<AudioGroup>();
    [ArrayElementTitle("type")]
    public List<AudioGroup> effectAudio= new List<AudioGroup>();




    public AudioController FindClip(AudioType audioType, List<AudioGroup> soundGroup, bool randomize)
    {
        List<AudioController> foundClips = new List<AudioController>();
        int index = 0;

        for (int i = 0; i < soundGroup.Count; i++)
        {
            if (soundGroup[i].type == audioType)
            {
                if (!randomize)
                    return soundGroup[i].myControllers[0];

                index = Random.Range(0, soundGroup[i].myControllers.Count);
                    return soundGroup[i].myControllers[index];
            }
        }
        Debug.LogError("Could not find desired clip to return");
        return null;
    }

    public void PlayClip(AudioSource source, AudioController audioController)
    {
        #region Null Errors
        if (source == null)
        {
            Debug.LogError("Audio source not found");
            return;
        }
        if(audioController == null)
        {
            Debug.LogError("Audio clip not found. \nTry using FindClip on AudioManager");
            return;
        }
        #endregion

        source.clip = audioController.clip;
        source.volume = audioController.volume;
        source.pitch = audioController.pitch;
        source.loop = audioController.loop;

        source.Play();
    }

    public void PlayClip(AudioSource source, AudioController audioController, ulong clipDelay)
    {
        #region Null Errors
        if (source == null)
        {
            Debug.LogError("Audio source not found");
            return;
        }
        if (audioController == null)
        {
            Debug.LogError("Audio clip not found. \nTry using FindClip on AudioManager");
            return;
        }
        #endregion

        source.clip = audioController.clip;
        source.volume = audioController.volume;
        source.pitch = audioController.pitch;
        source.loop = audioController.loop;

        source.Play(clipDelay);
    }
}
