using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IcicleInChild : MonoBehaviour
{
    Icicle icicle;

    private void Start()
    {
        icicle = this.transform.GetComponentInParent<Icicle>();
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
