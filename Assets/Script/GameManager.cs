using System.Collections;
using System.Collections.Generic;
using System.IO;
using System;
using UnityEngine;

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

    //csvファイルの行数管理用変数
    private int rownum;

    //csvデータ保存用list
    private List<CSVData> CSVDatas = new List<CSVData>();
    // Start is called before the first frame update
    void Start()
    {
        //CSVファイルからデータを受け取る関数
        GetCSV();
    }

    // Update is called once per frame
    void Update()
    {
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
            CSVData[] csvdata = new CSVData[]{new CSVData(line.Split(',')[0],line.Split(',')[1],float.Parse(line.Split(',')[2]),float.Parse(line.Split(',')[3]))};
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
}
