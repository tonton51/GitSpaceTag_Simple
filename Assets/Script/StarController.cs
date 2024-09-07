using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 星の動きに関するスクリプト
public class StarController : MonoBehaviour
{
    public float dropspeed=-0.05f; // 落下速度
    
    // プレイヤーとぶつかった時の判定
     void OnTriggerEnter2D(Collider2D other){
        if(other.CompareTag("Player")){
            Destroy(gameObject);
        }
        if(other.CompareTag("barrier")){
            Destroy(gameObject);
        }
    }


    // 画面外にいったら削除
    void Update()
    {
        transform.Translate(0,this.dropspeed,0);
        if(transform.position.y<-4.0f){
            Destroy(gameObject);
        }
    }
}
