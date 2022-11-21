using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{

    //現在時間取得用変数
    private float nowTime;

    //ノーツ用変数
    Block notes;

    //どこのラインなのかを示す文字列変数
    [SerializeField] string linename;

    //GameManager用のゲームオブジェクト変数
    [SerializeField] GameObject GameManager;
    private GameManager gamemanager;

    //クリック時間取得用変数
    private float clickTime = 0f;
    // Start is called before the first frame update
    void Start()
    {
        gamemanager = GameManager.GetComponent(typeof(GameManager)) as GameManager;
    }

    // Update is called once per frame
    void Update()
    {
        nowTime = Time.time;
    }

    public void PointerCheck(){
        if(notes != null){
            notes.ChangeIsEntry();
            if(notes.GetIsLongNotes()){
                var difference_time = notes.GetInterTime() - notes.TimeDifference();
                if((-0.2f < difference_time) && (difference_time < 0.2f)){
                    gamemanager.SetScore("perfect");
                }else if((-0.3f < difference_time) && (difference_time < 0.3f)){
                    gamemanager.SetScore("great");
                }else if((-0.4f < difference_time) && (difference_time < 0.4f)){
                    gamemanager.SetScore("bad");
                }else{
                    gamemanager.SetScore("miss");
                }
            }else{
                var difference_time = notes.TimeDifference();
                if(difference_time < 0.2f){
                    gamemanager.SetScore("perfect");
                }else if(difference_time < 0.3f){
                    gamemanager.SetScore("great");
                }else if(difference_time < 0.4f){
                    gamemanager.SetScore("bad");
                }else{
                    gamemanager.SetScore("miss");
                }
            }
            Destroy(notes);
        }
    }

    public void PointerDown(){
        if(notes != null){
            if(notes.GetIsLongNotes()){
                notes.ChangeIsEntry();
            }
        }
    }


    private void OnTriggerEnter(Collider cl)
    {
        if(((cl.gameObject.name == "下ノーツ(Clone)") || cl.gameObject.name == "下ロング(Clone)") && (linename == "bottom")){
            notes = cl.gameObject.GetComponent(typeof(Block)) as Block;
            if(notes != null){
                if(!notes.GetIsLongNotes()){
                    notes.ChangeIsEntry();
                }else{
                    notes.ChangeisEntryLongNotes();
                }
            }
        }
        if(((cl.gameObject.name == "上ノーツ(Clone)") || cl.gameObject.name == "上ロング(Clone)") && (linename == "top")){
            notes = cl.gameObject.GetComponent(typeof(Block)) as Block;
            if(notes != null){
                if(!notes.GetIsLongNotes()){
                    notes.ChangeIsEntry();
                }else{
                    notes.ChangeisEntryLongNotes();
                }
            }
        }
        if(((cl.gameObject.name == "右ノーツ(Clone)") || cl.gameObject.name == "右ロング(Clone)") && (linename == "right")){
            notes = cl.gameObject.GetComponent(typeof(Block)) as Block;
            if(notes != null){
                if(!notes.GetIsLongNotes()){
                    notes.ChangeIsEntry();
                }else{
                    notes.ChangeisEntryLongNotes();
                }
            }
        }
        if(((cl.gameObject.name == "左ノーツ(Clone)") || cl.gameObject.name == "左ロング(Clone)") && (linename == "left")){
            notes = cl.gameObject.GetComponent(typeof(Block)) as Block;
            if(notes != null){
                if(!notes.GetIsLongNotes()){
                    notes.ChangeIsEntry();
                }else{
                    notes.ChangeisEntryLongNotes();
                }
            }
        }
    }
}
