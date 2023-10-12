using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class ScavengerSensor : MonoBehaviour
{
    public float detectionDistance;
    public float shootingDistance;
    public float detectionAngle;
    public float shootingAngle;
    public float height;
    public float scanFrequency;
    public Color detectionMeshColor = Color.blue;
    public Color shootingMeshColor = Color.red;
    public LayerMask playerLayer;
    public LayerMask occlusionLayer;
    public List<GameObject> playersDetected = new List<GameObject>();
    public List<GameObject> playersShooting = new List<GameObject>();

    Collider[] colliders = new Collider[50];
    Mesh detectionMesh;
    Mesh shootingMesh;
    int detectionCount;
    int shootingCount;
    float scanInterval;
    float scanTimer;

    private void Start()
    {
        scanInterval = 1.0f / scanFrequency;
    }

    private void Update()
    {
        scanTimer -= Time.deltaTime;
        if(scanTimer < 0)
        {
            scanTimer += scanInterval;
            Scan();
        }
    }
    private Mesh CreateDetectionWedgeMesh()
    {
        Mesh mesh = new Mesh();

        int segments = 10;
        int numTriangles = (segments * 4) + 2 + 2;
        int numVertices = numTriangles * 3;

        Vector3[] vertices = new Vector3[numVertices];
        int[] triangles = new int[numVertices];

        Vector3 bottomCenter = Vector3.zero;
        Vector3 bottomLeft = Quaternion.Euler(0, -detectionAngle, 0) * Vector3.forward * detectionDistance;
        Vector3 bottomRight = Quaternion.Euler(0, detectionAngle, 0) * Vector3.forward * detectionDistance;

        Vector3 topCenter = bottomCenter + Vector3.up * height;
        Vector3 topRight = bottomRight + Vector3.up * height;
        Vector3 topLeft = bottomLeft + Vector3.up * height;

        int vert = 0;

        // left side
        vertices[vert++] = bottomCenter;
        vertices[vert++] = bottomLeft;
        vertices[vert++] = topLeft;

        vertices[vert++] = topLeft;
        vertices[vert++] = topCenter;
        vertices[vert++] = bottomCenter;

        // right side
        vertices[vert++] = bottomCenter;
        vertices[vert++] = topCenter;
        vertices[vert++] = topRight;

        vertices[vert++] = topRight;
        vertices[vert++] = bottomRight;
        vertices[vert++] = bottomCenter;

        float currentAngle = -detectionAngle;
        float deltaAngle = (detectionAngle * 2) / segments;
        for (int i = 0; i < segments; ++i)
        {
            bottomLeft = Quaternion.Euler(0, currentAngle, 0) * Vector3.forward * detectionDistance;
            bottomRight = Quaternion.Euler(0, currentAngle + deltaAngle, 0) * Vector3.forward * detectionDistance;

            topLeft = bottomLeft + Vector3.up * height;
            topRight = bottomRight + Vector3.up * height;

            // far side
            vertices[vert++] = bottomLeft;
            vertices[vert++] = bottomRight;
            vertices[vert++] = topRight;

            vertices[vert++] = topRight;
            vertices[vert++] = topLeft;
            vertices[vert++] = bottomLeft;

            // top
            vertices[vert++] = topCenter;
            vertices[vert++] = topLeft;
            vertices[vert++] = topRight;

            // bottom
            vertices[vert++] = bottomCenter;
            vertices[vert++] = bottomRight;
            vertices[vert++] = bottomLeft;

            currentAngle += deltaAngle;
        }

        for (int i = 0; i < numVertices; ++i)
        {
            triangles[i] = i;
        }

        mesh.vertices = vertices;
        mesh.triangles = triangles;
        mesh.RecalculateNormals();

        return mesh;
    }

    private Mesh CreateShootingWedgeMesh()
    {
        Mesh mesh = new Mesh();

        int segments = 10;
        int numTriangles = (segments * 4) + 2 + 2;
        int numVertices = numTriangles * 3;

        Vector3[] vertices = new Vector3[numVertices];
        int[] triangles = new int[numVertices];

        Vector3 bottomCenter = Vector3.zero;
        Vector3 bottomLeft = Quaternion.Euler(0, -shootingAngle, 0) * Vector3.forward * shootingDistance;
        Vector3 bottomRight = Quaternion.Euler(0, shootingAngle, 0) * Vector3.forward * shootingDistance;

        Vector3 topCenter = bottomCenter + Vector3.up * height;
        Vector3 topRight = bottomRight + Vector3.up * height;
        Vector3 topLeft = bottomLeft + Vector3.up * height;

        int vert = 0;

        // left side
        vertices[vert++] = bottomCenter;
        vertices[vert++] = bottomLeft;
        vertices[vert++] = topLeft;

        vertices[vert++] = topLeft;
        vertices[vert++] = topCenter;
        vertices[vert++] = bottomCenter;

        // right side
        vertices[vert++] = bottomCenter;
        vertices[vert++] = topCenter;
        vertices[vert++] = topRight;

        vertices[vert++] = topRight;
        vertices[vert++] = bottomRight;
        vertices[vert++] = bottomCenter;

        float currentAngle = -shootingAngle;
        float deltaAngle = (shootingAngle * 2) / segments;
        for (int i = 0; i < segments; ++i)
        {
            bottomLeft = Quaternion.Euler(0, currentAngle, 0) * Vector3.forward * shootingDistance;
            bottomRight = Quaternion.Euler(0, currentAngle + deltaAngle, 0) * Vector3.forward * shootingDistance;

            topLeft = bottomLeft + Vector3.up * height;
            topRight = bottomRight + Vector3.up * height;

            // far side
            vertices[vert++] = bottomLeft;
            vertices[vert++] = bottomRight;
            vertices[vert++] = topRight;

            vertices[vert++] = topRight;
            vertices[vert++] = topLeft;
            vertices[vert++] = bottomLeft;

            // top
            vertices[vert++] = topCenter;
            vertices[vert++] = topLeft;
            vertices[vert++] = topRight;

            // bottom
            vertices[vert++] = bottomCenter;
            vertices[vert++] = bottomRight;
            vertices[vert++] = bottomLeft;

            currentAngle += deltaAngle;
        }

        for (int i = 0; i < numVertices; ++i)
        {
            triangles[i] = i;
        }

        mesh.vertices = vertices;
        mesh.triangles = triangles;
        mesh.RecalculateNormals();

        return mesh;
    }

    private void Scan()
    {
        detectionCount = Physics.OverlapSphereNonAlloc(transform.position, detectionDistance, colliders, playerLayer, QueryTriggerInteraction.Collide);
        shootingCount = Physics.OverlapSphereNonAlloc(transform.position, shootingDistance, colliders, playerLayer, QueryTriggerInteraction.Collide);

        playersDetected.Clear();
        playersShooting.Clear();
        for(int i = 0; i < detectionCount; i++)
        {
            GameObject obj = colliders[i].gameObject;
            if(IsInDetectionSight(obj))
            {
                playersDetected.Add(obj);
            }
        }

        for (int i = 0; i < shootingCount; i++)
        {
            GameObject obj = colliders[i].gameObject;
            if (IsInShootingSight(obj))
            {
                playersShooting.Add(obj);
            }
        }
    }

    public bool IsInDetectionSight(GameObject obj)
    {
        Vector3 origin = transform.position;
        Vector3 dest = obj.transform.position;
        Vector3 direction = dest - origin;

        // Commenting out the vertical axis check because it was causing problems due having to offset
        // the navmesh agents height to fit animations. Will fix later when level design requires
        // They are ranged enemies, do they even need a limit on how high/low they can look??

        //if (direction.y < 0 || direction.y > height)
        //{
            //Debug.Log("fail 1");
            //return false;
        //}

        direction.y = 0;
        float deltaAngle = Vector3.Angle(direction, transform.forward);
        if (deltaAngle > detectionAngle)
        {
            return false;
        }

        origin.y += height / 2;
        dest.y = origin.y;
        if (Physics.Linecast(origin, dest, occlusionLayer))
        {
            return false;
        }

        return true;
    }

    public bool IsInShootingSight(GameObject obj)
    {
        Vector3 origin = transform.position;
        Vector3 dest = obj.transform.position;
        Vector3 direction = dest - origin;

        // Commenting out the vertical axis check because it was causing problems due having to offset
        // the navmesh agents height to fit animations. Will fix later when level design requires
        // They are ranged enemies, do they even need a limit on how high/low they can look??

        //if (direction.y < 0 || direction.y > height)
        //{
        //Debug.Log("fail 1");
        //return false;
        //}

        direction.y = 0;
        float deltaAngle = Vector3.Angle(direction, transform.forward);
        if (deltaAngle > shootingAngle)
        {
            return false;
        }

        origin.y += height / 2;
        dest.y = origin.y;
        if (Physics.Linecast(origin, dest, occlusionLayer))
        {
            return false;
        }

        return true;
    }

    private void OnValidate()
    {
        detectionMesh = CreateDetectionWedgeMesh();
        shootingMesh = CreateShootingWedgeMesh();
        scanInterval = 1.0f / scanFrequency;
    }

    private void OnDrawGizmos()
    {
        if (detectionMesh)
        {
            Gizmos.color = detectionMeshColor;
            Gizmos.DrawMesh(detectionMesh, transform.position, transform.rotation);
        }

        if (shootingMesh)
        {
            Gizmos.color = shootingMeshColor;
            Gizmos.DrawMesh(shootingMesh, transform.position, transform.rotation);
        }

        // Highlights player green if in seen range
        //Gizmos.color = Color.green;
        //for(int i = 0; i < count; ++i)
        //{
        //    Gizmos.color = Color.green;
        //    Gizmos.DrawSphere(colliders[i].transform.position, 0.2f);
        //}
    }
}
