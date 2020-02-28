/* ***********************************************
* Discribe：
* Author：PeterGao
* CreateTime：2020-02-28 18:53:32
* Edition：1.0
* ************************************************/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Security.Cryptography;
using System.Text;
using System;

public class Encrypt : MonoBehaviour
{
    // Rijndael加密算法
        public static string RijndaelEncrypt(string pString, string pKey)
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
