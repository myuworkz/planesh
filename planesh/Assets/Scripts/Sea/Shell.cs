using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shell : StillAccelerator
{
    private float timeCount;     //時間計測用
    private bool shellTouch;     //貝に触れたかどうかの判定用
    private bool attack;            //攻撃判定用
    private Rigidbody2D rb;     //ボールの動きを変化させる用
    private SpriteRenderer sr;  //ボールの不透明度を変化させる用
    private Animator shellAnimator;     //アニメーション用の変数
    [SerializeField] private float ballDrag = 10.0f;    //抵抗変化(1.0を基準として、数値を大きくすると抵抗も大きくなる)
    [SerializeField] private float ballAlpha = 0.2f;    //不透明度変化(1.0を基準として、数値を小さくすると不透明度が下がる)
    [SerializeField] private float resetTime = 8.0f;   //抵抗と不透明度の変化をリセットするまでの時間

    public override void controllBall()
    {
        Debug.Log("shell");
    }

    public override void repeatAnimation()
    {
        Debug.Log("shell");
    }

    // アニメーションのための関数
    private void shellAnimationTrigger()
    {
        shellAnimator.SetTrigger("ShellShutTrigger");
    }

    //初期設定
    void Start()
    {
        timeCount = 0;  
        shellTouch = false;
        attack = true;
        shellAnimator = GetComponent<Animator>();
    }

    void Update()
    {
        //貝に触れたとき、攻撃判定を消して時間を計測
        if(shellTouch == true)
        {
            attack = false;
            timeCount += Time.deltaTime;
        }
        //貝に触れてから3.2秒後に攻撃判定を復活させる
        if (timeCount > 3.2f)
        {
            shellTouch = false;
            attack = true;
            timeCount = 0;
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        //攻撃判定が存在する時だけ処理
        if(attack == true)
        {
            rb = collision.gameObject.GetComponent<Rigidbody2D>();
            rb.drag = ballDrag;                                      //抵抗を変える
            sr = collision.gameObject.GetComponent<SpriteRenderer>();
            sr.color = new Color(1, 1, 1, ballAlpha);     //ボールの不透明度を変える
            shellTouch = true;                                      //Shellに触れた安定
            shellAnimationTrigger();                            //Shellのアニメーション起動
            Invoke("reset", resetTime);                       //8秒後に抵抗とボールの不透明度を元に戻す
        }
    }
    
    //抵抗と不透明度を元に戻す関数
    void reset()
    {
        rb.drag = 1.0f;
        sr.color = new Color(1, 1, 1, 1);
    }
}
