using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Book : MonoBehaviour, IInteractable
{
    public void Interact()
    {
        UiManager.instance.OpenBook();
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