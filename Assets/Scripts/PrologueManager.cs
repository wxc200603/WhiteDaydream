using System.Collections;
using UnityEngine;
using TMPro;

public class PrologueManager : MonoBehaviour
{
    [Header("UI 演员分配")]
    public TextMeshProUGUI subtitleText; 
    public CanvasGroup scene1_Meteor;    
    public CanvasGroup scene2_Table;     
    public CanvasGroup scene3_Trapped;   
    public CanvasGroup scene4_WhiteFlash;

    [Header("摄像机 (用于震屏)")]
    public Transform mainCamera;

    void Start()
    {
        // 游戏一开始，启动演出
        StartCoroutine(PlayPrologue());
    }

    IEnumerator PlayPrologue()
    {
        subtitleText.text = ""; // 清空字幕

        // --- 画面1 (陨石) 自动触发 ---
        yield return StartCoroutine(TypeText("爷爷说过，世界曾经是有颜色的。"));
        yield return new WaitForSeconds(1.5f); 

        yield return StartCoroutine(TypeText("但我出生的那天，天空只剩下了死寂。"));
        yield return new WaitForSeconds(1.5f);

        StartCoroutine(FadeIn(scene1_Meteor, 2f)); // 淡入陨石
        StartCoroutine(ShakeCamera(0.5f, 5f));     // 震屏
        yield return StartCoroutine(TypeText("没有火焰，没有轰鸣。只有无声的黑泥，像墨水一样，缓缓洇透了这片大地的每一寸色彩。"));
        
        // ==========================================
        // 暂停，等待玩家点击左键后，再进入画面2
        // ==========================================
        yield return StartCoroutine(WaitForClick());

        // --- 画面2 (餐桌) 点击后触发 ---
        StartCoroutine(FadeOut(scene1_Meteor, 1.5f));
        StartCoroutine(FadeIn(scene2_Table, 2.5f)); 
        yield return StartCoroutine(TypeText("在这个世界上，‘生存’变成了一场单调的苦役。"));
        yield return new WaitForSeconds(1.0f); // 两句台词间稍微停顿一下
        yield return StartCoroutine(TypeText("人们还活着，但舌尖上的灵魂已经死去了。"));
        
        // ==========================================
        // 暂停，等待玩家点击左键后，再进入画面3
        // ==========================================
        yield return StartCoroutine(WaitForClick());

        // --- 画面3 (被困) 点击后触发 ---
        StartCoroutine(FadeOut(scene2_Table, 1f));
        StartCoroutine(FadeIn(scene3_Trapped, 1.5f));
        yield return StartCoroutine(TypeText("我听见它们在黑暗中窃窃私语。"));

        // ==========================================
        // 暂停，等待玩家点击左键后，触发画面4觉醒
        // ==========================================
        yield return StartCoroutine(WaitForClick());

        // --- 画面4 (觉醒) 点击后瞬间触发 ---
        subtitleText.text = ""; 
        scene4_WhiteFlash.alpha = 1f; // 瞬间将白屏Alpha拉到1
        yield return StartCoroutine(FadeOut(scene4_WhiteFlash, 1.5f)); // 白光缓慢消散

        Debug.Log("序章演出结束！准备进入新手教程/第一关！");
    }

    // --- 下面是导演的专用工具箱 ---

    // [新增工具] 等待玩家点击屏幕
    IEnumerator WaitForClick()
    {
        // 在当前台词后面加上一句提示（带有颜色和略小的字号）
        subtitleText.text += "\n<size=30><color=#aaaaaa>(点击屏幕继续...)</color></size>";
        
        // 稍微等待0.2秒，防止玩家刚才不小心多点了一下直接跳过
        yield return new WaitForSeconds(0.2f);
        
        bool isClicked = false;
        while (!isClicked)
        {
            // 检测鼠标左键 (0代表左键)
            if (Input.GetMouseButtonDown(0)) 
            {
                isClicked = true;
            }
            yield return null; // 每一帧检查一次
        }
    }

    // 打字机效果
    IEnumerator TypeText(string textToType)
    {
        subtitleText.text = "";
        foreach (char letter in textToType.ToCharArray())
        {
            subtitleText.text += letter;
            yield return new WaitForSeconds(0.08f); 
        }
    }

    // 淡入
    IEnumerator FadeIn(CanvasGroup cg, float duration)
    {
        float time = 0;
        while (time < duration)
        {
            cg.alpha = Mathf.Lerp(0, 1, time / duration);
            time += Time.deltaTime;
            yield return null;
        }
        cg.alpha = 1;
    }

    // 淡出
    IEnumerator FadeOut(CanvasGroup cg, float duration)
    {
        float time = 0;
        while (time < duration)
        {
            cg.alpha = Mathf.Lerp(1, 0, time / duration);
            time += Time.deltaTime;
            yield return null;
        }
        cg.alpha = 0;
    }

    // 震屏
    IEnumerator ShakeCamera(float duration, float magnitude)
    {
        Vector3 originalPos = mainCamera.localPosition;
        float elapsed = 0.0f;
        while (elapsed < duration)
        {
            float x = Random.Range(-1f, 1f) * magnitude;
            float y = Random.Range(-1f, 1f) * magnitude;
            mainCamera.localPosition = new Vector3(x, y, originalPos.z);
            elapsed += Time.deltaTime;
            yield return null;
        }
        mainCamera.localPosition = originalPos;
    }
}