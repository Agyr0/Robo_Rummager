using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.AI;

public class ScavengerAgent : MonoBehaviour, IDamageable
{
    public AudioManager audioManager;
    public AudioSource audioSource;
    public Animator animator;
    public NavMeshAgent navMeshAgent;
    public ScavengerSensor scavengerSensor;
    public ScavengerWeaponIK scavengerWeaponIK;
    public ScavengerStateMachine stateMachine;
    public ScavengerStateId initialState;
    public ScavengerConfig config;

    public float scavengerHealth;

    void Start()
    {
        audioManager = AudioManager.Instance;
        audioSource = GetComponent<AudioSource>();
        animator = GetComponent<Animator>();
        navMeshAgent = GetComponent<NavMeshAgent>();
        scavengerSensor = GetComponent<ScavengerSensor>();
        scavengerWeaponIK = GetComponent<ScavengerWeaponIK>();

        // State Machine Stuff
        stateMachine = new ScavengerStateMachine(this);
        stateMachine.RegisterState(new ScavengerPatrolState());
        stateMachine.RegisterState(new ScavengerDetectionState());
        stateMachine.RegisterState(new ScavengerShootingState());
        stateMachine.RegisterState(new ScavengerRepositionState());
        config = ScavengerConfig.Instantiate(config);

        // Stuff to do when enemy is spawned
        stateMachine.ChangeState(initialState);
        scavengerHealth = config.maxHealth;

        // Set Spawn Location and patrol points
        GameObject spawnLocation = FindClosestSpawnLocation();

        foreach (Transform child in spawnLocation.transform)
        {
            config.scavengerPatrolPoints.Add(child);
        }
    }

    private void Update()
    {
        animator.SetFloat("Speed", navMeshAgent.velocity.magnitude);
        stateMachine.Update();
    }

    public GameObject FindClosestSpawnLocation()
    {
        GameObject[] gos;
        gos = GameObject.FindGameObjectsWithTag("Scavenger Spawn Location");
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

    public void TakeDamage(float damage)
    {
        scavengerHealth -= damage;
        if (scavengerHealth <= 0)
        {
            GetComponent<ScavengerFireGun>().enabled = false;
            navMeshAgent.velocity = Vector3.zero;
            navMeshAgent.isStopped = true;
            animator.SetBool("Dead", true);
            // Play Death Audio
            audioManager.PlayClip(audioSource, audioManager.FindRandomizedClip(AudioType.Scavenger_Death, audioManager.effectAudio));
            StartCoroutine(DespawnScavenger());
        }
        else
        {
            // Go after player
            stateMachine.ChangeState(ScavengerStateId.Detection);
        }
    }

    IEnumerator DespawnScavenger()
    {
        yield return new WaitForSeconds(3.069f);
        // Item Drops
        LootBag lootBag = this.gameObject.GetComponent<LootBag>();
        lootBag.DropResource(this.gameObject.transform.position);

        // Respawn and Object Pool stuff
        navMeshAgent.isStopped = false;
        gameObject.SetActive(false);
        navMeshAgent.enabled = false;
        scavengerHealth = config.maxHealth;
        GetComponent<ScavengerFireGun>().enabled = true;
        animator.SetBool("Dead", false);
    }
}
