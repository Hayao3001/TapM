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
    private float speed = 6.0f;

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
    
    //ノーツかロングノーツかどうかを判定するbool変数
    private bool islongnotes = false;

    //islongnotesをtrueに変える関数
    public void ChangeIsLongNotes(){
        islongnotes = true;
    }

    public bool GetIsLongNotes(){
        return islongnotes;
    }

    //開始時間を記録する変数
    private float starttime = 0f;

    public void SetStartTIme(float starttime){
        this.starttime = starttime;
    }
    //最後の時間を記録する変数
    private float endtime = 0f;

    public void SetEndTime(float endtime){
        this.endtime = endtime;
    }

    //ロングノーツだった場合の時間間
    private float intertime;

    public float GetInterTime(){return intertime;}

    //ロングノーツが判定ラインに入ったときの判定変数
    private bool isEntryLongNotes = false;

    //GameManager用のゲームオブジェクト変数
    private GameObject GameManager;
    private GameManager gamemanager;

    public void ChangeisEntryLongNotes(){
        isEntryLongNotes = !isEntryLongNotes;
    }

    //ロングノーツが侵入したときに記録する時間変数
    private float longnotes_entrytime;

    // Start is called before the first frame update
    void Start()
    {
        //自分自身を指定
        rb = GetComponent<Rigidbody>();
        if(islongnotes){
            intertime = endtime - starttime;
        }
        GameManager = GameObject.Find("GameManager");
        gamemanager = GameManager.GetComponent(typeof(GameManager)) as GameManager;
    }

    // Update is called once per frame
    void Update()
    {
        //ブロックにスピードを与える
        SetSpeed();
        if(isEntry){
            difference_time += Time.deltaTime;
        }

        if(isEntryLongNotes){
            longnotes_entrytime += Time.deltaTime;
        }
        //ロングノーツか否か
        if(islongnotes){
            //ロングノーツが入ったときの時間を記録して、その時間がロングノーツ単体の時間+1.0f秒経過したらそのオブジェクトを消す
            if(longnotes_entrytime > intertime + 1.0f){
                gamemanager.SetScore("miss");
                Destroy(this.gameObject);
            }

        }else{
            //ノーツが入って1.0f秒立ったらそのオブジェクトを消す。
            if(difference_time > 1.0f){
                gamemanager.SetScore("miss");
                Destroy(this.gameObject);
            }
        }
    }

    //ブロックにスピードを与えるメソッド
    private void SetSpeed(){

        //下のラインなら下にスピードを与える。
        if(linename == "bottom"){
            rb.velocity = new Vector3(0f,-1*speed,0f);
        }

        //右のラインなら右にスピードを与える
        if(linename == "right"){
            rb.velocity = new Vector3(speed,0f,0f);
        }

        //左のラインなら左にスピードを与える
        if(linename == "left"){
            rb.velocity = new Vector3(-1*speed,0f,0f);
        }

        //上のラインなら上にスピードを与える
        if(linename == "top"){
            rb.velocity = new Vector3(0f,speed, 0f);
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

    public float TimeDifference(){
        return difference_time;
    }

}
