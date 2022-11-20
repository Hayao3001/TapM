using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicDataManager : MonoBehaviour
{
    //使用するデータベースを入れる変数
    [SerializeField] private MusicDataBase musicdatabase; 

    //データベース内の要素数を取得する処理
    public int GetMusicDataBaseCount(){
        return musicdatabase.GetMusicDataList().Count;
    }

    //データベース内のデータをすべて取得する。
    public List<MusicData> GetMusicDataList(){
        var list = new List<MusicData>();
        for(int i=0;i<GetMusicDataBaseCount();i++){
            list.Add(musicdatabase.GetMusicDataList()[i]);
        }
        return list;
    }
}
