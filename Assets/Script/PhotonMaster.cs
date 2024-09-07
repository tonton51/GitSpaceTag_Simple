using UnityEngine.SceneManagement;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.UI;
public class PhotonMaster : MonoBehaviourPunCallbacks
{
    // コメントアウトした部分はUIをテキスト表示する際に使うかもなので残しておく
    private const int MaxPlayerPerRoom = 2; // maxplayerの設定
    
    // 初期化メソッドstartより先
    private void Awake()
    {
        PhotonNetwork.AutomaticallySyncScene = true; // trueならマスタークライアントと同期
        
    }
    // マスターサーバーへ接続する
    private void Start()
    {
        PhotonNetwork.ConnectUsingSettings(); // マスターサーバーへの接続
    }

    //これをボタンにつける FindOponentは作成したメソッド
    public void MatchStart()
    {
        // photonに接続できたら
        if (PhotonNetwork.IsConnected)
        {
            // フィルタに一致するランダムなルームに参加
            PhotonNetwork.JoinRandomRoom();
        }
    }

    
    // //Photonのコールバック
    public override void OnConnectedToMaster()
    {
    //     Debug.Log("マスターに繋ぎました。");
    }

    // // photonサーバーに繋げない場合
    public override void OnDisconnected(DisconnectCause cause)
    {
    //     Debug.Log($"{cause}の理由で繋げませんでした。");
    }

    // ルームに入るのに失敗したら呼び出されるメソッド
    public override void OnJoinRandomFailed(short returnCode, string message)
    {
        Debug.Log("ルームを作成します。");
        // maxplayer2でルーム作成
        PhotonNetwork.CreateRoom(null, new RoomOptions { MaxPlayers = MaxPlayerPerRoom });
    }

    // // ルームに入るのに成功
    public override void OnJoinedRoom()
    {

    }

    // 人数が揃ったかの確認
    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        if (PhotonNetwork.IsMasterClient)
        {
            if (PhotonNetwork.CurrentRoom.PlayerCount == MaxPlayerPerRoom)
            {
                PhotonNetwork.CurrentRoom.IsOpen = false; // ルームに入室できないようにする
                // statusText.text = "対戦相手が揃いました。バトルシーンに移動します。";
                PhotonNetwork.LoadLevel("ExplainScene");

            }
        }
    }
}