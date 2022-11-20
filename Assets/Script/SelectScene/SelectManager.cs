using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SelectManager : MonoBehaviour
{
    //データベース管理ゲームオブジェクトを取得するための変数
    [SerializeField] private GameObject MusicDataManager;
    private MusicDataManager music_data_manager;

    //音楽データを保存する変数
    private List<MusicData> MusicDataList;

    //ボタン用ゲームオブジェクトを保存するための変数
    [SerializeField] private GameObject Button;

    //Canvasを保存するための変数
    [SerializeField] private Transform Canvas;

    //音楽データの要素数
    private int MusicDataCount;

    //ボタンの初期配置の変数
    private float base_x;
    private float base_y;

    //Startボタンが出てるかどうかを判断する変数
    private bool isStartButton = false;

    public void ChangeisStartButton(){
        isStartButton = !isStartButton;
    }

    public bool GetisStartButton(){
        return isStartButton;
    }

    public Transform GetCanvas(){
        return Canvas;
    }

    // Start is called before the first frame update
    void Start()
    {
        music_data_manager = MusicDataManager.GetComponent(typeof(MusicDataManager)) as MusicDataManager;
        if(music_data_manager != null){
            MusicDataList = music_data_manager.GetMusicDataList();
            MusicDataCount = music_data_manager.GetMusicDataBaseCount();
        }
        base_x = Button.transform.position.x;
        base_y = Button.transform.position.y;
        SetButton();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void SetButton(){
        for(int i=0; i<MusicDataCount; i++){
            var place = new Vector3(base_x+960f, base_y+540f, 0);
            GameObject obj = Instantiate(Button, Vector3.zero, Quaternion.identity);
            
            var canvas_button = obj.GetComponent(typeof(RectTransform)) as RectTransform;
            canvas_button.anchoredPosition = place;
            obj.transform.SetParent(Canvas);

            var button = obj.GetComponent(typeof(Button)) as Button;
            var button_text = button.GetComponentInChildren<Text>();
            button_text.text = MusicDataList[i].GetMusicName();

            var button_Click = obj.GetComponent(typeof(ButtonClickObject)) as ButtonClickObject;
            button_Click.GetSetSceneName = MusicDataList[i].GetSceneName();
            

            base_y -= 120f;
        }
    }
}
