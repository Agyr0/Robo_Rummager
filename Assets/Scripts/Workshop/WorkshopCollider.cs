using Agyr.Workshop;
using Agyr.CustomAttributes;
using UnityEngine;


[RequireComponent(typeof(BoxCollider))]
public class WorkshopCollider : MonoBehaviour
{
    [Space(20)]
#if UNITY_EDITOR
    [SerializeField, ReadOnly,
        Header("Set ONE Box Collider to cover the whole workshop zone")]
#endif 
    private string IMPORTANT = "";


    private void Start()
    {
        GetComponent<BoxCollider>().isTrigger = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            if(!WorkshopManager.Instance.WorkshopZone.ResourcesAvailable())
                EventBus.Publish(EventType.REFRESH_RESOURCES);

            AudioManager.Instance.ChangeAmbientAudio(AudioType.Workshop_Playlist);
            
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            AudioManager.Instance.ChangeAmbientAudio(AudioType.Scrapyard_Playlist);
        }
    }
}
