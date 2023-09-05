using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ScavengerBot : MonoBehaviour
{
    private NavMeshAgent _agent;

    [Tooltip("Range that the enemy AI will patrol around in.")]
    public float range;
    [Tooltip("Center Point that the enemy AI will patrol around.")]
    public Transform centerPoint;
    [Tooltip("Minimum distance that can be between new patrol points.")]
    public float minDistFromLastPoint;
    [Tooltip("Max time the AI can wait at a patrol point before selecting another.")]
    public float maxWaitTime;
    [Tooltip("Min time the AI can wait at a patrol point before selecting another.")]
    public float minWaitTime;


    // Start is called before the first frame update
    void Start()
    {
        _agent = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        if (_agent.remainingDistance <= _agent.stoppingDistance)
        {
            Vector3 point;
            if (RandomPoint(centerPoint.position, range, out point))
            {
                Debug.DrawRay(point, Vector3.up, Color.red, 5.0f); //Display the next point the AI will move to
                _agent.SetDestination(point);
            }
        }

        bool RandomPoint(Vector3 center, float range, out Vector3 result)
        {
            Vector3 randomPoint = center + Random.insideUnitSphere * range; //Generate a random point
            float distFromLastPoint = Vector3.Distance(this.transform.position, randomPoint);

            if (distFromLastPoint < minDistFromLastPoint)
                randomPoint = center + Random.insideUnitSphere * range; //Generate a random point if the first point was within the minimum distance

            NavMeshHit hit;
            if (NavMesh.SamplePosition(randomPoint, out hit, 1.0f, NavMesh.AllAreas))
            {
                result = hit.position;
                return true;
            }

            result = Vector3.zero;
            return false;
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(centerPoint.position, range); //Display the radius the AI will patrol around in
    }
}
