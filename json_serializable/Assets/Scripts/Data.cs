/* ***********************************************
* Discribe：
* Author：PeterGao
* CreateTime：2020-02-25 16:11:48
* Edition：1.0
* ************************************************/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 游戏数据基类
public class Data
{
    public int level;
    public int money;
    public List<Item> bagitems = new List<Item>(); // 物品背包
}
