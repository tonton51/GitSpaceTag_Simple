using System.Collections;
using UnityEngine;
using Photon.Pun;
 
 // プレイヤーを生成するためのスクリプト
public class StickPlayerGenerator : MonoBehaviourPunCallbacks
{
    private GameObject kuma;
    private GameObject rocket;
 
    void Start()
    {
        if (PhotonNetwork.LocalPlayer.ActorNumber == 1)
        {
            kuma = PhotonNetwork.Instantiate("kuma", new Vector3(-1.5f, -3.5f, 0), Quaternion.identity);
        }
        else
        {
            rocket = PhotonNetwork.Instantiate("rocket", new Vector3(1.5f, -3.5f, 0), Quaternion.identity);
        }
    }
}