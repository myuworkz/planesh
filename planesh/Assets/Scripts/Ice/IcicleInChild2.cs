using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IcicleInChild2 : MonoBehaviour
{
    Icicle2 icicle;

    private void Start()
    {
        icicle = this.transform.GetComponentInParent<Icicle2>();
    }

    private void fallFlagIcicleInChild()
    {
        icicle.fallFlagIcicle();
    }

    private void destroyIcicleInChild()
    {
        icicle.destroyIcicle();
    }
}
