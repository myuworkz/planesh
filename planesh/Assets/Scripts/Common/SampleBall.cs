using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SampleBall : MonoBehaviour
{
    private Camera cam;
    
    void Start()
    {
        cam = GameObject.FindWithTag("MainCamera").GetComponent<Camera>() as Camera;
    }

    void Update()
    {
        Vector2 screenPos = cam.WorldToViewportPoint(this.gameObject.transform.position);
        if (screenPos.y  < 0)
        {
            Destroy(this.gameObject);
        }
    }

}
