/* ***********************************************
* Discribe：
* Author：PeterGao
* CreateTime：2020-02-26 09:34:51
* Edition：1.0
* ************************************************/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 定义道具类型枚举
public enum Type
{
    Weapon,
    Shoe
}

// 物品道具基类
public class Item
{
    public int id;
    public string name;
    public Type type;

    public Item()
    {

    }

    public Item(string n, Type t)
    {
        this.type = t;
        this.name = n;
    }
}