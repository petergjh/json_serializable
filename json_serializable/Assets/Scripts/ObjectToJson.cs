/* ***********************************************
* Discribe：
* Author：PeterGao
* CreateTime：2020-02-23 18:35:07
* Edition：1.0
* ************************************************/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LitJson;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PetColor
{
    public int red;
    public int green;
    public int blue;

}

public class Pet
{
    public string name;
    public int age;
    public PetColor color;

    public void Bark()
    {

    }
}

public class ObjectToJson: MonoBehaviour
{
 
    private object myPet;
    private object myPetTom;
    private object myArray;
    private object myArray2;
    private object myList;
    private int myStr;
    private string mySave3;
    private string myArray4;
    private object save1;
    private object save2;
    private object save3;

    // Start is called before the first frame update
    void Start()
    {
        myPet = JsonMapper.ToJson(new Pet());
        Debug.LogFormat("测试将class实例转换为Json格式：{0},实例方法和未公开的属性不会被转化", myPet);

        var petTom = new Pet();
        petTom.name = "Tom";
        petTom.age = 12;
        petTom.color = new PetColor();
        myPetTom = JsonMapper.ToJson(petTom);
        Debug.LogFormat("测试嵌套实例转换为Json格式：{0}", myPetTom);


        var array = new string[][]
        {
            new string[]{"bar", "foo" },
            new string[]{"baz"}
        };
        myArray = JsonMapper.ToJson(array);
        Debug.LogFormat("测试" + "<color=red>" + "交错数组" + "</color>" + "转换为Json格式：{0}", myArray);

        var array2 = new int[,]
        {
            { 1,2,3 },
            { 55,56,57 }
        };
        myArray2 = JsonMapper.ToJson(array2);
        Debug.LogFormat("测试二维数组转换为Json格式：{0}", myArray2);

        var list = new List<bool>(new bool[] { true, false });
        myList = JsonMapper.ToJson(list);
        Debug.LogFormat("测试List列表转换为Json格式：{0}", myList);

        var b = "{ 'name': 'Jerry', 'age': 3 }";
        var myPetJerry = JsonMapper.ToObject<Pet>(b);
        Debug.LogFormat("测试把Json字符串转为特定类的实例：{0}, {1}, {2}", myPetJerry, myPetJerry.name, myPetJerry.age);


        string arrayStr = "[1,2,3,11,12,13]";
        int myStr = JsonMapper.ToObject<int[]>(arrayStr)[3];
        Debug.LogFormat("测试用泛型把Json转为字符串: {0}", myStr);


//        使用泛型时 ToObject 的参数只能为字符串形式，而不能以JsonData 类型的变量作为参数
//        不能将 List<int[]> 转换为交错数组
//        var arrayStr3 = "['foo', 'baz'], ['bar']]";
//        var myArray3 = JsonMapper.ToObject<string[][]>(arrayStr3);
//        myArray4 = myArray3[1][0];
//        Debug.LogFormat("测试用泛型把Json转为字符串{0}", myArray4);

        var save = new JsonData();
        save["name"] = "Third";
        save["level"] = 3;
        var mySave = save.ToJson();
        Debug.LogFormat("测试JsonData字典实例{0}", mySave);


        var SaveAll = new JsonData();
        SaveAll.Add(save1);
        SaveAll.Add(save2);
        SaveAll.Add(save3);

        var MySaveAll = SaveAll.ToJson();
        Debug.LogFormat("测试Json数组实例{0}", MySaveAll);




    }



    
    private void OnGUI()
    {
        if (GUILayout.Button("Turn to Scene2"))
        {
            // 切换场景
            SceneManager.LoadScene("Scene2");
            Debug.Log("切换到场景2");
        }
    }
}


