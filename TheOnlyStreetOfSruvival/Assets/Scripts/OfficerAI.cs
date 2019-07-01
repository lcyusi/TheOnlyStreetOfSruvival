using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OfficerAI : MonoBehaviour
{
    public Animator officerAni;
    public float waitTime = 0f;
    public float distance = 1f;
    public float dir = 0.6f;

    float calTime;

    // Use this for initialization
    void Start()
    {
        officerAni = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        calTime += Time.deltaTime;
        if (calTime >= waitTime)
        {
            Debug.Log("Wlak");
            officerAni.SetFloat("Forwards", dir);
        }
    }
}
