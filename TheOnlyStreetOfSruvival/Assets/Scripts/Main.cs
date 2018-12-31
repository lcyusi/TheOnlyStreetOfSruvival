using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Main : MonoBehaviour
{
    private int[] nums = new int[] { 20, 90, 13, 88, 1, 15, 40 };
    char cha = 'a';
    public float moneyMJ;
    public struct Man
    {
        public string name;
        public int age;
        public int money;
    }

    enum S
    {
        one,
        two
    }

    class Person
    {
        public string name;
        public int age;
        public float money;
        public void Talk()
        {
            Debug.Log("我能嗨唱");
        }
    }

    private int MianjinAge()
    {
        int age = 30;
        return age;
    }


    private void MianjinShouru(float x1, float x2)
    {
        moneyMJ = x1 + x2;
    }

    // Use this for initialization
    private void Start()
    {
        Debug.Log(MianjinAge());
        MianjinShouru(500, 1000);
        Debug.Log(moneyMJ);

        Person person = new Person();
        person.name = "面筋哥";
        person.age = 30;
        person.money = 5000000000;
        person.Talk();
        Debug.Log(person.age);
        //i=0;j=0;
        //if (nums[0]>nums[1])=20>90  j++ j=1(6,j=0)

        //i=0 第一趟 
        //j=0 nums.Length - 1 - i = 7 - 1 - 0 = 6 6次
        //j=0 if(nums[0]>nums[1])=20>90;  temp = 20; nums[0] = 20; nums[1] = 90;  nums = {20,90,13,88,1,15,40}
        //j=1 if(nums[1]>nums[2])=90>13;  temp = 90; nums[1] = 13; nums[2] = 90;  nums = {20,13,90,88,1,15,40}
        //j=2 if(num[2]>nums[3])=90>88;   temp = 90; nums[2] = 90; nums[3] = 88;  nums = {20,13,88,90,1,15,40}
        //j=3 if(num[3]>nums[4])=90>1;    temp = 90; nums[3] = 90; nums[4] = 1 ;  nums = {20,13,88,1,90,15,40}
        //j=4 if(num[4]>nums[5])=90>15;   temp = 90; nums[4] = 90; nums[5] = 15;  nums = {20,13,88,1,15,90,40}
        //j=5 if(num[5]>nums[6])=90>40;   temp = 90; nums[5] = 90; nums[6] = 40;  nums = {20,13,88,1,15,40,90}

        //i=1 第二趟 
        //j=0 nums.Length - 1 - i = 7 - 1 - 1 = 5 5次
        //j=0 if(nums[0]>nums[1])=20>13;  temp = 20; nums[0] = 20; nums[1] = 13;  nums = {13,20,88,1,15,40,90}
        //j=1 if(nums[1]>nums[2])=20>88;  temp = 20; nums[1] = 20; nums[2] = 88;  nums = {13,20,88,1,15,40,90}
        //j=2 if(num[2]>nums[3])=88>1;    temp = 88; nums[2] = 88; nums[3] = 1;   nums = {13,20,1,88,15,40,90}
        //j=3 if(num[3]>nums[4])=88>15;   temp = 88; nums[3] = 88; nums[4] = 15;  nums = {13,20,1,15,88,40,90}
        //j=4 if(num[4]>nums[5])=88>40;   temp = 88; nums[4] = 88; nums[5] = 40;  nums = {13,20,1,15,40,88,90}

        //i=2 第三趟 
        //j=0 nums.Length - 1 - i = 7 - 1 - 2 = 4 4次
        //j=0 if(nums[0]>nums[1])=13>20;  temp = 13; nums[0] = 13; nums[1] = 20;  nums = {13,20,1,15,40,88,90}
        //j=1 if(nums[1]>nums[2])=20>1;   temp = 20; nums[1] = 1;  nums[2] = 20;  nums = {13,1,20,15,40,88,90}
        //j=2 if(num[2]>nums[3])=20>15;   temp = 20; nums[2] = 15; nums[3] = 20;  nums = {13,1,15,20,40,88,90}
        //j=3 if(num[3]>nums[4])=20>40;   temp = 20; nums[3] = 20; nums[4] = 40;  nums = {13,1,15,20,40,88,90}

        //i=3 第四趟 
        //j=0 nums.Length - 1 - i = 7 - 1 - 3 = 3 3次
        //j=0 if(nums[0]>nums[1])=13>1;   temp = 13; nums[0] = 1;  nums[1] = 13;  nums = {1,13,15,20,40,88,90}
        //j=1 if(nums[1]>nums[2])=13>15;  temp = 13; nums[1] = 13; nums[2] = 15;  nums = {1,13,15,20,40,88,90}
        //j=2 if(num[2]>nums[3])=20>15;   temp = 20; nums[2] = 15; nums[3] = 20;  nums = {1,13,15,20,40,88,90}

        //i=4 第五趟 
        //j=0 nums.Length - 1 - i = 7 - 1 - 4 = 2 2次
        //j=0 if(nums[0]>nums[1])=13>1;   temp = 13; nums[0] = 1;  nums[1] = 13;  nums = {1,13,15,20,40,88,90}
        //j=1 if(nums[1]>nums[2])=13>15;  temp = 13; nums[1] = 13; nums[2] = 15;  nums = {1,13,15,20,40,88,90}

        //i=5 第六趟 
        //j=0 nums.Length - 1 - i = 7 - 1 - 5 = 1 1次
        //j=0 if(nums[0]>nums[1])=13>1;   temp = 13; nums[0] = 1;  nums[1] = 13;  nums = {1,13,15,20,40,88,90}

        for (int i = 0; i < nums.Length - 1; i++) // 趟数
        {
            for (int j = 0; j < nums.Length - 1 - i; j++)// 次数
            {
                if (nums[j] > nums[j + 1])//nums[0]=2  nums[1]=1
                {
                    int temp = nums[j];//temp = 2
                    nums[j] = nums[j + 1];//nums[0] = 1
                    nums[j + 1] = temp;//nums[1] = temp = 2
                }
            }
        }

        int num = 0;
        num = 10;
        switch (num)
        {
            case 0:
                Debug.Log("0");
                break;
            case 1:
                Debug.Log("10");
                break;
            default:
                break;
        }

        for (int i = 0; i < nums.Length - 1; i++)
        {
            Debug.Log(nums[i]);
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}
