using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using TMPro;

public class ResultDirector : MonoBehaviour
{
    public TextMeshProUGUI Rocketresult;
    public TextMeshProUGUI Kumaresult;
    // Start is called before the first frame update
    void Start()
    {
        Kumaresult.text="Kuma"+GameDirector.Rpoint[0].ToString();
        Rocketresult.text="Rocket"+GameDirector.Rpoint[1].ToString();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
