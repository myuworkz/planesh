using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    private PhysicsMaterial2D ballMA;   //ボールの摩擦を変化させる用
    private Rigidbody2D ballRB;
    private SpriteRenderer sr;

    [SerializeField] public float resetTime = 8.0f;
    [SerializeField] private float ballFrictIni = 0.1f; //摩擦初期設定
    [SerializeField] private float ballFrictCha = 0.6f; //摩擦変化(1.0を基準として、数値を大きくすると抵抗も大きくなる)
    [SerializeField] private float ballDragChange = 10.0f;  //抵抗変化(1.0を基準として、数値を大きくすると抵抗も大きくなる)
    [SerializeField] private float ballAlpha = 0.6f;        //不透明度変化(1.0を基準として、数値を小さくすると不透明度が下がる)


    //audio
    AudioSource audioData;
    [SerializeField] AudioClip presentSound;


    void Awake()
    {
        //audio
        audioData = GetComponent<AudioSource>();
    }


    void Update()
    {
        Vector3 thisPos = this.gameObject.transform.position;
        Vector3 screen_LeftBottom = Camera.main.ScreenToWorldPoint(Vector3.zero);
        Vector3 screen_RightTop = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, 0));

        //make ball in the course
        if (thisPos.x < screen_LeftBottom.x)
        {
            this.gameObject.transform.position = new Vector3(screen_LeftBottom.x, thisPos.y, thisPos.z);
        }

        if (thisPos.x > screen_RightTop.x)
        {
            this.gameObject.transform.position = new Vector3(screen_RightTop.x, thisPos.y, thisPos.z);
        }

        if (thisPos.y < screen_LeftBottom.y)
        {
            this.gameObject.transform.position = new Vector3(thisPos.x, screen_LeftBottom.y, thisPos.z);
        }

        if (thisPos.y > screen_RightTop.y)
        {
            this.gameObject.transform.position = new Vector3(thisPos.x, screen_RightTop.y, thisPos.z);
        }





    }

    public void hitByPenguin()
    {

        ballRB = this.gameObject.GetComponent<Rigidbody2D>();
        ballRB.drag = ballDragChange;                               //抵抗を変える
        sr = this.gameObject.GetComponent<SpriteRenderer>();
        sr.color = new Color(1, 1, 1, ballAlpha);                   //ボールの不透明度を変える
        Invoke("resetBallPenguin", resetTime);
    }

    public void hitByPresent()
    {
        //audio
        audioData.Play();

        ballMA = this.gameObject.GetComponent<Rigidbody2D>().sharedMaterial;
        ballMA.friction = ballFrictCha;                                 //摩擦を変える
        sr = this.gameObject.GetComponent<SpriteRenderer>();
        sr.color = new Color(1, 1, 1, ballAlpha);                   //ボールの不透明度を変える
        // presentTouch = true;                                        //Presentに触れた判定
        Invoke("resetBallPresent", resetTime);                                 //resetTime秒後に抵抗とボールの不透明度を元に戻す

    }

    //抵抗と不透明度を元に戻す関数
    private void resetBallPenguin()
    {
        ballRB.drag = 1.0f;
        sr.color = new Color(1, 1, 1, 1);
    }

    //抵抗と不透明度を元に戻す関数
    private void resetBallPresent()
    {
        ballMA.friction = ballFrictIni;
        sr.color = new Color(1, 1, 1, 1);
    }
}
