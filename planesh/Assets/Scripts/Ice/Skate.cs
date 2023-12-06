using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skate : MovingAccelerator
{
    private float timeCount;        //時間計測用
    private bool presentTouch;      //Presentに触れたかどうかの判定用
    private bool attack;            //攻撃判定用
    // private PhysicsMaterial2D ma;   //ボールの摩擦を変化させる用
    // private SpriteRenderer sr;      //ボールの不透明度を変化させる用
    private Rigidbody2D presentRB;  //PresentのRigidbody2Dを取得
    private float presentSpeed;     //Presentの速度を取得
    private GameObject ball;
    [SerializeField] Vector2 presentForce　= new Vector2(-2.0f, 0.0f);  //Penguinに与える加速度
    [SerializeField] float limitSpeed = 100.0f;  //Penguinの速度を制限する
    // [SerializeField] private float ballFrictIni = 0.1f; //摩擦初期設定
    // [SerializeField] private float ballFrictCha = 0.4f; //摩擦変化(1.0を基準として、数値を大きくすると抵抗も大きくなる)
    // [SerializeField] private float ballAlpha = 0.2f;    //不透明度変化(1.0を基準として、数値を小さくすると不透明度が下がる)
    // [SerializeField] private float resetTime = 8.0f;    //抵抗と不透明度の変化をリセットするまでの時間
    [SerializeField] float destroyTime;//生成からdestroyまでの時間間隔
    private double spawnedTime;//スポーンされた時間

    //初期設定
    void Start()
    {
        timeCount = 0;
        presentTouch = false;
        attack = true;
    }

    void Update()
    {
        presentRB = this.GetComponent<Rigidbody2D>();    
        presentSpeed = presentRB.velocity.magnitude;

        //Presentに触れたとき、攻撃判定を消して時間を計測
        if (presentTouch == true)
        {
            attack = false;
            timeCount += Time.deltaTime;
        }
        //Presentに触れてからresetTime秒後に攻撃判定を復活させる
        if (timeCount > 8.0f)
        {
            presentTouch = false;
            attack = true;
            timeCount = 0;
        }
        //Presentが画面外に出たら消す
        if (this.transform.position.y <= -20)
        {
            Destroy(this.gameObject);
        }
        //加速度に制限をつける
        if(presentSpeed <= limitSpeed)
        {
            presentRB.AddForce(Vector2.one * presentForce); //Presentに加速度を与え続ける
        }

        //一定時間たったら消す
        if (Time.time - spawnedTime > destroyTime)
        {
            Destroy(this.gameObject);
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        ball = GameObject.Find("Ball");
        if(collision.gameObject == ball){
            //攻撃判定が存在する時だけ処理
            if (attack == true)
            {
                // ma = collision.gameObject.GetComponent<PhysicsMaterial2D>();
                // ma.friction = ballFrictCha;                                 //摩擦を変える
                // sr = collision.gameObject.GetComponent<SpriteRenderer>();
                // sr.color = new Color(1, 1, 1, ballAlpha);                   //ボールの不透明度を変える
                presentTouch = true;                                        //Presentに触れた判定
                // Invoke("resetBall", resetTime);                                 //resetTime秒後に抵抗とボールの不透明度を元に戻す
                ball.GetComponent<Ball>().hitByPresent();
                Destroy(this.gameObject);
            }
        }
    }

    // //摩擦と不透明度を元に戻す関数
    // void resetBall()
    // {
    //     ma.friction = ballFrictIni;
    //     sr.color = new Color(1, 1, 1, 1);
    // }

    public override void controllBall()
    {        
        Debug.Log("present");
    }

     public override void repeatAnimation()
    {
        Debug.Log("present");
    }
}
