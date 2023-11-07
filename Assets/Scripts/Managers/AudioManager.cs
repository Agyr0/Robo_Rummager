using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Agyr.CustomAttributes;

public class AudioManager : Singleton<AudioManager>
{
#if UNITY_EDITOR
    [ArrayElementTitle("type")]
#endif
    public List<AudioGroup> ambientAudio = new List<AudioGroup>();
#if UNITY_EDITOR
    [ArrayElementTitle("type")]
#endif
    public List<AudioGroup> effectAudio= new List<AudioGroup>();


    #region Formatting
    //Shhh this was so the descriptions would look better for function summaries

    /// <summary>
    /// Plays an audio clip (<paramref name="audioController"/>.clip) on the source (<paramref name="source"/>).
    /// <para>
    /// <i>Also sets the volume, pitch, and loop status of source to that of  (<paramref name="audioController"/>).</i>
    /// </para> 
    /// 
    /// <example>
    /// 
    /// <para>
    ///     <list>
    ///     <listheader>
    ///         <b>Example:</b>
    ///     </listheader>
    ///         <code>
    ///             <list>
    ///     <see cref="AudioManager"/>.Instance.PlayClip(source, AudioManager.Instance.FindRandomizedClip(AudioType.Gun, AudioManager.Instance.effectAudio));
    ///         - <see cref="FindRandomizedClip"/>
    ///             </list>
    ///             <list>
    ///     <see cref="AudioManager"/>.Instance.PlayClip(source, AudioManager.Instance.FindClip(AudioType.Gun, AudioManager.Instance.effectAudio));
    ///         - <see cref="FindClip"/>
    ///             </list>
    ///     
    ///         </code>
    ///     </list>
    /// </para>
    /// 
    /// </example>
    /// </summary>
    /// <param name="source"></param>
    /// <param name="audioController"></param>
    private void PlayClip() { }

    #endregion





    /// <summary>
    /// Searches through <paramref name="soundGroup"/> to find the correct <paramref name="audioType"/>. 
    /// <para> 
    /// Then plays the first clip of that <seealso cref="AudioType"/>.
    /// </para>
    /// </summary>
    /// <param name="audioType"></param>
    /// <param name="soundGroup"></param>
    /// <returns> <seealso cref="AudioController"/> </returns>
    public AudioController FindClip(AudioType audioType, List<AudioGroup> soundGroup)
    {

        for (int i = 0; i < soundGroup.Count; i++)
        {
            if (soundGroup[i].type == audioType)
            {
                return soundGroup[i].myControllers[0];
            }
        }
        Debug.LogError("Could not find desired clip to return");
        return null;
    }

    /// <summary>
    /// Searches through <paramref name="soundGroup"/> to find the correct <paramref name="audioType"/>. 
    /// <para> 
    /// Then plays a random clip of that <paramref name="audioType"/>.
    /// </para>
    /// </summary>
    /// <param name="audioType"></param>
    /// <param name="soundGroup"></param>
    /// <returns> <seealso cref="AudioController"/> </returns>
    public AudioController FindRandomizedClip(AudioType audioType, List<AudioGroup> soundGroup)
    {
        int index = 0;

        for (int i = 0; i < soundGroup.Count; i++)
        {
            if (soundGroup[i].type == audioType)
            {
                index = Random.Range(0, soundGroup[i].myControllers.Count);
                    return soundGroup[i].myControllers[index];
            }
        }
        Debug.LogError("Could not find desired clip to return");
        return null;
    }

    /// <summary>
    /// Searches through <paramref name="soundGroup"/> to find the correct <paramref name="audioType"/>. 
    /// </summary>
    /// <param name="audioType"></param>
    /// <param name="soundGroup"></param>
    /// <returns> A list of <see cref="AudioController"/> </returns>
    public List<AudioController> FindClipTypes(AudioType audioType, List<AudioGroup> soundGroup)
    {
        for (int i = 0; i < soundGroup.Count; i++)
        {
            if (soundGroup[i].type == audioType)
            {
                return soundGroup[i].myControllers;
            }
        }
        Debug.LogError("Could not find desired clip to return");
        return null;
    }

    

    /// <summary>
    /// <inheritdoc cref="PlayClip"/>
    /// </summary>
    /// <param name="source"></param>
    /// <param name="audioController"></param>
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


    /// <summary>
    /// <para>
    /// <i><b>Plays the clip with a specified delay in seconds (<paramref name="clipDelay"/>).</b></i>
    /// </para>
    /// <inheritdoc cref="PlayClip(AudioSource, AudioController)"/>
    /// </summary>
    /// <param name="source"></param>
    /// <param name="audioController"></param>
    /// <param name="clipDelay"></param>
    public void PlayClip(AudioSource source, AudioController audioController, float clipDelay)
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

        source.PlayDelayed(clipDelay);
    }



    /// <summary>
    /// Searches through <paramref name="audioController"/> and picks a random sound to play on the <paramref name="source"/> until stopped. 
    /// <para> 
    /// <i>Also sets the volume, pitch, and loop status of source to that of  (<paramref name="audioController"/>).</i>
    /// </para>
    /// <para> 
    /// <i>Also ensures the same clip won't play twice in a row.</i>
    /// </para>
    /// </summary>
    /// <param name="source"></param>
    /// <param name="audioController"></param>
    public IEnumerator LoopRandomizedClips(AudioSource source, List<AudioController> audioController)
    {
        #region Null Errors
        if (source == null)
        {
            Debug.LogError("Audio source not found");
            yield break;
        }
        if (audioController == null)
        {
            Debug.LogError("Audio clip list not found. \nTry using FindClipTypes on AudioManager");
            yield break;
        }
        #endregion

        int index = 0;
        int lastIndex = 0;
        while (true)
        {
            //Ensure last clip doesnt play again
            do 
            {
                index = Random.Range(0, audioController.Count);
                yield return null;
            } 
            while (index == lastIndex);

            source.clip = audioController[index].clip;
            source.volume = audioController[index].volume;
            source.pitch = audioController[index].pitch;

            source.Play();

            lastIndex = index;
            yield return new WaitUntil(() => source.isPlaying == false);
        }
    }




}
