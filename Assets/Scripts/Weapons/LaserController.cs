using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserController : MonoBehaviour
{
    [SerializeField]
    private float _speed = 40f;
    [SerializeField]
    private GameObject hitEffect;

    private void Start()
    {
        Destroy(gameObject, 10f);
    }

    private void Update()
    {
        transform.position += transform.forward * Time.deltaTime * _speed;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer != LayerMask.NameToLayer("Player"))
        {
            Destroy(gameObject);
            if(hitEffect != null)
                Instantiate(hitEffect, transform.position, Quaternion.identity);
        }
    }
}
