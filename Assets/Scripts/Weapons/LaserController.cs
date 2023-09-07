using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserController : MonoBehaviour
{
    [SerializeField]
    private float _speed = 40f;

    
    private void Update()
    {
        transform.position += transform.forward * Time.deltaTime * _speed;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject != null)
        {
            //Destroy(gameObject);
        }
    }
}
