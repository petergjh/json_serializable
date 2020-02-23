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

public class ClassToJson : MonoBehaviour
{

    private object myPet;

    // Start is called before the first frame update
    void Start()
    {
        myPet = JsonMapper.ToJson(new Pet());
        Debug.Log(""myPet);

    }
}

public class Pet
{
    public string name;
    public int age;
    public string color;

    public void Bark()
    {

    }
}
