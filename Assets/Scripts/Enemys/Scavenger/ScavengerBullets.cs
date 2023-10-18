using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScavengerBullets : MonoBehaviour
{
    [SerializeField]
    private float bulletSpeed;

    private void Start()
    {
        StartCoroutine(DespawnBullet());
    }

    private void Update()
    {
        transform.position += transform.forward * Time.deltaTime * bulletSpeed;
    }

    private void OnTriggerEnter(Collider other)
    {
        // Damage Handling
        if (other.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            GameManager.Instance.playerController.TakeDamage(10);
            Debug.Log(GameManager.Instance.playerController.Health);
            //this.gameObject.SetActive(false);
        }

        // Destroy Bullet if it hits a wall
        if (other.gameObject.layer == LayerMask.NameToLayer("Geometry"))
        {
            Debug.Log("Hit:" +  other.gameObject);
            this.gameObject.SetActive(false);
        }
    }

    IEnumerator DespawnBullet()
    {
        yield return new WaitForSeconds(5);
        this.gameObject.SetActive(false);
    }
}
