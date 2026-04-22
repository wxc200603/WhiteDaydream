using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Frozen : MonoBehaviour,IInteractable
{
    [HideInInspector]
    public bool hasInteracted = false;
    public void Interact()
    {
        if (!hasInteracted)
        {
            hasInteracted = true;
            ProgressManager.Instance.AddProgress(20);
        }
        Debug.Log("这是黑噬灵");
    }
    //发光相关方法
    public GameObject outlineObj;
    public void OnHighlight()
    {
        outlineObj.SetActive(true);
    }

    public void OffHighlight()
    {
        outlineObj.SetActive(false);
    }
}
