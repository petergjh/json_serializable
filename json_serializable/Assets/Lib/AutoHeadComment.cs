/* ***********************************************
* Discribe：
* Author：PeterGao
* CreateTime：2020-02-23 18:39:49
* Edition：1.0
* ************************************************/
/* ***********************************************
* Discribe：
* Author：PeterGao
* CreateTime：2020-02-23 18:37:04
* Edition：1.0
* ************************************************/
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;


namespace _Editor
{
    public class AutoHeadComment : UnityEditor.AssetModificationProcessor
    {
        /// <summary>
        /// 创建脚本时调用
        /// </summary>
        /// <param name="path">自动生成的脚本路径</param>
        public static void OnWillCreateAsset(string path)
        {
            path = path.Replace(".meta", "");
            if (!path.EndsWith(".cs")) return;
            string CommentContent = "/* ***********************************************\r\n"
                             + "* Discribe：\r\n"
                             + "* Author：PeterGao\r\n"
                             + "* CreateTime：2020-02-23 18:39:49\r\n"
                             + "* Edition：1.0\r\n"
                             + "* ***********************************************"
                             + "*/\r\n";
            CommentContent += File.ReadAllText(path);
            CommentContent = CommentContent.Replace("2020-02-23 18:39:49", System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
            File.WriteAllText(path, CommentContent);
        }
    }
}


