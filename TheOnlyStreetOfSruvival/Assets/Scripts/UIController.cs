using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    public Image[] showImgs;
    public Image[] hideImgs;
    public Text[] showTexts;
    public float waitTime;
    public float showSpeed;
    public float hideSpeed;
    private bool startShow = false;
    private bool startHide = false;

    // Use this for initialization
    void Start()
    {
        for (int i = 0; i < showImgs.Length; i++)
        {
            showImgs[i].color = new Color(showImgs[i].color.r, showImgs[i].color.g, showImgs[i].color.b, 0);
            showTexts[i].color = new Color(showTexts[i].color.r, showTexts[i].color.g, showTexts[i].color.b, 0);
        }
        for (int i = 0; i < hideImgs.Length; i++)
        {
            hideImgs[i].color = new Color(hideImgs[i].color.r, hideImgs[i].color.g, hideImgs[i].color.b, 1);
        }

        StartCoroutine(WaitHideImgs(waitTime));
    }

    // Update is called once per frame
    void Update()
    {
        if (startShow)
        {
            ShowImages(showSpeed, showImgs);
            ShowTexts(showSpeed, showTexts);
        }
        if (startHide)
        {
            HideImages(hideSpeed, hideImgs);
        }
    }

    /// <summary>
    /// 载入场景
    /// </summary>
    public void SelectLevel(string levelName)
    {
        SceneManager.LoadScene(levelName);
    }

    /// <summary>
    /// 退出程序
    /// </summary>
    public void ExitGame()
    {
        Application.Quit();
    }

    IEnumerator WaitHideImgs(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        startHide = true;
        startShow = true;
    }

    /// <summary>
    /// 显示图片，图片透明度渐变为1
    /// </summary>
    /// <param name="transitionSpeed">渐变速度</param>
    /// <param name="imgs">图片列表</param>
    void ShowImages(float transitionSpeed, Image[] imgs)
    {
        for (int i = 0; i < imgs.Length; i++)
        {
            float a = imgs[i].color.a;
            a += transitionSpeed * Time.deltaTime;
            imgs[i].color = new Color(imgs[i].color.r, imgs[i].color.g, imgs[i].color.b, a);
            if (imgs[i].color.a >= 0.99f)
            {
                imgs[i].color = new Color(imgs[i].color.r, imgs[i].color.g, imgs[i].color.b, 1);
            }
        }
    }

    /// <summary>
    /// 隐藏图片，图片透明度渐变为0
    /// </summary>
    /// <param name="transitionSpeed">渐变速度</param>
    /// <param name="imgs">图片列表</param>
    void HideImages(float transitionSpeed, Image[] imgs)
    {
        for (int i = 0; i < imgs.Length; i++)
        {
            float a = imgs[i].color.a;
            a -= transitionSpeed * Time.deltaTime;
            imgs[i].color = new Color(imgs[i].color.r, imgs[i].color.g, imgs[i].color.b, a);
            if (imgs[i].color.a <= 0.01f)
            {
                imgs[i].color = new Color(imgs[i].color.r, imgs[i].color.g, imgs[i].color.b, 0);
            }
        }
    }

    /// <summary>
    /// 显示文字，文字透明度渐变为1
    /// </summary>
    /// <param name="transitionSpeed">渐变速度</param>
    /// <param name="texts">文字列表</param>
    void ShowTexts(float transitionSpeed, Text[] texts)
    {
        for (int i = 0; i < texts.Length; i++)
        {
            float a = texts[i].color.a;
            a += transitionSpeed * Time.deltaTime;
            texts[i].color = new Color(texts[i].color.r, texts[i].color.g, texts[i].color.b, a);
            if (texts[i].color.a >= 0.99f)
            {
                texts[i].color = new Color(texts[i].color.r, texts[i].color.g, texts[i].color.b, 1);
            }
        }
    }
}
