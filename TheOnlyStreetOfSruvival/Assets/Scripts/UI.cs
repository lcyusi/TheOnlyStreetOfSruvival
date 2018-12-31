using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI : MonoBehaviour
{
    //公共访问修饰符public
    public string name_Main = "一巷生机";
    //私有访问修饰符private
    private string name_Pri = "第一关";
    public int @i;
    public Text text;
    private int age = 23;
    private string age_1 = "10";
    private int age_2;
    private bool isTrue = false;

    public int[] intArray = new int[10];
    private float[] floatArray = new float[10];
    public int[] int2Array = new int[10] { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9 };

    //初始的时候执行一次
    // Use this for initialization
    void Start()
    {
        for (int i = 0; i < intArray.Length; i++)
        {
            intArray[i] = i;
            Debug.Log("intArray" + i + ":" + intArray[i]);
        }
        //！：false
        if (!text)
        {
            text = GameObject.Find("MainText").GetComponent<Text>();
        }
        text.text = name_Main;
        age = 24;
        age++;
        age_2 = int.Parse(age_1);
        if (age == age_2)
        {
            //Debug.Log("age>age_2");
        }
        //逻辑与
        if (age == 25 && age_2 == 11)
        {
            Debug.Log("age_2=" + age_2);
        }
        //逻辑或
        if (age == 24 || age_2 == 10)
        {
            Debug.Log("age_2=" + age_2);
        }
        //逻辑非
        Debug.Log(!isTrue);
        Debug.Log(age);
        if (!text.enabled)
        {
            Debug.Log(!isTrue);
        }

        //age = 20;
        //if (age == 20)
        //{
        //    Debug.Log("20");
        //}
        //else if (age == 21)
        //{
        //    Debug.Log("21");
        //}
        //else
        //{
        //    Debug.Log("25");
        //}

        //switch (age)
        //{
        //    case 20:
        //        Debug.Log("20");
        //        break;
        //    case 21:
        //        Debug.Log("21");
        //        break;
        //    case 25:
        //        Debug.Log("25");
        //        break;
        //    default:
        //        Debug.Log("30");
        //        break;
        //}


        //int c = age > age_2 ? age : age_2;

        //if (age > age_2)//age>age_2?
        //{
        //    c = age;//age
        //}
        //else//:
        //{
        //    c = age_2;//age_2
        //}
        //Debug.Log(c);
        //while (age == 25)
        //{
        //    age_2++;
        //    Debug.Log(age_2);
        //}
        //for (int i = 0; i < 10; i++)
        //{
        //    Debug.Log(i);
        //}
    }

    //没一帧执行一次
    // Update is called once per frame
    void Update()
    {
        age++;
        //Debug.Log(age);
    }
}
