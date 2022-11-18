using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateBlock : MonoBehaviour
{
    //下のラインのブロック生成
    [SerializeField] private GameObject bottomblock;

    //右のラインのブロック生成
    [SerializeField] private GameObject rightblcok;
    
    //上のラインのブロック生成
    [SerializeField] private GameObject topblock;

    //左のラインのブロック生成
    [SerializeField] private GameObject leftblock;

    //ノーツの画像
    [SerializeField] private GameObject NotesImage;

    //斜めの実装は様子見
    //斜めのラインのブロック生成
    // [SerializeField] private GameObject diagonalblock_right_up;
    // [SerializeField] private GameObject diagonalblock_right_down;
    // [SerializeField] private GameObject diagonalblock_left_up;
    // [SerializeField] private GameObject diagonalblock_left_down;

    //右上の座標
    [SerializeField] private float right_up_x;
    [SerializeField] private float right_up_y;

    //右下の座標
    [SerializeField] private float right_down_x;
    [SerializeField] private float right_down_y;


    //左上の座標
    [SerializeField] private float left_up_x;
    [SerializeField] private float left_up_y;

    //左下の座標
    [SerializeField] private float left_down_x;
    [SerializeField] private float left_down_y;

    public void Start(){
        SetBlock("topline2");
    }

    //ノーツの配置
    //引数はどのラインに落とすかのライン名
    public void SetBlock(string linename)
    {

        //ライン名一覧
        //斜め方向の実装は様子見
        //"diagonal1","diagonal2","diagona3","diagonal4"
        string[] Line = new string[]
        {
            "bottomline1","bottomline2","bottomline3","bottomline4","bottomline5", "rightline1","rightline2","rightline3", "rightline4",
            "topline1","topline2","topline3","topline4","topline5", "leftline1","leftline2","leftline3","leftline4",
        };

        //ライン番号
        int linenum = 0;
        //ライン分割時のライン番号
        int perlinenum;
        //それぞれのラインの距離
        float linelength;
        //配置する座標
        Vector3 place;
        //ノーツの長さ
        float block_length;
        //変更される座標(上下ならx座標、右左ならy座標)
        float place_x_y;
        //ノーツの長さの基準値
        float length_basic = 0.9f;
        //ノーツ配置用GameObject
        GameObject notes;
        //オブジェクトの大きさを代入するVector3
        Vector3 size;
        //NoteImageの横幅
        float width = NotesImage.GetComponent<SpriteRenderer>().bounds.size.x;

        

        //ライン名をわかりやすく番号に
        for(int i=0;i<Line.Length;i++){
            if(Line[i] == linename){
                linenum = i + 1;
            }
        }

        //下ラインへの配置
        //5分割
        if((1 <= linenum) && (linenum <= 5)){
            perlinenum = linenum;
            
            linelength = right_down_x - left_down_x;
            //５分割します。
            place_x_y = left_down_x + ((linelength)/(float)5)*(0.5f+(float)(perlinenum-1));

            //設置場所の指定
            place = new Vector3(place_x_y,0f,0f);

            //ノーツの長さ指定
            block_length = ((linelength)/(float)5) * length_basic;

            //ノーツの配置
            notes = Instantiate(bottomblock,place, Quaternion.identity);

            //サイズ変更部分
            size = notes.transform.localScale;
            size.x = block_length / width;
            notes.transform.localScale = size;
        }
        
        //右ラインへの配置
        //4分割
        if((6 <= linenum) && (linenum <= 9)){
            perlinenum = linenum - 5;

            linelength = right_up_y - right_down_y;

            //4分割します。
            place_x_y = right_up_y - ((linelength)/(float)4)*(0.5f+(float)(perlinenum-1));

            //設置場所の指定
            place = new Vector3(0f,place_x_y,0f);

            //ノーツの長さ指定
            block_length = ((linelength)/(float)4) * length_basic;

            //ノーツの配置
            notes = Instantiate(rightblcok,place, Quaternion.Euler(0f, 0f, 90f));

            //サイズ変更部分
            size = notes.transform.localScale;
            size.x = block_length / width;
            notes.transform.localScale = size;
        }

        //上ラインへの配置
        //5分割
        if((10 <= linenum) && (linenum <= 14)){   
            perlinenum = linenum - 9;
            linelength = right_up_x - left_up_x;

            //５分割します。
            place_x_y = left_up_x + ((linelength)/(float)5)*(0.5f+(float)(perlinenum-1));

            //設置場所の指定
            place = new Vector3(place_x_y,0f,0f);

            //ノーツの長さ指定
            block_length = ((linelength)/(float)5) * length_basic;

            //ノーツの配置
            notes = Instantiate(topblock,place, Quaternion.identity);

            //サイズ変更部分
            size = notes.transform.localScale;
            size.x = block_length / width;
            notes.transform.localScale = size;
        }

        //左ラインへの配置
        //4分割
        if((15 <= linenum) && (linenum <= 18)){
            perlinenum = linenum - 14;
            linelength = left_up_y - left_down_y;

            //4分割します。
            place_x_y = left_up_y - ((linelength)/(float)4)*(0.5f+(float)(perlinenum-1));

            //設置場所の指定
            place = new Vector3(0f,place_x_y,0f);

            //ノーツの長さ指定
            block_length = ((linelength)/(float)4) * length_basic;

            //ノーツの配置
            notes = Instantiate(leftblock,place, Quaternion.Euler(0f, 0f, 90f));

            //サイズ変更部分
            size = notes.transform.localScale;
            size.x = block_length / width;
            notes.transform.localScale = size;
        }
    }

}
