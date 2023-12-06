using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Penguin : MovingAccelerator
{
    private float timeCount;    //時間計測用
    private bool penguinTouch;  //Penguinに触れたかどうかの判定用
    private bool attack;        //攻撃判定用
    private Rigidbody2D penguinRB;      //PenguinのRigidbody2Dを取得
    private float penguinSpeed;         //Penguinの速度を取得
    private GameObject ball;
    [SerializeField] Vector2 penguinForce　= new Vector2(-200.0f, 0.0f);    //Penguinに与える加速度
    [SerializeField] float limitSpeed = 10.0f;              //Penguinの速度を制限する
    [SerializeField] float destroyTime;//生成からdestroyまでの時間間隔
    private double spawnedTime;//スポーンされた時間

    //初期設定
    void Start()
    {
        timeCount = 0;
        penguinTouch = false;
        attack = true;
        spawnedTime = Time.time;
    }

    void Update()
    {
        penguinRB = this.GetComponent<Rigidbody2D>();    
        penguinSpeed = penguinRB.velocity.magnitude;

        //Penguinに触れたとき、攻撃判定を消して時間を計測
        if (penguinTouch == true)
        {
            attack = false;
            timeCount += Time.deltaTime;
        }
        //Penguinに触れてからresetTime秒後に攻撃判定を復活させる
        if (timeCount > 8.0f)
        {
            penguinTouch = false;
            attack = true;
            timeCount = 0;
        }
        //Penguinが画面外に出たら消す
        if (this.transform.position.y <= -20)
        {
            Destroy(this.gameObject);
        }
        //加速度に制限をつける
        if(penguinSpeed <= limitSpeed)
        {
            penguinRB.AddForce(Vector2.one * penguinForce); //Penguinに加速度を与え続ける
        }

        //一定時間たったら消す
        if(Time.time - spawnedTime > destroyTime)
        {
            Destroy(this.gameObject);
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        // ball = GameObject.FindWithTag("Ball");
        // //攻撃判定が存在する時だけ処理
        // if(collision.gameObject == ball){
        //     if (attack == true)
        //     {
        //         penguinTouch = true;
        //         ball.GetComponent<Ball>().hitByPenguin();
        //     }
        // }

        if(collision.gameObject.tag == "Ball")
        {
            if (attack == true)
            {
                penguinTouch = true;
                collision.gameObject.GetComponent<Ball>().hitByPenguin();
            }

        }
    }

    public override void controllBall()
    {
        Debug.Log("penguin");
    }

     public override void repeatAnimation()
    {
        Debug.Log("penguin");
    }

}
