using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using TMPro;
using ExitGames.Client.Photon;

public class StickPlayerController : MonoBehaviourPunCallbacks, IOnEventCallback
{
    public static int[] point=new int[2];
    private const byte INPUT_EVENT_CODE = 1;
    private const byte PointEventCode=2;
    Rigidbody2D rb;

    // プレイヤーの入力状態
    private bool leftKey, rightKey;

    // 受信した相手プレイヤーの入力状態
    private bool otherLeftKey, otherRightKey;

    public static bool flag = false;

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
        // if (photonView.IsMine)
        // {
        //     StickPlayerController.flag = false;  // まずはfalseにリセット

        //     CheckInput();
        //     SendInputEvent();

        //     if (CheckIfInputsMatch())
        //     {
        //         StickPlayerController.flag = true;
        //         MovePlayer();
        //         // Debug.Log("Inputs match! Both players are pressing the same keys.");
        //     }
        // }
        if (photonView.IsMine)
        {
            StickPlayerController.flag = false;  // まずはfalseにリセット

            CheckInput();
            SendInputEvent();

            if (CheckIfInputsMatch())
            {
                StickPlayerController.flag = true;
                MovePlayer();  // 一致したときのみMovePlayerを呼び出す
            }
            else
            {
                rb.velocity = Vector2.zero;  // 一致していない場合は動かさない
            }
        }
    }

    void CheckInput()
    {
        leftKey = Input.GetKey(KeyCode.LeftArrow);
        rightKey = Input.GetKey(KeyCode.RightArrow);
    }

    void SendInputEvent()
    {
        object[] content = new object[] { leftKey, rightKey };
        RaiseEventOptions raiseEventOptions = new RaiseEventOptions { Receivers = ReceiverGroup.Others };
        PhotonNetwork.RaiseEvent(INPUT_EVENT_CODE, content, raiseEventOptions, SendOptions.SendReliable);
    }

    public void OnEvent(EventData photonEvent)
    {
        if (photonEvent.Code == INPUT_EVENT_CODE)
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
