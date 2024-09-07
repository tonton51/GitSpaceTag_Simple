using System.Collections;
using System.Collections.Generic;
using UnityEngine;
 
public class LineGenerator : MonoBehaviour
{
    private LineRenderer line;
    private GameObject kuma;
    private GameObject rocket;
 
    void Start()
    {
        // LineRendererコンポーネントを取得
        line = GetComponent<LineRenderer>();
 
        // 線の幅を決める
        line.startWidth = 0.1f;
        line.endWidth = 0.1f;
 
        // 頂点の数を決める
        line.positionCount = 2;
 
        // プレイヤーオブジェクトの参照を取得
        StartCoroutine(FindPlayers());
    }
 
    IEnumerator FindPlayers()
    {
        // Player1とPlayer2が生成されるまで待つ
        while (kuma == null || rocket == null)
        {
            kuma = GameObject.Find("kuma(Clone)");
            rocket = GameObject.Find("rocket(Clone)");
            yield return null; // 次のフレームまで待つ
        }
    }
 
    void Update()
    {
        // プレイヤーオブジェクトが見つかっている場合に線を引く
        if (kuma != null && rocket != null)
        {
            line.SetPosition(0, kuma.transform.position);
            line.SetPosition(1, rocket.transform.position);
        }
    }
}