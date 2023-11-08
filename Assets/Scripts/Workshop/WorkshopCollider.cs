using Agyr.Workshop;
using Agyr.CustomAttributes;
using UnityEngine;


[RequireComponent(typeof(BoxCollider))]
public class WorkshopCollider : MonoBehaviour
{
    [Space(20)]
    [SerializeField, ReadOnly, 
        Header("Set ONE Box Collider to cover the whole workshop zone")]
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
            
        }
    }
}
