using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization.Json;
using System.IO;

namespace JSONCS
{
    public static class JSON
    {
        /// <summary>
        /// 将json string转换成C# object
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="jsonString"></param>
        /// <returns></returns>
        public static T Parse<T>(string jsonString)
        {
            using (var ms = new MemoryStream(Encoding.UTF8.GetBytes(jsonString)))
            {
                return (T)new DataContractJsonSerializer(typeof(T)).ReadObject(ms);
            }
        }
        /// <summary>
        /// 将 C#object 转换成 string
        /// </summary>
        /// <param name="jsonObject"></param>
        /// <returns></returns>
        public static string Parse(object jsonObject)
        {
            using (var ms = new MemoryStream())
            {
                new DataContractJsonSerializer(jsonObject.GetType()).WriteObject(ms, jsonObject);
                return Encoding.UTF8.GetString(ms.ToArray());
            }
        }
        /// <summary>
        /// 将对象o转换成包含json数据的文件
        /// </summary>
        /// <param name="FileName"></param>
        /// <param name="o"></param>
        public static void CreateJSONFile(string FileName, object o)
        {
            try
            {
                FileStream fs = new FileStream(FileName, FileMode.Create);
                BinaryWriter bw = new BinaryWriter(fs);
                bw.Write(Parse(o));
                fs.Close();
                bw.Close();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
        }
        /// <summary>
        /// 将可能包含json数据的文件中的字符串读出
        /// </summary>
        /// <param name="FileName"></param>
        /// <returns></returns>
        public static string ReadJSONFile(string FileName)
        {
            
            try
            {
                if (File.Exists(FileName))
                {
                    FileStream fs = new FileStream(FileName, FileMode.Open, FileAccess.Read);
                    BinaryReader br = new BinaryReader(fs);
                    return br.ReadString();
                }
                else
                    return "no this file";
            }
            catch(Exception e)
            {
                return e.ToString();
            }
        }
    }
}
