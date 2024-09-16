using System.Collections.Generic;
using System.IO;
using UnityEngine;
using System.Linq; // LINQをインポート
using System.Text;


public class PlayerMoveLog : MonoBehaviour
{
    public GameObject player1;       // プレイヤー1オブジェクト
    public GameObject player2;       // プレイヤー2オブジェクト

    class PlayerLog{
        public float time;
        public float player1x;
        public float player2x;

        public PlayerLog(float time, float player1x, float player2x){
            this.time = time;
            this.player1x = player1x;
            this.player2x = player2x;
        }
    }
    private List<PlayerLog> playerLog = new List<PlayerLog>();

    private float timer = 0f;        // 時間計測用
    private int num=0;

 
    void Update()
    {
        player1 = GameObject.FindWithTag("kuma");
        player2 = GameObject.FindWithTag("rocket");
        timer += Time.deltaTime;

        playerLog.Add(new PlayerLog(timer, player1.transform.position.x, player2.transform.position.x));
        Debug.Log("time:" + timer + " player1x:" + player1.transform.position.x + " player2x:" + player2.transform.position.x);

    }

    void OnDestroy()
    {
        SavePlayerLogToCSV(playerLog, "PlayerLog.csv");
    }

    private void SavePlayerLogToCSV(List<PlayerLog> playerLog, string fileName)
    {
        StringBuilder csvData = new StringBuilder();
        csvData.AppendLine("Time, Player1_X, Player2_X");

        foreach (PlayerLog log in playerLog)
        {
            csvData.AppendLine($"{log.time}, {log.player1x}, {log.player2x}");
        }

        File.WriteAllText(fileName, csvData.ToString());
    }
    void Start()
    {
        // CSVファイルを開いて、ヘッダー行を書き込む
        string fileName = Path.Combine(System.Environment.CurrentDirectory, "PlayerLog.csv");
        File.WriteAllText(fileName, "Time, Player1_X, Player2_X\n");
    }
}
