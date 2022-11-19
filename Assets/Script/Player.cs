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

    //クリック時間取得用変数
    private float clickTime = 0f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        nowTime = Time.time;
    }

    public void PointerCheck(){
        Debug.Log("PUSH!!!!!");
        if(notes != null){
            notes.ChangeIsEntry();
            Debug.Log("TimeDifference:"+notes.TimeDifference());
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
            Debug.Log("Entry Line!!!");
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
            Debug.Log("Entry Line!!!");
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
            Debug.Log("Entry Line!!!");
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
            Debug.Log("Entry Line!!!");
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
