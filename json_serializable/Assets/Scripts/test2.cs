/* ***********************************************
* Discribe：
* Author：PeterGao
* CreateTime：2020-02-25 08:36:50
* Edition：1.0
* ************************************************/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using LitJson;
using System.IO;
using UnityEngine.SceneManagement;
using System.Security.Cryptography;
using System;



// 存档脚本
public class Test2 : MonoBehaviour
{
    Data GameData = new Data();  // 声明数据对象的实例
    public static string SaveName = "Save0"; //  定义一个存档名称的全局变量

    public Text[] Name;
    public Text itemtext;
    public Button[] Up;
    private readonly string key = "12345678912345678912345678912345";
    private string SavePath;


    // Start is called before the first frame update
    void Start()
    {
        Init();
    }

    // UI显示
    void Init()
    {
        Name[0].text = "等级: " + GameData.level.ToString();  // 显示等级
        Name[1].text = "金币: " + GameData.money.ToString();

        Up[0].onClick.AddListener(() => { GameData.level++; }); // 点击加1
        Up[1].onClick.AddListener(() => { GameData.money++; });

        string itemname = "";  // 显示道具名称
        foreach (var item in GameData.bagitems)
        {
            itemname += item.name + " ";
        }
        itemtext.text = itemname;

//        if (!File.Exists("GameAllData.json"))
//        {
//            File.Create("GameAllData.json").Close();
//            Debug.Log("Creat Save File.");
//        }
//


    }

    // UI刷新
    private void Update()
    {
        Name[0].text = "等级: " + GameData.level.ToString();
        Name[1].text = "金币: " + GameData.money.ToString();

        string itemname = "";
        foreach (var item in GameData.bagitems)
        {
            itemname += item.name + " ";
        }
        itemtext.text = itemname;


    }

    // 点击后向背包列表增加一个物品
    public void GetItem()
    {
        GameData.bagitems.Add(new Item("长剑", Type.Weapon));
    }


    // 对象禁用时发起存档
    private void OnDisable()
    {
        Debug.Log("对象禁用时发起存档");
        SaveData();
    }

    // 存档一
    public void LoadSave1()
    {
        SaveName = "Save1";
        LoadData();
    }

    // 存档二
    public void LoadSave2()
    {
        SaveName = "Save2";
        LoadData();
    }

    // 存档三
    public void LoadSave3()
    {
        SaveName = "Save3";
        LoadData();
    }

    // 数据存档
    // public void SaveData()
    // string SavePath, string key, object GameData
      public void SaveData()
      {
        SavePath = SaveName.ToString() + ".json";
        if (!File.Exists(SavePath))
        {
            File.Create(SavePath).Close();
            Debug.LogFormat("创建存档: {0}", SavePath);
        }

        // 数据转换成json字符串
        string JsonStr = JsonMapper.ToJson(GameData);
        Debug.LogFormat("游戏数据转存成Json字符串:{0}", JsonStr);
        JsonStr = Encrypt.RijndaelEncrypt(JsonStr, key);
        Debug.Log("加密Json字符串");

        // 将Json字符串以文件流的方式写入并覆盖原Json文件
            using (StreamWriter sw = new StreamWriter(new FileStream(SavePath, FileMode.Truncate)))
            {
                sw.Write(JsonStr);
                sw.Close();
                Debug.LogFormat("游戏数据已存档到: {0}", SavePath);
            }
      }


    //  加载存档
    public void LoadData()
    {
        string SavePath = SaveName.ToString() + ".json";

        if (!File.Exists(SavePath))
        {
            // File.Create(SavePath).Close();
            // Debug.LogFormat("创建存档：{0}", SavePath);
            SaveData();
        }

        // 将存档文件以文件流的方式读取并转换成json字符串，再转换成数据对象
        using (StreamReader sr = new StreamReader(new FileStream(SavePath, FileMode.Open)))
        {
            string json = sr.ReadLine();
            json = Encrypt.RijndaelDecrypt(json, key);
            GameData = JsonMapper.ToObject<Data>(json);
            sr.Close();
            Debug.Log("GameData Loaded! ");

        }
    }


    // 切换场景
    private void OnGUI()
    {
        //if (GUI.Button(new Rect(125, -125, 200, 60), "Turn to Scene1"))
        if (GUILayout.Button("Turn to Scene1"))
        {
            SceneManager.LoadScene("Scene1");
            Debug.Log("切换到场景1");
        }
    }
}