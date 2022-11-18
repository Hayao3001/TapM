using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{

    //現在時間取得用変数
    private float nowTime;

    //ノーツ用変数
    Block notes;

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

    public void PointerDown(){
        Debug.Log("PUSH!!!!!");
        if(notes != null){
            notes.ChangeIsEntry();
            Debug.Log("TimeDifference:"+notes.TimeDifference());
            Destroy(notes);
        }
    }

    private void OnTriggerEnter(Collider cl)
    {
        notes = cl.gameObject.GetComponent(typeof(Block)) as Block;
    }
}
