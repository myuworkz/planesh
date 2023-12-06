using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Snowball : MonoBehaviour
{
    private float timeCount;    //時間計測用
    private bool snowballTouch; //snowballに触れたかどうかの判定用
    private bool attack;        //攻撃判定用
    private SpriteRenderer sr;  //ボールの不透明度を変化させる用
    [SerializeField] private float deleteTime = 0.1f;    //snowballを消すまでの時間

    //初期設定
    void Start()
    {
        timeCount = 0;
        snowballTouch = false;
        attack = true;
    }

    void Update()
    {
        //snowballに触れたとき、攻撃判定を消して時間を計測
        if (snowballTouch == true)
        {
            Invoke("snowballDelete", deleteTime);
        }
        //snowballが画面外に出たら消す
        if (this.transform.position.y <= -20)
        {
            snowballDelete();
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        snowballTouch = true;   //snowballに触れた判定
    }

    void snowballDelete()
    {
        Destroy(this.gameObject);
    }

}
