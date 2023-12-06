using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hand : MonoBehaviour
{
    private bool isActive;
    private double lastActivated;
    double timeGap;

    void Awake()
    {
        isActive = false;
        timeGap = 1;
        
    }

    // Update is called once per frame
    void Update()
    {
        timeGap = Time.realtimeSinceStartup - lastActivated;
        if(timeGap > 0.2f)
        {
            isActive = false;
        }
    }

    //void OnMouseOver()
    //{
    //    isActive = true;
    //    Debug.Log("mouse Entered isActive: " + isActive);
    //}

    //void OnMouseExit()
    //{
    //    Debug.Log("mouse Exit isActive: " + isActive);
    //    isActive = false;
    //}

    void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("ontrigger triggered");
        isActive = true;
        lastActivated = Time.realtimeSinceStartup;
    }

    public bool getIsActive()
    {
        return isActive;
    }

    public void hideMyself()
    {
        //isActive = false;
        this.gameObject.SetActive(false);
    }

    public void showMyself()
    {
        //isActive = true;
        this.gameObject.SetActive(true);
    }
}