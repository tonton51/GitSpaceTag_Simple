using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class ItemGenerator : MonoBehaviourPunCallbacks
{
    float span=3.0f;
    float delta=0;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        this.delta+=Time.deltaTime;
        if(this.delta>this.span){
            this.delta=0;
            float x=Random.Range(-8,8);
            PhotonNetwork.Instantiate("Star",new Vector3(x, 7, 0),Quaternion.identity);
        }
    }
}
