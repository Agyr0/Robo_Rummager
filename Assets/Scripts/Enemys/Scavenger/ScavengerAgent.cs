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

    public float scavengerMaxHealth = 100;
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
        scavengerHealth = scavengerMaxHealth;

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
        Debug.Log("I've been shot!");
        scavengerHealth -= damage;
        if (scavengerHealth <= 0)
        {
            // Item Drops (Disabled drops until I know what Scavengers are supposed to drop)
            //LootBag lootBag = this.gameObject.GetComponent<LootBag>();
            //lootBag.DropResource(this.gameObject.transform.position);

            audioManager.PlayClip(audioSource, audioManager.FindRandomizedClip(AudioType.Scavenger_Death, audioManager.effectAudio));
            gameObject.SetActive(false);
            navMeshAgent.enabled = false;
            scavengerHealth = scavengerMaxHealth;
        }
        else
        {
            // Chase Player
            stateMachine.ChangeState(ScavengerStateId.Detection);
        }
    }
}
