using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManCtrl : MonoBehaviour
{
    GameObject man;
    public GameObject cube;

    private void Awake()
    {

    }

    // Use this for initialization
    void Start()
    {
        man = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        //transform.Rotate(Vector3.up, 10);
        //获取键盘按键的方法
        if (Input.GetKey(KeyCode.D))
        {
            //man.transform.Translate(new Vector3(0, 0, 1), Space.World);
            Debug.Log("GetKey");
            cube.GetComponent<Rigidbody>().AddForce(new Vector3(50, 0, 0));
        }
        if (Input.GetKey(KeyCode.S))
        {
            //man.transform.Translate(new Vector3(0, 0, 1), Space.World);
            Debug.Log("GetKey");
            cube.GetComponent<Rigidbody>().AddForce(new Vector3(-50, 0, 0));
        }
        if (Input.GetKeyDown(KeyCode.W))
        {
            Debug.Log("GetKeyDown");
            cube.GetComponent<Rigidbody>().AddExplosionForce(10, new Vector3(10, 0, 0), 10);
        }
        if (Input.GetKeyUp(KeyCode.W))
        {
            Debug.Log("GetKeyUp");
        }

        //虚拟按键，横轴
        if (Input.GetAxis("Horizontal") > 0)
        {
            Debug.Log("Horizontal>0");
        }
        if (Input.GetAxis("Horizontal") < 0)
        {
            Debug.Log("Horizontal<0");
        }

        //虚拟按键，横轴
        if (Input.GetAxis("Vertical") > 0)
        {
            Debug.Log("Vertical>0");
        }
        if (Input.GetAxis("Vertical") < 0)
        {
            Debug.Log("Vertical<0");
        }

        //获取鼠标点击的方法
        if (Input.GetMouseButton(0))
        {
            Debug.Log("Input.GetMouseButton(0):" + "获取鼠标左键");
        }
        if (Input.GetMouseButton(1))
        {
            Debug.Log("Input.GetMouseButton(1):" + "获取鼠标右键");
        }
        if (Input.GetAxis("Mouse ScrollWheel") > 0)
        {
            Debug.Log("朝上");
        }
        if (Input.GetAxis("Mouse ScrollWheel") < 0)
        {
            Debug.Log("朝下");
        }

        //获取鼠标位置
        //Vector3 mouse = Input.mousePosition;
        //Debug.Log(mouse);
    }
}
