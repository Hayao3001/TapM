using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ButtonClickObject : MonoBehaviour
{
    //次のシーン名を保存する変数
    private string SceneName;

    public string GetSetSceneName{
        get{ return this.SceneName; }
        set{ this.SceneName = value; }
    }

    //音楽データ保存用変数
    private AudioClip music;

    public AudioClip GetSetMusic{
        get{ return this.music; }
        set{ this.music = value; }
    }

    //SelectManagerを生成するためのゲームオブジェクト変数
    private GameObject selectmanager;
    private SelectManager SelectManager;

    //Startボタン,Backボタンを生成するためのゲームオブジェクト変数
    [SerializeField] private GameObject StartButton;
    [SerializeField] private GameObject BackButton;
    private GameObject startbutton;
    private GameObject backbutton;

    private GameObject HighScoreText;
    private Text high_score;

    //Canvasを保存するための変数
    private Transform Canvas;

    //ハイスコア用変数
    private int highScore;

    public string GetSetHighScoreKey{
        get { return this.higeScore_key; }
        set { this.higeScore_key = value; }
    }

    //ハイスコアが保存されているキー変数
    private string higeScore_key;

    // Start is called before the first frame update
    void Start()
    {
        selectmanager = GameObject.Find("SelectManager");
        SelectManager = selectmanager.GetComponent(typeof(SelectManager)) as SelectManager;
        Canvas = SelectManager.GetCanvas();
        HighScoreText = SelectManager.GetSetHighScoreText;
        high_score = HighScoreText.GetComponent(typeof(Text)) as Text;
        //ハイスコアの設定
        highScore = PlayerPrefs.GetInt(higeScore_key, 0);
    }

    public void OnClickMainButton(){
        if(!SelectManager.GetisStartButton()){
            SelectManager.StartMusic(music);
            var place_start_button = new Vector3(-674f+960f,-260f+540f);
            var place_back_button = new Vector3(-224f+960f,-260f+540f);
            startbutton = Instantiate(StartButton, place_start_button, Quaternion.identity) as GameObject;
            backbutton =  Instantiate(BackButton, place_back_button, Quaternion.identity) as GameObject;
            
            var startbutton_click = startbutton.GetComponent(typeof(ButtonClickObject)) as ButtonClickObject;
            var backbutton_click = backbutton.GetComponent(typeof(ButtonClickObject)) as ButtonClickObject;
            startbutton_click.GetSetSceneName = SceneName;
            backbutton_click.GetSetSceneName = SceneName;

            var canvas_start_button = startbutton.GetComponent(typeof(RectTransform)) as RectTransform;
            var canvas_back_button = backbutton.GetComponent(typeof(RectTransform)) as RectTransform;
            canvas_start_button.anchoredPosition = place_start_button;
            canvas_back_button.anchoredPosition = place_back_button;
            startbutton.transform.SetParent(Canvas);
            backbutton.transform.SetParent(Canvas);

            SelectManager.ChangeisStartButton();

            high_score.text = "HighScore:"+highScore;
            Debug.Log("aaa");
            HighScoreText.SetActive(true);
        }
    }
    public void OnClickStartButton(){
        SelectManager.ChangeisStartButton();
        SceneManager.LoadScene(SceneName);
    }

    public void OnCLickBackButton(){
        SelectManager.ChangeisStartButton();
        for( int i=0; i < Canvas.childCount; ++i ){
                if(Canvas.GetChild(i).gameObject.name == "Back(Clone)" || Canvas.GetChild(i).gameObject.name == "Start(Clone)"){
                    Destroy( Canvas.GetChild(i).gameObject );
                }
        }
        SelectManager.StopMusic();
        HighScoreText.SetActive(false);
    }
}
