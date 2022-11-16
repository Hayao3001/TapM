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

    //斜めの実装は様子見
    //斜めのラインのブロック生成
    // [SerializeField] private GameObject diagonalblock_right_up;
    // [SerializeField] private GameObject diagonalblock_right_down;
    // [SerializeField] private GameObject diagonalblock_left_up;
    // [SerializeField] private GameObject diagonalblock_left_down;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
