using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using TMPro;
using Photon.Realtime;
using UnityEngine.SceneManagement;

public class GameDirector : MonoBehaviourPunCallbacks
{
    public TextMeshProUGUI[] pointtext;
    public TextMeshProUGUI nametext;
    float delta=63.0f;
    float count=3.0f;
    public TextMeshProUGUI timertext;
    public TextMeshProUGUI counttext;
    public static int[] Rpoint=new int[2];
    private bool flag;
    public TextMeshProUGUI checktext;
    // Start is called before the first frame update
    void Start()
    {
        if (PhotonNetwork.LocalPlayer.ActorNumber == 1)
        {
            nametext.text="kuma";
        }
        else
        {
            nametext.text="rocket";
        }
    }

    // Update is called once per frame
    void Update()
    {
        flag = StickPlayerController.flag;
        StickPlayerController.flag = false;  // flagをリセット

        if (flag)
        {
            checktext.text = "OK";
        }
        else
        {
            checktext.text = "NG";
        }

        int[] point=StickPlayerController.point;
        pointtext[0].text="kuma:"+point[0].ToString();
        pointtext[1].text="rocket:"+point[1].ToString();

        this.delta-=Time.deltaTime;
        if(this.delta<=60.0f){
            counttext.enabled=false;
            timertext.text=this.delta.ToString("F2");
            if(this.delta<0){
                Rpoint=StickPlayerController.point;
                SceneManager.LoadSceneAsync("Ending",LoadSceneMode.Single);
            }
        }else{
            this.count-=Time.deltaTime;
            if(this.count<1.0f){
                counttext.text="START";
            }else{
                counttext.text=this.count.ToString("F0");
            }
            
        }
    }
}
