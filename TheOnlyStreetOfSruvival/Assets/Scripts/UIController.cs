using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void EnterGame()
    {
        Application.LoadLevel("Past1");
    }

    /// <summary>
    /// 退出程序
    /// </summary>
    public void ExitGame()
    {
        Application.Quit();
    }
}
