存档Json(LitJson)

private void SaveByJson()
{
　　Save save = CreateSaveGO();
　　path = Application.dataPath + “/StreamingFile” + “/byJson.json”;
　　//利用JsonMapper将save对象转换为Json格式的字符串
　　string saveJsonStr = JsonMapper.ToJson(save);
　　//将这个字符串写入到文件中
　　//创建一个StreamWriter，并将字符串写入
　　StreamWriter sw = new StreamWriter(path);
　　sw.Write(saveJsonStr);
　　//关闭写入流
　　sw.Close();
　　AssetDatabase.Refresh();
}

读档

private void LoadByJson()
{
　　path = Application.dataPath + “/StreamingFile” + “/byJson.json”;
　　//创建一个StreamReader，用来读取流
　　StreamReader sr = new StreamReader(path);
　　//将读取到的流赋值给saveJsonStr
　　string saveJsonStr = sr.ReadToEnd();
　　sr.Close();
　　//将字符串转换为Save对象
　　Save save = JsonMapper.ToObject(saveJsonStr);
　　SetGame(save);
}


LitJson输出中文有问题怎么破？变成来Unicode形式的

一个解决方案正则转码。可以试试写个通用的Log函数

https://blog.csdn.net/qq_42672770/article/details/104463074


