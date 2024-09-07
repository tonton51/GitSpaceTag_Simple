using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using TMPro;
using Photon.Realtime;
using UnityEngine.SceneManagement;

// ゲーム全体の監督用スクリプト（点数、役割、時間を管理）
public class GameDirector : MonoBehaviourPunCallbacks
{
    public TextMeshProUGUI[] pointtext;
    public TextMeshProUGUI nametext;
    float delta=63.0f; // 制限時時間
    float count=3.0f; // カウントダウン
    public TextMeshProUGUI timertext;
    public TextMeshProUGUI counttext;
    public static int[] Rpoint=new int[2]; // それぞれのポイント
    private bool flag;
    public TextMeshProUGUI checktext;
    // Start is called before the first frame update
    void Start()
    {
        // それぞれの役割を表示する
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
        // 移動方向が一致しているかの判定
        StickPlayerController.flag = false;  // flagをリセット // 0907上下入れ替え
        flag = StickPlayerController.flag;
        if (flag)
        {
            checktext.text = "OK";
        }
        else
        {
            checktext.text = "NG";
        }

        //  ポイント表示
        int[] point=StickPlayerController.point;
        pointtext[0].text="kuma:"+point[0].ToString();
        pointtext[1].text="rocket:"+point[1].ToString();

        // 時間管理
        this.delta-=Time.deltaTime;
        if(this.delta<=60.0f){
            counttext.enabled=false;
            timertext.text=this.delta.ToString("F2");
            // 制限時間が0になったらシーン遷移
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
