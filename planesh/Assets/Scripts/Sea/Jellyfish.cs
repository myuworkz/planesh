using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jellyfish : MovingAccelerator
{
    [SerializeField] private float upDown = 6.0f;   //Jellyfishが上下する大きさ
    private Vector2 nowPosi;

    //audio
    AudioSource audioData;
    [SerializeField] AudioClip sound;

    void Start()
    {
        nowPosi = this.transform.position;    //現在位置

        //audio
        audioData = GetComponent<AudioSource>();
    }

    void Update()
    {
        transform.position = new Vector2(nowPosi.x, nowPosi.y + Mathf.PingPong(3*Time.time, upDown));   //現在位置からy軸方向に移動させる
    }

    public override void controllBall()
    {
        Debug.Log("jellyfish");
    }

     public override void repeatAnimation()
    {
        Debug.Log("jellyfish");
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Ball")
        { 
            audioData.Play();
        }
    }

}
