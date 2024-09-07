using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Photon.Pun;
using Photon.Realtime;
using ExitGames.Client.Photon;
using UnityEngine.SceneManagement;

// 役割表示のUIに関するスクリプト
public class ExplainText : MonoBehaviourPunCallbacks,IOnEventCallback
{
    private const byte ButtonEventCode=1; // ボタンクリック判定用のイベントコード
    public GameObject button;
    public TextMeshProUGUI Role;
    // public TextMeshProUGUI Explain; // それぞれの役割に応じた説明文
    public GameObject kumaex;
    public GameObject rocketex;
    int buttoncount=0;
    // Start is called before the first frame update
    void Start()
    {
        // それぞれのActorNumberに応じて画像と説明文を生成
        if (PhotonNetwork.LocalPlayer.ActorNumber == 1)
        {
            Instantiate(kumaex, new Vector3(0, 0, 0), Quaternion.identity);
            Role.text="クマ";
            // Explain.text="星をたくさん取ってください";

        }
        else
        {
            Instantiate(rocketex, new Vector3(0, 0, 0), Quaternion.identity);
            Role.text="ロケット";
            // Explain.text="星を避けてください";

        }
        PhotonNetwork.AddCallbackTarget(this); // ボタンクリック判定用のイベントコールバック
    }

    // 他のシーンでもイベントが残らないようにするためのメソッド
    void OnDestroy(){
        PhotonNetwork.RemoveCallbackTarget(this);
    }

    // ボタンがクリックされたら準備完了でボタンの表示を消す
    public void OnClick(){
        RaiseEventOptions raiseEventOptions=new RaiseEventOptions{
            Receivers=ReceiverGroup.All
        };
        PhotonNetwork.RaiseEvent(ButtonEventCode,null,raiseEventOptions,SendOptions.SendReliable);
        button.SetActive(false);
    }

    // ボタンが押されたことを相手に知らせるため
    public void OnEvent(EventData photonEvent){
        if(photonEvent.Code==ButtonEventCode){
            buttoncount+=1;
        }
    }

    // Update is called once per frame
    void Update()
    {
        // 両方のプレイヤーが準備完了したらゲームシーンに移行
        if(buttoncount==2){
            SceneManager.LoadScene("GameScene");
        }
    }
}
