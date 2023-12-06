using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PenguinSpawner : MonoBehaviour
{
    private Vector2 spawnPos;   //Penguinが生成される位置
    private float timeCount;    //penguinが生成されてからの時間計測用
    [SerializeField] private GameObject penguinPrefab;  //PrefabのPenguinをセット
    [SerializeField] private float spawnTime = 10.0f;   //次のPenguinが生成されるまでの時間間隔

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
        else if (timeCount > spawnTime)
        {
            birth();
            timeCount = 0;
        }
    }

    //Penguinを生成する
    private void birth()
    {
        Instantiate(penguinPrefab, spawnPos, Quaternion.identity);
    }

}
