using System.Collections;
using System.Collections.Generic;
using System.IO;
using System;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;
using UnityEngine.SceneManagement;

//CSVデータ保存用のクラス
class CSVData{
    //落ちるライン名を保存する変数
    private string linename;
    //タップかノーツを入れる変数
    private string type;
    //出現時間の変数
    private float startime;
    //ロングノーツの場合その最後の時間を入れる変数
    private float endtime;

    public CSVData(string linename, string type, float startime, float endtime){
        this.linename = linename;
        this.type = type;
        this.startime = startime;
        this.endtime = endtime;
    }

    public string GetLineName(){return this.linename;}
    public string GetType(){return this.type;}
    public float GetStartTime(){return this.startime;}
    public float GetEndTime(){return this.endtime;}
}

public class GameManager : MonoBehaviour
{
    //csvファイル保存用変数
    [SerializeField] private TextAsset csvfile;

    //ブロック生成用ゲームオブジェクト変数
    [SerializeField] private GameObject CreateBlockObject;
    
    //Restartボタン用ゲームオブジェクト変数
    [SerializeField] private GameObject restart_button;
    // private Button restart_button;

    //Exitボタン用ゲームオブジェクト変数
    [SerializeField] private GameObject exit_button;
    // private Button exit_button;

    //Retryボタン用ゲームオブジェクト変数
    [SerializeField] private GameObject retry_button;
    // private Button retry_button;

    //PAUSEボタン用ゲームオブジェクト変数
    [SerializeField] private GameObject pause_button;

    //動画用ゲームオブジェクト変数
    [SerializeField] private GameObject VideoClip;
    private VideoPlayer videoplayer; 

    //ブロック生成オブジェクト
    private CreateBlock createblock;

    //csvファイルの行数管理用変数
    private int rownum;

    //現時刻代入用変数
    private float nowtime = 0f;

    //csvデータ保存用list
    private List<CSVData> CSVDatas = new List<CSVData>();

    //スコア用変数
    private float score = 0f;

    //csvファイルデータの行数変換用変数
    private int rowcount = 0;

    //パーフェクト時のスコアの変数
    private float perfectscore = 1000000f;

    //一ノーツのスコア保存用変数
    private float pernotesscore;

    //Score表示用ゲームオブジェクトとテキストクラス変数
    [SerializeField] private GameObject ScoreText;
    private Text score_text;

    //ゲーム終了時のスコア表示用テキストクラス変数
    [SerializeField] private GameObject FinishScoreText;
    private Text finish_score;

    //ハイスコア用テキストクラス変数
    [SerializeField] private GameObject HighScoreText;
    private Text high_score;

    //動画が再生し終わったあとのカウントダウン用変数
    private float countdown = 1.5f;

    //Notesを保存する親オブジェクトの変数
    [SerializeField] private Transform NotesParent;

    //音楽データを保存する変数
    [SerializeField] private AudioClip music;

    //音楽を操作する変数
    private AudioSource audioSource;

    //停止ボタンか押されてかどうかを識別する変数
    private bool isStop = false;

    //ハイスコア用変数
    private float highScore;

    //ハイスコアが保存されているキー変数
    [SerializeField] private string higeScore_key;

    //最初に再生されたときにTrueに変える変数
    //音楽終了時の判定がこれがないと最初に流すときの判定式とおなじになってしまうので
    private bool isFirstPlay = false;


    void Awake() {
        //ハイスコアの設定
        highScore = PlayerPrefs.GetInt(higeScore_key, 0);
        //CSVファイルからデータを受け取る関数
        GetCSV();
        //メディアの設定
        videoplayer = VideoClip.GetComponent(typeof(VideoPlayer)) as VideoPlayer;
        audioSource = gameObject.GetComponent<AudioSource>();
        audioSource.clip = music;
        videoplayer.Play();
        //ノーツ生成用クラス定義
        createblock = CreateBlockObject.GetComponent(typeof(CreateBlock)) as CreateBlock;
        //スコア表示用の定義
        score_text = ScoreText.GetComponent(typeof(Text)) as Text;
        //ゲーム終了時のスコア表示の設定
        finish_score = FinishScoreText.GetComponent(typeof(Text)) as Text;
        //ハイスコア用の表示設定
        high_score = HighScoreText.GetComponent(typeof(Text)) as Text;
    }
    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale = 1;
        //ボタンは最初は非表示にしておく
        restart_button.SetActive(false);
        exit_button.SetActive(false);
        retry_button.SetActive(false);
        pause_button.SetActive(false);
        FinishScoreText.SetActive(false);
        HighScoreText.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if(!videoplayer.isPlaying && Time.time > 1.0f){
            if(countdown < 0.0f){
                if(!isStop && !audioSource.isPlaying && !isFirstPlay){
                    audioSource.Play();
                    isFirstPlay = true;
                }
                if(rowcount < rownum){
                    if(CSVDatas[rowcount].GetStartTime() < nowtime){
                        createblock.SetBlock(CSVDatas[rowcount].GetLineName(), CSVDatas[rowcount].GetType(), CSVDatas[rowcount].GetStartTime(), CSVDatas[rowcount].GetEndTime());
                        rowcount += 1;
                    }
                }
                nowtime += Time.deltaTime;
                score_text.text = score.ToString("f0");
                if(!isStop && !audioSource.isPlaying && isFirstPlay){
                    FinishGame();
                }
            }else{
                countdown -= Time.deltaTime;
                pause_button.SetActive(true);
            }
        }
    }

    //CSVファイルからデータを受け取る関数
    public void GetCSV(){
        //CSVファイルからのデータを受け取る変数
        StringReader file = new StringReader(csvfile.text);
        //csvファイルの行数管理用変数の初期化
        rownum = 0;
        //CSVファイルのデータを１行ずつみて列ごとに分割してCSVDatasにデータを追加する処理。
        while(file.Peek() != -1){
            string line = file.ReadLine();
            CSVData[] csvdata = new CSVData[]{new CSVData(line.Split(',')[0],line.Split(',')[1],(float.Parse(line.Split(',')[2])-1.0f),(float.Parse(line.Split(',')[3]))-1.0f)};
            CSVDatas.AddRange(csvdata);
            rownum = rownum + 1;
        }
        //出現時間をソートする処理
        var c = new Comparison<CSVData>(Compare);
        CSVDatas.Sort(c);
    }

    //２つのCSVDataのstarttimeを比較する処理
    static int Compare(CSVData a, CSVData b){
        if(a.GetStartTime() > b.GetStartTime()){
            return 1;
        }else if(a.GetStartTime() == b.GetStartTime()){
            return 0;
        }else{
            return -1;
        }
    }

    //スコア計算関数
    public void SetScore(string jugde){
        pernotesscore = (perfectscore / (float)rowcount);
        Debug.Log(jugde);
        if(jugde == "perfect"){
            score += pernotesscore;
        }
        if(jugde == "great"){
            score += pernotesscore * 0.9f;
        }
        if(jugde == "bad"){
            score += pernotesscore * 0.7f;
        }
        if(jugde == "miss"){
            score += 0.0f;
        }
    }

    public void OnClickPause(){
        Time.timeScale = 0;
        audioSource.Pause();
        isStop = true;
        restart_button.SetActive(true);
        exit_button.SetActive(true);
        retry_button.SetActive(true);
        pause_button.SetActive(false);
    }

    public void OnClickRetryButton(){
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void OnClickRestartButton(){
        Time.timeScale = 1;
        isStop = false;
        audioSource.UnPause();
        restart_button.SetActive(false);
        exit_button.SetActive(false);
        retry_button.SetActive(false);
        pause_button.SetActive(true);
    }

    public void OnClickExitButton(){
        SceneManager.LoadScene("SelectScene");
    }

    private void FinishGame(){
        ScoreText.SetActive(false);
        if(score > highScore){
            highScore = score;
            PlayerPrefs.SetInt(higeScore_key, (int)Math.Floor(highScore));
            HighScoreText.SetActive(true);
        }
        finish_score.text = "Score:"+score.ToString("f0");
        FinishScoreText.SetActive(true);
    }
}
