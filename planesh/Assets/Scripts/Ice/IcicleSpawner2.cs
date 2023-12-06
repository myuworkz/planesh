using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IcicleSpawner2 : MonoBehaviour
{

    private Vector2[] spawnPosArray = {new Vector2(-8.0f, 4.2f), new Vector2(-5.7f, 4.7f), new Vector2(-2.0f, 4.0f), new Vector2(0.9f, 1.5f), new Vector2(5.3f, 2.9f)};//生成位置の候補リスト
    private List<int> nothingIcicleList = new List<int>();//シーンにないつららの生成位置番号リスト
    //本クラスでのみ代入可、直近の生成位置番号
    public int latestIcicle
    {
        get;
        private set;
    } = 0;

    [SerializeField] private GameObject iciclePrefab; //プレハブ
    private float timeCount = 0.0f;
    [SerializeField] private float spawnRanDur = 5.0f;
    private const float MIN_SPAWN_TIME = 2.0f;
    private const float MAX_SPAWN_TIME = 5.0f;

    void Start()
    {
        //生成位置の候補数ぶんの番号追加
        for(int i = 0; i < spawnPosArray.Length; i++)
        {
            nothingIcicleList.Add(i);
        }
    }

    void Update()
    {
        if (timeCount <= spawnRanDur)
        {
            timeCount += Time.deltaTime;
        }
        else if (spawnRanDur < timeCount)
        {
            //生成位置残ってたら
            if (0 < nothingIcicleList.Count)
            {
                //位置指定&つらら生成
                latestIcicle = getRandomInList(nothingIcicleList);
                generateIcicle(latestIcicle);

                //生成時間リセット
                timeCount = 0;
                spawnRanDur = Random.Range(MIN_SPAWN_TIME, MAX_SPAWN_TIME);
            }
        }
    }

    //つらら生成
    private void generateIcicle(int num)
    {
        //インスタンス作成
        Instantiate(iciclePrefab, spawnPosArray[num], Quaternion.identity);
    }

    //リストからランダムに値を抜き出し
    private T getRandomInList<T>(List<T> Params)
    {
        //リスト数からランダム数
        int num = Random.Range(0, Params.Count);
        //先にリストから取得
        var Param = Params[num];
        //リストから削除
        Params.Remove(Params[num]);

        return Param;
    }

    //つらら破壊時、リストに値を復活
    public void setIcicleNumToList(int num)
    {
        //配列の数以内の正の値以外を弾く
        if(0 <= num && num < spawnPosArray.Length)
        {
            //リストに重複がないようにする
            if (nothingIcicleList.Contains(num) == false)
            {
                //戻す
                nothingIcicleList.Add(num);
            }
        }
    }
}
