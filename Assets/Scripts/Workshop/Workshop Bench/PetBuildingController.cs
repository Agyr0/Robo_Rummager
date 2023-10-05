using Agyr.Workshop;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

public class PetBuildingController : MonoBehaviour
{
    [HideInInspector]
    public TabButtonHeader myTab;

    [SerializeField]
    private VisualEffect completedEffect;
    [SerializeField]
    private Animator animator;
    [SerializeField]
    private GameObject combinedMesh;
    [SerializeField]
    private GameObject seperatedMesh;
    [SerializeField]
    private GameObject aiPrefab;
    private float progress;

    private int numActive = 0;
    

    [SerializeField]
    private List<RobotPartPairs> myParts = new List<RobotPartPairs>();

    private void OnEnable()
    {
        StartCoroutine(DisplayHologram());
    }

    public void ResetRobot()
    {
        Destroy(this.gameObject);
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

        if (CheckBuildStage())
        {
            StartCoroutine(BuildCompleteEffect());
            
        }
    }

    private bool CheckBuildStage()
    {
        numActive = 0;
        for (int i = 0; i < myParts.Count; i++)
        {
            if (myParts[i].realPart.activeInHierarchy)
                numActive++;
        }
        if (numActive == myParts.Count)
        {
            return true;
        }
        return false;
    }

    private bool SpawnAIPrefab()
    {
        if(CheckBuildStage())
        {
            GameObject go = null;

            if (aiPrefab != null)
            {
                go = Instantiate(aiPrefab, transform.position, transform.rotation, WorkshopBench.Instance.tabManager.robotParent);

            }
            
            for (int i = 0;i < myParts.Count;i++)
            {
                myParts[i].ResetParts();
            }
            return true;
        }

        return false;
    }

    private IEnumerator BuildCompleteEffect()
    {


        animator.SetTrigger("Play");

        if (completedEffect != null)
            completedEffect.Play();



        yield return new WaitForSeconds(3f);
        EventBus.Publish(EventType.ROBOT_BUILT);

        if(SpawnAIPrefab())
            ResetRobot();

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
