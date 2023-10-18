using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScavengerBullets : MonoBehaviour
{
    [SerializeField]
    private float bulletSpeed;

    private void Start()
    {
        Destroy(gameObject, 5f);
    }
    private void Update()
    {
        transform.position += transform.forward * Time.deltaTime * bulletSpeed;
    }

    private void OnTriggerEnter(Collider other)
    {

        if (other.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            GameManager.Instance.playerController.TakeDamage(10);
            Debug.Log(GameManager.Instance.playerController.Health);
        }
    }
}
