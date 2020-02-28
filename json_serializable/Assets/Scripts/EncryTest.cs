/* ***********************************************
* Discribe：
* Author：PeterGao
* CreateTime：2020-02-28 17:55:06
* Edition：1.0
* ************************************************/
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using UnityEngine;

public class EncryTest : MonoBehaviour
{
    public static class IOHelper
    {
        /// <summary>
        /// 判断文件是否存在
        /// </summary>
        /// <param name="fileName"></param>
        /// <returns></returns>
        public static bool IsFileExists(string fileName)
        {
            return File.Exists(fileName);
        }
        /// <summary>
        /// 判断路径是否存在
        /// </summary>
        /// <param name="fileName"></param>
        /// <returns></returns>
        public static bool IsDirectoryExists(string fileName)
        {
            return Directory.Exists(fileName);
        }
        /// <summary>
        /// 创建一个文本文件
        /// </summary>
        /// <param name="fileName"></param>
        /// <param name="content"></param>
        public static void CreateFile(string fileName, string content)
        {
            StreamWriter sw = File.CreateText(fileName);
            sw.Write(content);
            sw.Close();
        }
        /// <summary>
        /// 创建一个文件夹
        /// </summary>
        /// <param name="fileName"></param>
        public static void CreateDirectory(string fileName)
        {
            if (IsDirectoryExists(fileName))
                return;
            Directory.CreateDirectory(fileName);
        }
        // 设置加密实例
        public static void SetData(string fileName, string key, object o)
        {
            //序列化为字符串
            string toSave = JsonUtility.ToJson(o);
            toSave = RijndaelEncrypt(toSave, key);
            StreamWriter sw = File.CreateText(fileName);
            sw.Write(toSave);
            sw.Close();
        }

        //设置解密实例
        public static string GetData(string fileName, string key)
        {
            StreamReader sr = File.OpenText(fileName);
            string data = sr.ReadToEnd();
            //对数据进行解密，32位解密密钥
            data = RijndaelDecrypt(data, key);
            sr.Close();
            return data;
        }


        // Rijndael加密算法
        private static string RijndaelEncrypt(string pString, string pKey)
        {
            byte[] keyArray = UTF8Encoding.UTF8.GetBytes(pKey);//密钥
            byte[] toEncryptArray = UTF8Encoding.UTF8.GetBytes(pString);//待加密明文数组
                                                                        //加密算法
            RijndaelManaged rDel = new RijndaelManaged();
            rDel.Key = keyArray;
            rDel.Mode = CipherMode.ECB;
            rDel.Padding = PaddingMode.PKCS7;
            ICryptoTransform cTransform = rDel.CreateEncryptor();

            //返回加密后的密文
            byte[] resultArray = cTransform.TransformFinalBlock(toEncryptArray, 0, toEncryptArray.Length);
            return Convert.ToBase64String(resultArray, 0, resultArray.Length);
        }

        /// Rijndael解密算法
        public static string RijndaelDecrypt(string pString, string pKey)
        {
            byte[] keyArray = UTF8Encoding.UTF8.GetBytes(pKey);//解密密钥
            byte[] toEncryArray = Convert.FromBase64String(pString);//待解密密文数组


            //Rijndael解密算法
            RijndaelManaged rDel = new RijndaelManaged();
            rDel.Key = keyArray;
            rDel.Mode = CipherMode.ECB;
            rDel.Padding = PaddingMode.PKCS7;
            ICryptoTransform cTransform = rDel.CreateDecryptor();


            //返回解密后的明文
            byte[] resultArray = cTransform.TransformFinalBlock(toEncryArray, 0, toEncryArray.Length);
            return UTF8Encoding.UTF8.GetString(resultArray);
        }

    }



    //测试脚本：


[System.Serializable]//序列化
    public class Student
    {
        public string Name;
        public int ID;
        public Student(string name, int id)
        {
            this.Name = name;
            this.ID = id;
        }
    }
    public class Test : MonoBehaviour
    {
        public string key = "12345678912345678912345678912345";
        string path;

        void Start()
        {
            path = Application.persistentDataPath + "/Save";
            IOHelper.CreateDirectory(path);
            string fileName = path + "/GameData.sav";
            Debug.Log(fileName);
            Student s = new Student("张三", 45);
            IOHelper.SetData(fileName, key, s);
            string message = IOHelper.GetData(fileName, key);
            Student ss = JsonUtility.FromJson<Student>(message);
            Debug.Log(ss.Name + ":" + ss.ID);
        }
    }
}
