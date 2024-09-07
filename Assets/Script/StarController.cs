using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StarController : MonoBehaviour
{
    public float dropspeed=-0.05f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

     void OnTriggerEnter2D(Collider2D other){
        if(other.CompareTag("Player")){
            Destroy(gameObject);
        }
        if(other.CompareTag("barrier")){
            Destroy(gameObject);
        }
    }


    // Update is called once per frame
    void Update()
    {
        transform.Translate(0,this.dropspeed,0);
        if(transform.position.y<-4.0f){
            Destroy(gameObject);
        }else if(this.dropspeed==0){
            Destroy(gameObject);
        }
    }
}
