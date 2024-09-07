using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

// アイテムを生成するためのスクリプト
public class ItemGenerator : MonoBehaviourPunCallbacks
{
    float span=3.0f; // アイテム生成間隔
    float delta=0;
    // Update is called once per frame
    void Update()
    {
        this.delta+=Time.deltaTime;
        if(this.delta>this.span){
            this.delta=0;
            float x=Random.Range(-8,8);
            PhotonNetwork.Instantiate("Star",new Vector3(x, 7, 0),Quaternion.identity);
        }
    }
}
