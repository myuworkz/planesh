using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Icicle : StillAccelerator
{
    private int numIcicle; //位置通し番号受け取り
    [SerializeField] private GameObject icicleSpawner; //スポナー認識用

    private float timeCount; //時間計測用
    [SerializeField] private float fallRanDur; //落ちるまでの時間
    private const float MIN_FALL_TIME = 5.0f; //落ちるまでの最小時間
    private const float MAX_FALL_TIME = 10.0f; //落ちるまでの最大時間
    private bool fallFlag = false; //落下フラグ
    private const float GRAVITY = 9.81f;
    [SerializeField] private float speed = 15.0f; //Icicleの密度

    [SerializeField] private float glowSizeIcicle; //成長サイズ
    [SerializeField] private float nowSizeIcicle = 0.0f; //成長サイズ
    private const float MIN_SIZE = 0.2f; //最小サイズ
    private const float MAX_SIZE = 0.5f; //最大サイズ
    private bool completedGlowIcicle = false; //成長完了
    private bool isWobbleOnce = false; //揺れ移行一度だけ

    private bool attack = true; //攻撃判定用
    [SerializeField] private GameObject ball;
    private bool hitBall = false;
    private Rigidbody2D rb; //ボールの動きを変化させる用
    private SpriteRenderer sr; //ボールの不透明度を変化させる用
    [SerializeField] private float ballDragIni = 1.0f; //抵抗初期設定
    [SerializeField] private float ballDragCha = 10.0f; //抵抗変化(1.0を基準として、数値を大きくすると抵抗も大きくなる)
    [SerializeField] private float ballAlpha = 0.2f; //不透明度変化(1.0を基準として、数値を小さくすると不透明度が下がる)
    [SerializeField] private float resetTime = 8.0f;    //抵抗と不透明度の変化をリセットするまでの時間

    private Animator icicleAnimator; //アニメーター

    //audio
    AudioSource audioData;
    [SerializeField] AudioClip sound;



    public override void controllBall()
    {

    }
    public override void repeatAnimation()
    {

    }

    private void Awake()
    {
        //スポナー・ボール参照
        icicleSpawner = GameObject.Find("Icicle Spawner");
        ball = GameObject.Find("Ball");

        //生成時、つららの生成位置番号にアクセス
        numIcicle = icicleSpawner.GetComponent<IcicleSpawner>().latestIcicle;

        //rigidbody設定＝collisionで動かない
        this.gameObject.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Static;

        //重力無視
        this.GetComponent<Rigidbody2D>().gravityScale = 0.0f;
        //落下までの時間決定
        fallRanDur = Random.Range(MIN_FALL_TIME, MAX_FALL_TIME);
        //初期化
        timeCount = 0.0f;
        fallFlag = false;

        //最大サイズ決定
        glowSizeIcicle = Random.Range(MIN_SIZE, MAX_SIZE);
        //初期化
        nowSizeIcicle = 0.0f;
        completedGlowIcicle = false;
        isWobbleOnce = false;
        this.GetComponent<Transform>().localScale = Vector3.one * nowSizeIcicle;

        //アニメーター
        icicleAnimator = this.GetComponentInChildren<Animator>();


        //audio
        audioData = GetComponent<AudioSource>();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        timeCount += Time.deltaTime;

        //生成中
        if(completedGlowIcicle == false)
        {
            glowIcicle();
        }

        //時間経過＆生成完了
        else if (fallRanDur <= timeCount && completedGlowIcicle == true)
        {
            if (isWobbleOnce == false)
            {
                //一度だけ
                isWobbleOnce = true;

                //揺れ開始
                wobbleIcicle();
            }
        }

        //フラグ立ったら落下
        if(fallFlag == true)
        {
            fallIcicle();
        }
    }


    //生成アニメーション(手動)
    private void glowIcicle()
    {
        //成長
        if (nowSizeIcicle < glowSizeIcicle)
        {
            nowSizeIcicle += 0.04f;
            this.GetComponent<Transform>().localScale = Vector3.one * nowSizeIcicle;
        }
        else if (glowSizeIcicle <= nowSizeIcicle)
        {
            completedGlowIcicle = true;
        }
    }

    //揺れアニメトリガー
    private void wobbleIcicle()
    {
        icicleAnimator.SetTrigger("icicleWobbleTrigger");
    }

    //wobbleアニメーションイベントで呼び出して有効化
    public void fallFlagIcicle()
    {
        //落ちる際、コライダーをisTriggerに
        this.GetComponent<Collider2D>().isTrigger = true;

        //rigidbody設定＝collisionで動く
        this.gameObject.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;

        //攻撃有効化
        attack = true;

        //落下フラグ
        fallFlag = true;

        //audio play once
        audioData.Play();

    }

    //落とす
    private void fallIcicle()
    {
        //重力(サイズによって力を強く、speedで微調整)
        this.GetComponent<Rigidbody2D>().AddForce(Vector2.down * (GRAVITY + speed));
    }

    //衝突判定
    private void OnTriggerEnter2D(Collider2D collision)
    {
        //止める
        this.GetComponent<Rigidbody2D>().velocity = new Vector2(1, 0);

        //攻撃判定が存在する時だけ処理
        if (attack == true)
        {
            //破壊アニメーション
            icicleAnimator.SetTrigger("icicleCrashTrigger");

            //ボールに影響
            if (collision.gameObject == ball)
            {
                hitBall = true;
                rb = collision.gameObject.GetComponent<Rigidbody2D>();
                rb.drag = ballDragCha;                                         //抵抗を変える
                sr = collision.gameObject.GetComponent<SpriteRenderer>();
                sr.color = new Color(1, 1, 1, ballAlpha);                   //ボールの不透明度を変える
                Invoke("resetBall", resetTime);
            }

            //攻撃処理は一度のみ
            attack = false;
        }
    }

    //ボールリセット＆つらら消す
    private void resetBall()
    {
        rb.drag = ballDragIni;
        sr.color = new Color(1, 1, 1, 1);

        icicleSpawner.GetComponent<IcicleSpawner>().setIcicleNumToList(numIcicle);
        Destroy(this.gameObject);
    }

    //アニメーションイベントでつらら消す(ボールに当たらなかったとき)
    public void destroyIcicle()
    {
        if (hitBall == false)
        {
            icicleSpawner.GetComponent<IcicleSpawner>().setIcicleNumToList(numIcicle);
            Destroy(this.gameObject);
        }
    }
}
