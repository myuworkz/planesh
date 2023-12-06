using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterCurrent : StillAccelerator
{
    [SerializeField] private Vector2 iniForce;


    void  OnTriggerStay2D(Collider2D collision)
    {
        Rigidbody2D rb = collision.gameObject.GetComponent<Rigidbody2D>(); 
        rb.AddForce (iniForce);   // 力を加える
    }

    public override void controllBall()
    {

    }

    public override void repeatAnimation()
    {

    }
}
