using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimator : MonoBehaviour
{
    public Animator m_animator;
    void Start()
    {
        m_animator = GetComponent<Animator>();
    }


    void Update()
    {

        float h = Input.GetAxis("Horizontal"); //获取左右移动的参数

        float v = Input.GetAxis("Vertical"); //获取前后移动的参数

        //按下W键
        if (Input.GetKeyDown(KeyCode.W))
        {
            m_animator.SetFloat("walk", 0.5f);
        }

        //抬起W键
        if (Input.GetKeyUp(KeyCode.W))
        {
            m_animator.SetFloat("walk", 0.1f);
        }

        //按下S键
        if (Input.GetKeyDown(KeyCode.S))
        {
            m_animator.SetFloat("walk", -0.5f);
        }

        //抬起S键
        if (Input.GetKeyUp(KeyCode.S))
        {
            m_animator.SetFloat("walk", -0.1f);
        }

        //print(h * h + v * v);

        //if (h != 0 || v != 0)
        //{
        //    m_animator.SetBool("Run", true);
        //}
        //else
        //{
        //    m_animator.SetBool("Run", false);
        //}

        //if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow))
        //{
        //    m_animator.SetBool("Run", true);
        //}

        //if (Input.GetKeyUp(KeyCode.W) || Input.GetKeyUp(KeyCode.UpArrow))
        //{
        //    m_animator.SetBool("Run", true);
        //}

        //m_animator.SetFloat("RunSpeed", h * h + v * v); //只要玩家点击前后左右的任意一个按键，RunSpeed的参数就不会为零。然后就开始移动

        //m_animator.SetFloat("RunDirection", h, 0.1f, Time.deltaTime); //移动的值就是RunDirection.

    }
}
