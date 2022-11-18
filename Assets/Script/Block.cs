using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Block : MonoBehaviour
{
    //どこのラインに向かって進むかを入れる変数
    [SerializeField] private string linename;

    //このオブジェクト。自分自身
    private Rigidbody rb;

    //ブロックのスピード
    private float speed = 3.0f;

    //判定領域に入ったときの判定変数
    private bool isEntry = false;

    public void ChangeIsEntry(){
        isEntry = !isEntry;
    }

    private float difference_time;

    //speedの取得と設定
    public float GetSetSpeed{
        get{ return speed; }
        set{ speed = value; }
    }
    
    // Start is called before the first frame update
    void Start()
    {
        //自分自身を指定
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        //ブロックにスピードを与える
        SetSpeed();
        if(isEntry){
            difference_time = Time.time;
        }
    }

    //ブロックにスピードを与えるメソッド
    private void SetSpeed(){

        //下のラインなら下にスピードを与える。
        if(linename == "bottom"){
            rb.AddForce(0f,-1*speed,0f);
        }

        //右のラインなら右にスピードを与える
        if(linename == "right"){
            rb.AddForce(speed,0f,0f);
        }

        //左のラインなら左にスピードを与える
        if(linename == "left"){
            rb.AddForce(-1*speed,0f,0f);
        }

        //上のラインなら上にスピードを与える
        if(linename == "top"){
            rb.AddForce(0f,speed, 0f);
        }

        //こっちは一度様子見

        // //右上斜のラインなら右上斜にスピードを与える
        // if(linename == "diagonally_right_up"){
        //     rb.AddForce((float)(1/2)*speed,(float)(System.Math.Sqrt(3)/2)*speed,0f);
        // }

        // //右下斜のラインなら右下斜にスピードを与える
        // if(linename == "diagonally_right_down"){
        //     rb.AddForce((float)(1/2)*speed,(-1)*(float)(System.Math.Sqrt(3)/2)*speed,0f);
        // }

        // //左上斜のラインなら左上斜にスピードを与える
        // if(linename == "diagonally_left_up"){
        //     rb.AddForce((-1)*(float)(1/2)*speed,(float)(System.Math.Sqrt(3)/2)*speed,0f);
        // }

        // //左下斜のラインなら左下斜にスピードを与える
        // if(linename == "diagonally_left_down"){
        //     rb.AddForce((-1)*(float)(1/2)*speed,(-1)*(float)(System.Math.Sqrt(3)/2)*speed,0f);
        // }
    }

    private void OnTriggerEnter(Collider cl)
    {
        Debug.Log("Entry Line!!!");
        ChangeIsEntry();
    }

    public float TimeDifference(){
        return difference_time;
    }
}
