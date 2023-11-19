using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Agyr.CustomAttributes;

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
    [SerializeField]
    private int _dropsLeft;

    [SerializeField, Header("Drops Rigidbody Effects")]
    private float _explosionForce = 2f;
    [SerializeField]
    private float _explosionRadius = 1f;
    [SerializeField]
    private float _upwardsModifier = 2f;

    public GameObject ResourceFullMesh
    {
        get
        {
            if (DropsLeft <= 0 && _resourceFullMesh != null)
            {
                transform.gameObject.SetActive(false);
                //if(gameObject.GetComponent<Collider>() != null && gameObject.GetComponent<Collider>().enabled != false)
                //    gameObject.GetComponent<Collider>().enabled = false;
            }

            else if (DropsLeft > 0 && !_resourceFullMesh.activeInHierarchy && _resourceFullMesh != null)
            {
                transform.gameObject.SetActive(true);
                //if(gameObject.GetComponent<Collider>() != null && gameObject.GetComponent<Collider>().enabled != true)
                //    gameObject.GetComponent<Collider>().enabled = true;
            }

            return transform.gameObject;
        }
    }
    public int MaxDrops
    { get { return _maxDrops; } set { _maxDrops = value; } }
    public int DropsLeft
    { get { return _dropsLeft; } set { _dropsLeft = value; } }

    private void Start()
    {
        RefreshDrops();
    }

    public void RefreshDrops()
    {
        DropsLeft = MaxDrops;
        if (_resourceFullMesh != null)
        {
            GameObject temp = ResourceFullMesh;
        }

    }



    public void DropResource(Vector3 hitPosition)
    {
        //Pick a random item in the drop table to drop
        int itemToDrop = Random.Range(0, _dropTable.Count);

        if (DropsLeft > 0)
        {

            for (int i = 0; i < _dropTable.Count; i++)
            {
                if (_dropTable[itemToDrop].CheckDrop())
                {

                    //Instantiate(_dropTable[itemToDrop].Resource, hitPosition, Quaternion.identity);
                    GameObject drop = ObjectPooler.PullObjectFromPool(_dropTable[itemToDrop].Resource);
                    drop.GetComponent<Resource_Item>().ResourceAmount = 1;
                    drop.GetComponent<Resource_Item>().IsReadyForPickup = false;
                    drop.GetComponent<Resource_Item>().PickupTimerCount = 1;
                    drop.transform.position = hitPosition;
                    drop.transform.rotation = Quaternion.identity;
                    drop.SetActive(true);

                    Rigidbody rb = drop.GetComponent<Rigidbody>();
                    if (rb != null)
                    {
                        rb.AddExplosionForce(_explosionForce, hitPosition, _explosionRadius, _upwardsModifier);
                    }

                    DropsLeft--;
                }
            }
            if (_resourceFullMesh != null)
            {
                GameObject temp = ResourceFullMesh;
            }
        }
    }
}
