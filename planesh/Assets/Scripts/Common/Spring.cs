using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spring : MonoBehaviour
{
    private Animator springAnimator; //アニメーター

    // Start is called before the first frame update
    void Start()
    {
        //アニメーター
        springAnimator = this.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void springAnimationTrigger()
    {
        springAnimator.SetTrigger("springJumpTrigger");
    }
}
