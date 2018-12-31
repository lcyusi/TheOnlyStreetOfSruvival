using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeController : MonoBehaviour
{

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    /// <summary>
    /// 碰撞开始的时候
    /// </summary>
    /// <param name="other"></param>
    void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.name == ("Cube (1)"))
        {
            Debug.Log("碰撞开始的时候");
        }
    }

    /// <summary>
    /// 碰撞持续的时候
    /// </summary>
    /// <param name="other"></param>
    void OnCollisionStay(Collision other)
    {
        if (other.gameObject.name == ("Cube (1)"))
        {
            Debug.Log("碰撞持续的时候");
        }
    }

    /// <summary>
    /// 碰撞结束的时候
    /// </summary>
    /// <param name="other"></param>
    void OnCollisionExit(Collision other)
    {
        if (other.gameObject.name == ("Cube (1)"))
        {
            Debug.Log("碰撞结束的时候");
        }
    }
}
