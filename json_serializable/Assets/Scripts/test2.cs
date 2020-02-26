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

// 存档脚本
public class Test2 : MonoBehaviour
{
    Data data = new Data();  // 声明数据对象的实例
    public Text[] Name;
    public Text itemtext;
    public Button[] Up;

    // Start is called before the first frame update
    void Start()
    {
        Init();
    }

    // UI显示
    void Init()
    {
        Name[0].text = "等级: " + data.level.ToString();  // 显示等级
        Name[1].text = "金币: " + data.money.ToString();

        Up[0].onClick.AddListener(() => { data.level++; }); // 点击加1
        Up[1].onClick.AddListener(() => { data.money++; });

        string itemname = "";  // 显示道具名称
        foreach (var item in data.bagitems)
        {
            itemname += item.name + " ";
        }
        itemtext.text = itemname;


    }

    // UI刷新
    private void Update()
    {
        Name[0].text = "等级: " + data.level.ToString();
        Name[1].text = "金币: " + data.money.ToString();

        string itemname = "";
        foreach (var item in data.bagitems)
        {
            itemname += item.name + " ";
        }
        itemtext.text = itemname;


    }

    // 点击后向背包列表增加一个物品
    public void GetItem()
    {
        data.bagitems.Add(new Item("长剑", Type.Weapon));
    }

    private void OnDestroy()
    {
        SaveData();
    }

    // 数据存档
    public void SaveData()
    {

        // 数据转换成json字符串
        string json = JsonMapper.ToJson(data);

        if (!File.Exists("data.json"))
        {
            File.Create("data.json").Close();
            Debug.Log("Create data.json");
        }

        // 将Json字符串以文件流的方式写入并覆盖原Json文件
        using (StreamWriter sw= new StreamWriter(new FileStream("data.json", FileMode.Truncate)))
        {
            sw.Write(json);
            sw.Close();
            Debug.Log("Writen data to Json File successfully !");
        }
        
    }

    //  加载存档
    public void LoadData()
    {
        if (!File.Exists("data.json"))
        {
            Debug.Log("No File found !");
            return;
        }

        // 将存档文件以文件流的方式读取并转换成json字符串，再转换成数据对象

        using (StreamReader sr = new StreamReader(new FileStream("data.json", FileMode.Open)))
        {
            string json = sr.ReadLine();
            data = JsonMapper.ToObject<Data>(json);
            sr.Close();
            Debug.Log("Load data successfully ! ");

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