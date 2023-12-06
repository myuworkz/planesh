using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 動くAcceleratorの動く範囲をもったクラス
/// </summary> 
/// <remarks>
///　ゲームオブジェクトの初期位置を原点とし, 
//// ゲームオブジェクトは, 左上をupperLeft, 右下をlowerRightとする長方形の範囲を動く
///</remarks>
public class MovingV : MonoBehaviour
{
    Vector2 upperLeft;
    Vector2 lowerRight;
    Vector2 speed;

    ///<param name="upperLeft">
    ///ゲームオブジェクトの初期位置を原点とし, ゲームオブジェクトが動く範囲の長方形のうち左上
    ///</param>
    ///<param name="lowerRight">
    ///ゲームオブジェクトの初期位置を原点とし, ゲームオブジェクトが動く範囲の長方形のうち右下
    ///</param>
    ///<param name="speed">
    ///ゲームオブジェクトが1フレームに動くベクトル量
    ///</param>
    public MovingV(Vector2 upperLeft, Vector2 lowerRight, Vector2 speed)
    {
        this.upperLeft = upperLeft;
        this.lowerRight = lowerRight;
        this.speed = speed;
    }

}
