using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ProgressManager : MonoBehaviour
{
    public static ProgressManager Instance { get; private set; }

    [Header("进度设置")]
    [SerializeField] private int maxProgress = 100;
    private int currentProgress = 0;

    // 进度变化时触发，传递当前值和最大值
    public UnityEvent<int, int> OnProgressChanged = new UnityEvent<int, int>();
    public UnityEvent OnProgressComplete = new UnityEvent();

    void Awake()
    {
        if (Instance != null && Instance != this) { Destroy(gameObject); return; }//确保全局只有一个实例
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    //增加进度值
    public void AddProgress(int amount)
    {
        currentProgress = Mathf.Clamp(currentProgress + amount, 0, maxProgress);
        OnProgressChanged.Invoke(currentProgress, maxProgress);

        if (currentProgress >= maxProgress)
            OnProgressComplete.Invoke();
        Debug.Log(currentProgress);
    }

    public int GetCurrentProgress() => currentProgress;
    public float GetProgressRatio() => (float)currentProgress / maxProgress;
}
