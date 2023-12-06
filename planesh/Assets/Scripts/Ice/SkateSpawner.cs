using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkateSpawner : MonoBehaviour
{
    private Vector2 spawnPos;   //Presentが生成される位置
    private float timeCount;    //Presentが生成されてからの時間計測用
    [SerializeField] private GameObject skatePrefab;  //PrefabのPresentをセット
    [SerializeField] private float spawnTime = 10.0f;   //次のPresentが生成されるまでの時間

    //初期設定
    void Start()
    {
        spawnPos = this.transform.position;
        timeCount = 0;
        birth();
    }

    void Update()
    {
        if (timeCount <= spawnTime)
        {
            timeCount += Time.deltaTime;
        }
        if (timeCount > spawnTime)
        {
            birth();
            timeCount = 0;
        }
    }

    //Presentを生成する
    private void birth()
    {
        Instantiate(skatePrefab, spawnPos, Quaternion.identity);
    }

}
