using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Screw : StillAccelerator
{
    [SerializeField] private Vector2 iniForce = new Vector2(0.001f, 0.0f);

    public override void controllBall()
    {

    }

    public override void repeatAnimation()
    {

    }

    void  OnTriggerStay2D(Collider2D collision)
    {
        Rigidbody2D rb = collision.gameObject.GetComponent<Rigidbody2D>();
        rb.AddForce(iniForce);  // 力を加える
    }

}
