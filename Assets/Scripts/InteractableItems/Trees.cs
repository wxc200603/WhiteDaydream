using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trees : MonoBehaviour, IInteractable
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
        Debug.Log("这里的木头干枯得像炭一样，闻不到一点树木的清香。");
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
