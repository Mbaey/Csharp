using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
/*参考网页。。
 * 1.添加dll
 * https://zhidao.baidu.com/question/147312745.html
 * 2.无法直接启动带有"类库输出类型"
 * https://blog.csdn.net/u010349629/article/details/77509045
 */

class Program
    {
        static void Main(string[] args)
        {
             SpeechDemo client=   new SpeechDemo();
             client.Tts("众里寻他千百度，那人却在灯火阑珊处");
             Console.WriteLine("asjf");
        }
    }

