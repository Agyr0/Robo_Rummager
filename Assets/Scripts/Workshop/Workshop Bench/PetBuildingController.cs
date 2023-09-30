using Agyr.Workshop;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PetBuildingController : MonoBehaviour
{
    [HideInInspector]
    public TabButtonHeader myTab;

    [SerializeField]
    private GameObject completedEffect;
    [SerializeField]
    private GameObject combinedMesh;
    [SerializeField]
    private GameObject seperatedMesh;
    [SerializeField]
    private GameObject aiPrefab;
    private float progress;

    [SerializeField]
    private List<RobotPartPairs> myParts = new List<RobotPartPairs>();

    private void OnEnable()
    {
        StartCoroutine(DisplayHologram());
    }
    public IEnumerator DisplayHologram()
    {
        float timeToComplete = 3f;
        while (progress < 1)
        {
            progress += Time.deltaTime / timeToComplete;

            combinedMesh.GetComponent<MeshRenderer>().material.SetFloat("_Progress", progress);
            
            yield return null;
        }
        seperatedMesh.SetActive(true);
        combinedMesh.SetActive(false);
    }
    public void BuildPiece()
    {
        int randomIndex = Random.Range(0, myParts.Count);

        if (myParts[randomIndex].hologramPart.activeInHierarchy)
        {
            myParts[randomIndex].SwapParts();
        }
        else
            Invoke("BuildPiece",0);

        if(SpawnAIPrefab())
        {
            gameObject.SetActive(false);
        }
    }

    private bool SpawnAIPrefab()
    {
        int numActive = 0;
        for (int i = 0; i < myParts.Count; i++)
        {
            if (myParts[i].realPart.activeInHierarchy)
                numActive++;
        }

        if(numActive == myParts.Count)
        {
            GameObject go = Instantiate(aiPrefab, transform.position, transform.rotation);
            for (int i = 0;i < myParts.Count;i++)
            {
                myParts[i].ResetParts();
            }
            return true;
        }

        return false;
    }
}

[System.Serializable]
public class RobotPartPairs
{
    public GameObject hologramPart;
    public GameObject realPart;

    public void SwapParts()
    {
        hologramPart.SetActive(false);
        realPart.SetActive(true);
    }
    public void ResetParts()
    {
        hologramPart.SetActive(true);
        realPart.SetActive(false);
    }
}
