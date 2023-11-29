using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScavengerBullets : MonoBehaviour
{
    [SerializeField]
    private float bulletSpeed;

    ScavengerAgent agent;

    private void Start()
    {
        agent = FindWhoShotMe().GetComponent<ScavengerAgent>();
        StartCoroutine(DespawnBullet());
    }

    private void Update()
    {
        transform.position += transform.forward * Time.deltaTime * bulletSpeed;
    }

    public GameObject FindWhoShotMe()
    {
        GameObject[] gos;
        gos = GameObject.FindGameObjectsWithTag("Scavenger");
        GameObject closest = null;
        float distance = Mathf.Infinity;
        Vector3 position = transform.position;
        foreach (GameObject go in gos)
        {
            Vector3 diff = go.transform.position - position;
            float curDistance = diff.sqrMagnitude;
            if (curDistance < distance)
            {
                closest = go;
                distance = curDistance;
            }
        }
        return closest;
    }

    private void OnTriggerEnter(Collider other)
    {
        // Damage Handling
        if (other.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            GameManager.Instance.playerController.TakeDamage(agent.config.damage);
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
        yield return new WaitForSeconds(1);
        this.gameObject.SetActive(false);
    }
}
