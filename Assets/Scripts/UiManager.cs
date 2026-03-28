using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UiManager : MonoBehaviour
{
    public static UiManager instance;
    void Awake()
    {
        instance = this;
    }

    public GameObject Panel_book;
    public void OpenBook()
    {
        Panel_book.SetActive(true);
        Time.timeScale = 0f;
    }

    public void CloseBook()
    {
        Panel_book.SetActive(false);
        Time.timeScale = 1f;
    }
}
