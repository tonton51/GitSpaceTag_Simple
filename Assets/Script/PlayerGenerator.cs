using System.Collections;
using UnityEngine;
using Photon.Pun;
 
public class PlayerGenerator : MonoBehaviourPunCallbacks
{
    private GameObject kuma;
    private GameObject rocket;
 
    void Start()
    {
        if (PhotonNetwork.LocalPlayer.ActorNumber == 1)
        {
            kuma = PhotonNetwork.Instantiate("kuma", new Vector3(-2.5f, 0, 0), Quaternion.identity);
        }
        else
        {
            rocket = PhotonNetwork.Instantiate("rocket", new Vector3(2.5f, 0, 0), Quaternion.identity);
        }
        StartCoroutine(CheckAndSetDistanceJoint());
    }
 
    IEnumerator CheckAndSetDistanceJoint()
    {
        // Player1とPlayer2が生成されるのを待つ
        while (kuma == null || rocket == null)
        {
            kuma = GameObject.Find("kuma(Clone)");
            rocket = GameObject.Find("rocket(Clone)");
            yield return null; // 次のフレームまで待つ
        }
 
        // プレイヤー1とプレイヤー2が両方とも生成されたら、DistanceJoint2Dを設定
        SetDistanceJoint(kuma, rocket);
    }
 
    private void SetDistanceJoint(GameObject kuma, GameObject rocket)
    { 
        var joint1 = kuma.AddComponent<DistanceJoint2D>();
        joint1.connectedBody = rocket.GetComponent<Rigidbody2D>();
        joint1.autoConfigureDistance = false;
        joint1.distance = 5f;
        joint1.maxDistanceOnly = true;
 
        var joint2 = rocket.AddComponent<DistanceJoint2D>();
        joint2.connectedBody = kuma.GetComponent<Rigidbody2D>();
        joint2.autoConfigureDistance = false;
        joint2.distance = 5f;
        joint2.maxDistanceOnly = true;
    }
}