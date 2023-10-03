using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using static UnityEngine.Rendering.DebugUI;

public class ItemGlowController : MonoBehaviour
{
    [SerializeField]
    private Animator m_Animator;
    [SerializeField]
    private float fadeIn = .25f;
    [SerializeField]
    private float fadeOut = .75f;
    private float startVal = 0f;
    private float midVal = 1f;
    private float endVal = 0f;

    public bool running, toggle = false;

   
    private void OnEnable()
    {
        EventBus.Subscribe(EventType.TOGGLE_SCANNER, ToggleGlow);
    }
    private void OnDisable()
    {
        EventBus.Unsubscribe(EventType.TOGGLE_SCANNER, ToggleGlow);
    }

    //If player closes goggles when still fading cut to endVal
    private void ToggleGlow()
    {
        toggle = !toggle;
        if(!toggle)
        {
            m_Animator.ResetTrigger("Play");
            m_Animator.ResetTrigger("StopPlay");
            m_Animator.SetTrigger("StopPlay");
        }
    }

    public void PlayGlow()
    {
        m_Animator.ResetTrigger("StopPlay");
        m_Animator.ResetTrigger("Play");
        m_Animator.SetTrigger("Play");
    }


}
