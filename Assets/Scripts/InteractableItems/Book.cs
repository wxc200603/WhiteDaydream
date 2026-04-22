using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Book : MonoBehaviour, IInteractable
{
    [HideInInspector]
    public bool hasInteracted = false;
    public BoxCollider2D []exitTrigger;
    public void Interact()
    {
        if(!hasInteracted)
        {
            hasInteracted = true;
            ProgressManager.Instance.AddProgress(20);
        }
        UiManager.instance.OpenBook();
        StartCoroutine(WaitForClose());
    }

    private IEnumerator WaitForClose()
    {
        yield return null;

        while (true)
        {
            if (Input.GetMouseButtonDown(0))
            {
                //用 UI 射线检测，替换原来的 Physics2D.OverlapPoint
                PointerEventData pointerData = new PointerEventData(EventSystem.current)
                {
                    position = Input.mousePosition
                };

                List<RaycastResult> results = new List<RaycastResult>();
                EventSystem.current.RaycastAll(pointerData, results);

                foreach (var result in results)
                {
                    foreach(var exitArea in exitTrigger)
                    {
                        if (result.gameObject == exitArea.gameObject) // ← exitTrigger 是你的 BoxCollider2D
                        {
                            UiManager.instance.CloseBook();
                            yield break;
                        }
                    }
                }
            }
            yield return null;
        }
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