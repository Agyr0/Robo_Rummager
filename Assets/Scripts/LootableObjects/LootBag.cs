using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class LootBag : MonoBehaviour
{
    #if UNITY_EDITOR 
    [ArrayElementTitle("elementName")] 
    #endif
    public List<DropTableItem> _dropTable = new List<DropTableItem>();

    [SerializeField]
    private GameObject _resourceFullMesh;
    [SerializeField, Tooltip("Number of times this lootable item can drop something")]
    private int _maxDrops = 10;
    private int _dropsLeft;

    public GameObject ResourceFullMesh
    {
        get 
        {
            if (DropsLeft <= 0)
                _resourceFullMesh.SetActive(false);
            else if (DropsLeft > 0 && !_resourceFullMesh.activeInHierarchy)
                _resourceFullMesh.SetActive(true);

            return _resourceFullMesh; 
        } 
    }
    public int MaxDrops
    { get { return _maxDrops; } set {  _maxDrops = value; } }
    public int DropsLeft
    { get { return _dropsLeft; } set { _dropsLeft = value; } }


    private void OnEnable()
    {
        EventBus.Subscribe(EventType.REFRESH_RESOURCES, RefreshDrops);
    }
    private void OnDisable()
    {
        EventBus.Unsubscribe(EventType.REFRESH_RESOURCES, RefreshDrops);
    }

    private void RefreshDrops()
    {
        DropsLeft = MaxDrops;
        GameObject temp = ResourceFullMesh;

    }



    public void DropResource(Vector3 hitPosition)
    {
        //Pick a random item in the drop table to drop
        int itemToDrop = Random.Range(0, _dropTable.Count);

        for (int i = 0; i < DropsLeft; i++)
        {
            if(_dropTable[itemToDrop].CheckDrop())
            {
                GameObject drop = Instantiate(_dropTable[itemToDrop].Resource, hitPosition, Quaternion.identity);
                Rigidbody rb = drop.GetComponent<Rigidbody>();
                if (rb != null)
                {
                    rb.AddExplosionForce(5f, hitPosition, 1f, 2f);
                }
                    
            }
            DropsLeft--;
        }
        GameObject temp = ResourceFullMesh;
    }
}
