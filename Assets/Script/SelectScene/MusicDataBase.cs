using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
//楽曲データのクラス。
public class MusicData {
    public string name;
    public AudioClip music;
    public string SceneName;
    public string HighScore_Key;


    public string GetMusicName(){
        return this.name;
    }

    public AudioClip GetMusic(){
        return this.music;
    }
    public string GetSceneName(){
        return this.SceneName;
    }
    public string GetHighScore_Key(){
        return this.HighScore_Key;
    }
}

[CreateAssetMenu(fileName="MusicDataBase", menuName="DataBase/music")]
public class MusicDataBase : ScriptableObject {
    public List<MusicData> musicdatabase = new List<MusicData>();

    public List<MusicData> GetMusicDataList(){
        return this.musicdatabase;
    }
}