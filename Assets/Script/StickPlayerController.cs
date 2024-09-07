using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using TMPro;
using ExitGames.Client.Photon;

// 棒に繋がれたような動きをするプレイヤーの動きのスクリプト
public class StickPlayerController : MonoBehaviourPunCallbacks, IOnEventCallback
{
    public static int[] point=new int[2]; // それぞれの取得したポイント
    private const byte InputKeyEventCode = 1; // 入力キー送信用イベントコード
    private const byte PointEventCode=2; // ポイント送信用イベントコード
    Rigidbody2D rb;

    // プレイヤーの入力状態
    private bool leftKey, rightKey;

    // 受信した相手プレイヤーの入力状態
    private bool otherLeftKey, otherRightKey;

    public static bool flag = false; // 入力一致判定用フラグ

    public float moveSpeed = 5f;  // プレイヤーの移動速度

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        PhotonNetwork.AddCallbackTarget(this);
    }

    void OnDestroy()
    {
        PhotonNetwork.RemoveCallbackTarget(this);
    }

    void OnTriggerEnter2D(Collider2D other){
        if(other.gameObject.tag=="star"){ 
            if (PhotonNetwork.LocalPlayer.ActorNumber == 1)
            {
                point[0]++;
            }else
            {
                point[1]++;
            }
            RaiseEventOptions raiseEventOptions=new RaiseEventOptions{
                Receivers=ReceiverGroup.All
            };

            PhotonNetwork.RaiseEvent(PointEventCode,point,raiseEventOptions,SendOptions.SendReliable);
        }
    }

    void Update()
    {
        if (photonView.IsMine)
        {
            // StickPlayerController.flag = false;  // まずはfalseにリセット
            flag=false; // 0907変更、変更前は上の行

            CheckInput(); // 左右キーの入力状態を取得
            SendInputEvent(); // 入力状態を送信

            // 自身の入力状態と相手の入力状態が一致しているかどうかを判定
            if (CheckIfInputsMatch())
            {
                // StickPlayerController.flag = true;
                flag=true; // 0907変更、変更前は上の行

                MovePlayer();  // 一致したときのみMovePlayerを呼び出す
            }
            else
            {
                rb.velocity = Vector2.zero;  // 一致していない場合は動かさない
            }
        }
    }

    // 左右キーの入力状態を取得
    void CheckInput()
    {
        // 押されている間はtrue、押されていない間はfalse
        leftKey = Input.GetKey(KeyCode.LeftArrow);
        rightKey = Input.GetKey(KeyCode.RightArrow);
    }

    // 入力状態を送信
    void SendInputEvent()
    {
        object[] content = new object[] { leftKey, rightKey };
        RaiseEventOptions raiseEventOptions = new RaiseEventOptions { Receivers = ReceiverGroup.Others };
        PhotonNetwork.RaiseEvent(InputKeyEventCode, content, raiseEventOptions, SendOptions.SendReliable);
    }

    public void OnEvent(EventData photonEvent)
    {
        // 受信した入力状態を取得
        if (photonEvent.Code == InputKeyEventCode)
        {
            object[] data = (object[])photonEvent.CustomData;
            otherLeftKey = (bool)data[0];
            otherRightKey = (bool)data[1];
        }
        if(photonEvent.Code==PointEventCode){
            int[] receivepoint=(int[])photonEvent.CustomData;
            point=receivepoint;
            Debug.Log("point0:"+point[0]);
            Debug.Log("point1:"+point[1]);
        }
    }

    // 自身の入力状態と相手の入力状態が一致しているかどうかを判定
    bool CheckIfInputsMatch()
    {
        return leftKey == otherLeftKey && rightKey == otherRightKey;
    }

    void MovePlayer()
    {
        Vector2 movement = Vector2.zero;

        if (Input.GetKey(KeyCode.RightArrow))
        {
            movement += Vector2.right;
        }
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            movement += Vector2.left;
        }

        rb.velocity = movement.normalized * moveSpeed;
        
        // transform.positionを使った移動 物理演算を無視するので非推奨
        // Vector3 moveDirection = Vector3.zero;

        // if (leftKey)
        // {
        //     moveDirection = Vector3.left;
        // }
        // else if (rightKey)
        // {
        //     moveDirection = Vector3.right;
        // }

        // if (moveDirection != Vector3.zero)
        // {
        //     transform.position += moveDirection * moveSpeed * Time.deltaTime;
        // }
    }
}
