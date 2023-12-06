using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Snowman : StillAccelerator
{
    private Animator snowmanAnimator;
    private Vector2 spawnPos;                               //snowballが生成される場所
    private float timeCount;                                //snowballが生成されてからの時間計測
    [SerializeField] private GameObject snowballPrefab;     //prefab内のsnowballをセット
    [SerializeField] private float spawnTime = 5.0f;        //snowballを生成する間隔
    [SerializeField] Vector2 iniForce = new Vector2(-30,10); //snowballを発射させる力

    //audio
    AudioSource audioData;
    [SerializeField] AudioClip sound;

    //初期設定
    void Start()
    {
        snowmanAnimator = this.GetComponentInChildren<Animator>();
        spawnPos = this.transform.position;
        timeCount = 0;
        snowmanAnimationTrigger();

        //audio
        audioData = GetComponent<AudioSource>();
    }

    void Update()
    {
        if (timeCount <= spawnTime)
        {
            timeCount += Time.deltaTime;
        }
        if (timeCount > spawnTime)
        {
            snowmanAnimationTrigger();
            timeCount = 0;
        }
    }

    //雪玉発射(アニメーションイベント)
    private void shootSnowBall()
    {
        //audio
        audioData.Play();

        GameObject snowball = Instantiate(snowballPrefab, spawnPos, Quaternion.identity);           //snowball生成
        snowball.GetComponent<Rigidbody2D>().AddForce(Vector2.one * iniForce, ForceMode2D.Impulse); //snowballの発射
    }

    //アニメーションのための関数
    private void snowmanAnimationTrigger()
    {
        snowmanAnimator.SetTrigger("SnowmanThrowTrigger");
    }
    
    public override void controllBall()
    {

    }
    public override void repeatAnimation()
    {

    }
}
