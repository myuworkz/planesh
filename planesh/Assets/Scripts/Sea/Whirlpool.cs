using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Whirlpool : StillAccelerator
{
   [SerializeField] private Vector2 iniForce = new Vector2(0.0f, 0.1f);  //加わる力の初期値


    void  OnTriggerStay2D(Collider2D collision)
    {
        Rigidbody2D rb = collision.gameObject.GetComponent<Rigidbody2D>(); 
        rb.AddForce (iniForce, ForceMode2D.Impulse);   // 力を加える
    }

    public override void controllBall()
    {

    }

    public override void repeatAnimation()
    {

    }
}
