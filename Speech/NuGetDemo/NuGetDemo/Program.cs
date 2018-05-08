using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Baidu.Aip.Speech;
using System.IO;
/*
 * VS2013新建MVC5项目，使用nuget更新项目引用后发生Newtonsoft.Json引用冲突的解决办法
 * https://www.cnblogs.com/xwgli/p/3617645.html
 * 
 * vs2013中如何更新NuGet程序包
 * https://blog.csdn.net/jayzai/article/details/51420534
 * 
 * 对于“Newtonsoft.Json”已拥有为“NETStander.Library”定义的依赖项，解决办法
 * https://blog.csdn.net/iimzhouyang/article/details/77929722
 */
namespace NuGetDemo
{
    class SpeechDemo
    {
        private readonly Asr _asrClient;
        private readonly Tts _ttsClient;

        public SpeechDemo()
        {

            _asrClient = new Asr("SajG1SyGU7QeKj07H6yG0UT5", "ba211186e70e2f40000b170cb6f453bf");

            _ttsClient = new Tts("SajG1SyGU7QeKj07H6yG0UT5", "ba211186e70e2f40000b170cb6f453bf");
        }

        // 识别本地文件
        public void AsrData()
        {
            var data = File.ReadAllBytes("语音pcm文件地址");
            var result = _asrClient.Recognize(data, "pcm", 16000);
            Console.Write(result);
        }

        // 识别URL中的语音文件
       /*
        public void AsrUrl()
        {
           
            var result = _asrClient.Recoginze(
                "http://xxx.com/待识别的pcm文件地址",
                "http://xxx.com/识别结果回调地址",
                "pcm",
                16000);
            Console.WriteLine(result);
        }*/

        // 合成
        public void Tts(string text)
        {
            // 可选参数
            var option = new Dictionary<string, object>()
            {
                {"spd", 5}, // 语速
                {"vol", 7}, // 音量
                {"per", 4}  // 发音人，4：情感度丫丫童声
            };
            var result = _ttsClient.Synthesis(text, option);

            if (result.ErrorCode == 0)  // 或 result.Success
            {
                File.WriteAllBytes("合成的语音文件本地存储地址.mp3", result.Data);
            }
        }
        static void Main(string[] args)
        {
            SpeechDemo client = new SpeechDemo();
            client.Tts("人生不满百，常怀千岁忧。穿过我青春所有说慌的日子，现在我可以枯萎而进入真理");
            Console.WriteLine("asjf");
        }

    }
}
