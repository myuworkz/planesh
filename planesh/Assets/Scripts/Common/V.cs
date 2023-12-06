using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 動くAcceleratorの位置情報を持ったクラス
/// </summary>   
public class V : MonoBehaviour
{
    Vector2 currentPos;
    Vector2 firstPos;

    /// <param name="firstPos">ゲームオブジェクトの初期位置</param>
    /// <param name="accelaration">加える力のベクトル</param>        
    public V (Vector2 firstPos)
    {
        this.firstPos = firstPos;
        this.currentPos = firstPos;
    }

    void Update()
    {
         currentPos = this.transform.position;
    }


}
