using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;

using System.Speech.Synthesis;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Converters;//IsoDateTimeConverter   mmp，我怎么知道要using 这三个？ 没有自动导包吗？？
/*1.C#模拟POST提交表单（一）--WebClient
 * https://blog.csdn.net/xizhibei/article/details/6972116
 * 2.C# 解析JSON格式数据
 * https://blog.csdn.net/coolszy/article/details/8606803
 * (⊙o⊙)… 原来百度API那会就用过
 * 3.C#播放声音【六种方法】
 * https://blog.csdn.net/wangzhen209/article/details/53285651
 */
class Program
{
    static void Main(string[] args)
    {
        WebClient webClient = new WebClient();
        string url = @"http://www.tuling123.com/openapi/api";
        string postPreString = "key=e904088025004ef4af76481e91b7d1da&info=";
        SpeechSynthesizer speaker = new SpeechSynthesizer();
        Console.WriteLine("你好啊，欢迎和我李欣墨聊天,输入 # 退出哦");
        string req = Console.ReadLine();
        while (!req.Equals("#"))
        {
            webClient.Headers.Add("Content-Type", "application/x-www-form-urlencoded");//采取POST方式必须加的header，如果改为GET方式的话就去掉这句话即可  
            string postString = postPreString + req;
            byte[] postData = Encoding.UTF8.GetBytes(postString);//编码，
            byte[] responseData = webClient.UploadData(url, "POST", postData);//得到返回字符流  
            string srcString = Encoding.UTF8.GetString(responseData);//解码  

            JObject jo = JObject.Parse(srcString);
            string response = jo["text"].ToString();
            speaker.Speak(response);  //播放指定的字符串,这是异步朗读

            Console.WriteLine(response + "   \t" + DateTime.Now.ToString());
            req = Console.ReadLine();
        }

        speaker.Dispose();  //释放所有语音资源

    }

}
