using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Photon.Pun;
using Photon.Realtime;
using ExitGames.Client.Photon;
using UnityEngine.SceneManagement;

public class ExplainText : MonoBehaviourPunCallbacks,IOnEventCallback
{
    private const byte ButtonEventCode=1;
    public GameObject button;
    public TextMeshProUGUI Role;
    // public TextMeshProUGUI Explain;
    public GameObject kumaex;
    public GameObject rocketex;
    int buttoncount=0;
    // Start is called before the first frame update
    void Start()
    {
        
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
        PhotonNetwork.AddCallbackTarget(this);
    }

    void OnDestroy(){
        PhotonNetwork.RemoveCallbackTarget(this);
    }

    public void OnClick(){
        RaiseEventOptions raiseEventOptions=new RaiseEventOptions{
            Receivers=ReceiverGroup.All
        };
        PhotonNetwork.RaiseEvent(ButtonEventCode,null,raiseEventOptions,SendOptions.SendReliable);
        button.SetActive(false);
    }

    public void OnEvent(EventData photonEvent){
        if(photonEvent.Code==ButtonEventCode){
            buttoncount+=1;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(buttoncount==2){
            SceneManager.LoadScene("GameScene");
            // PhotonNetwork.LoadLevel("GameScene");
        }
    }
}
