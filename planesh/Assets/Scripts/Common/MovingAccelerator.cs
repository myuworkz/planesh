using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class MovingAccelerator : Accelerator
{
    public abstract override void controllBall();
    public abstract override void repeatAnimation();

}
