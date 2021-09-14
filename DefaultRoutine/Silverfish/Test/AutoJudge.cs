using System;
using System.IO;
using System.Text;
using System.Security.Cryptography;
using System.Collections.Generic;
using System.Linq;

namespace HREngine.Bots
{
    public class AutoJudge : TestBase
    {
        private static string mainPath = @"D:\HB_分享版\Silverfish\Routines\DefaultRoutine\Silverfish\"; // 确保此路径正确
        //private static string testPath = @"Test\Data\鱼人萨\测试集1\"; // 需要测试的文件夹
        //private static string testPath = @"Test\Data\鱼人萨\"; // 需要测试的文件夹
        //private static string testPath = @"Test\Data\鱼人萨\计算时间过长\"; // 需要测试的文件夹

        private static string testPath = @"Test\Data\暗牧\"; // 需要测试的文件夹 
        // private static string testPath = @"Test\Data\暗牧\猜构筑\"; // 需要测试的文件夹 
        // private static string testPath = @"Test\Data\海盗战\"; // 需要测试的文件夹 
        // private static string testPath = @"Test\Data\口德\"; // 需要测试的文件夹 

        // private static string testPath = @"Test\Data\黑眼任务术\"; // 需要测试的文件夹 
        // private static string testPath = @"Test\Data\黑眼任务术\常错\"; // 需要测试的文件夹 
        // private static string testPath = @"Test\Data\黑眼任务术\1\"; // 需要测试的文件夹
        // private static string testPath = @"Test\Data\黑眼任务术\索子哥顶级理解\"; // 需要测试的文件夹
        // private static string testPath = @"Test\Data\骑士\"; // 需要测试的文件夹
        // private static string testPath = @"Test\Data\骑士\1\"; // 需要测试的文件夹

        //private static string testPath = @"Test\Data\亡语瞎\1\"; // 需要测试的文件夹
        //private static string testPath = @"Test\Data\亡语瞎\"; // 需要测试的文件夹

        //private static string testPath = @"Test\Data\标准骑士\"; // 需要测试的文件夹
        //private static string testPath = @"Test\Data\标准骑士\测试集1\"; // 需要测试的文件夹

        //private static string testPath = @"Test\Data\动物园\1\"; // 需要测试的文件夹
        //private static string testPath = @"Test\Data\动物园\"; // 需要测试的文件夹
        //private static string testPath = @"Test\Data\T7\"; // 需要测试的文件夹
        //private static string testPath = @"Test\Data\T7\1\"; // 需要测试的文件夹

        //private static string testPath = @"Test\Data\标准T7\1\"; // 需要测试的文件夹
        //private static string testPath = @"Test\Data\标准T7\"; // 需要测试的文件夹

        // private static string testPath = @"Test\Data\咆哮德\"; // 需要测试的文件夹
        //private static string testPath = @"Test\Data\咆哮德\1\"; // 需要测试的文件夹

        //private static string testPath = @"Test\Data\小丑德\"; // 需要测试的文件夹
        //private static string testPath = @"Test\Data\防战\"; // 需要测试的文件夹
        //private static string testPath = @"Test\Data\防战\问题集1\"; // 需要测试的文件夹

        //private static string testPath = @"Test\Data\巨化德\"; // 需要测试的文件夹
        //private static string testPath = @"Test\Data\巨化德\问题集1\"; // 需要测试的文件夹
        //private static string testPath = @"Test\Data\巨化德\问题集2\"; // 需要测试的文件夹
        //private static string testPath = @"Test\Data\巨化德\1\"; // 需要测试的文件夹


        // private static string testFile = @"裂心叫杀.txt"; // 如果只需要测试单个文件，设置该属性为对应文件名
        private static string testFile = null; // 如果只需要测试单个文件，设置该属性为对应文件名

        private static Ai ai = null;

        public void Test(string data)
        {
            InitSetting();
            if (ai == null)
            {
                ai = Ai.Instance;
                //ai.botBase = new Behavior狂野鱼人萨();  //根据卡组选择合适的策略
                // ai.botBase = new Behavior黑眼任务术();  //根据卡组选择合适的策略
                ai.botBase = new Behavior暗牧();  //根据卡组选择合适的策略
                // ai.botBase = new Behavior任务海盗战();  //根据卡组选择合适的策略
                // ai.botBase = new Behavior口德();  //根据卡组选择合适的策略
                //ai.botBase = new Behavior巨化德();  //根据卡组选择合适的策略
                // ai.botBase = new Behavior骑士();  //根据卡组选择合适的策略
                //ai.botBase = new Behavior亡语瞎();  //根据卡组选择合适的策略
                //ai.botBase = new Behavior经典T7猎();  //根据卡组选择合适的策略
                //ai.botBase = new Behavior标准T7猎();  //根据卡组选择合适的策略
                // ai.botBase = new Behavior咆哮德();  //根据卡组选择合适的策略
                //ai.botBase = new Behavior动物园();  //根据卡组选择合适的策略
                //ai.botBase = new Behavior小丑德();  //根据卡组选择合适的策略
                //ai.botBase = new Behavior防战();  //根据卡组选择合适的策略
                //ai.botBase = new BehaviorTest();  //根据卡组选择合适的策略
            }
            ai.autoTester(true, data, 2);// 0：全做 1:只斩杀 2：正常
        }

        public static void Main(string[] args)
        {
            //初始化
            Settings.Instance.test = true;
            Settings.Instance.mainPath = mainPath;
            Settings.Instance.logpath = Path.Combine(mainPath, @"Test\Data\");
            Settings.Instance.path = Path.Combine(mainPath, @"data\"); // 用于CardDB类构造，读取CardDefs.xml

            AutoJudge test = new AutoJudge();

            DirectoryInfo theFolder = new DirectoryInfo(mainPath + testPath);
            FileInfo[] fileInfo = theFolder.GetFiles();
            Console.WriteLine("初始化兄弟，请稍等......");
            System.DateTime startTime, endTime;
            startTime = System.DateTime.Now;

            Array.Sort(fileInfo, (FileInfo x, FileInfo y) => x.Name.CompareTo(y.Name));

            foreach (FileInfo NextFile in fileInfo)
            {
                string fileName = NextFile.Name;
                if (testFile != null && !fileName.Equals(testFile)) continue;  //testFile不为空，则是单文件测试
                if (fileName.Length <= 3) continue;
                string fileType = fileName.Substring(fileName.Length - 3);
                if ("txt".Equals(fileType) && !fileName.Equals("Logg.txt"))
                {
                    var testFilePath = mainPath + testPath + fileName;
                    var outputFilePath = mainPath + testPath + fileName.Substring(0, fileName.Length - 4) + ".result";
                    var answerFilePath = mainPath + testPath + fileName.Substring(0, fileName.Length - 4) + ".answer";
                    var detailFilePath = mainPath + testPath + fileName.Substring(0, fileName.Length - 4) + ".detail";
                    printUtils.outputFilePath = outputFilePath;
                    printUtils.detailFilePath = detailFilePath;

                    if (File.Exists(outputFilePath))
                    {
                        //删除文件
                        File.Delete(outputFilePath);
                    }
                    if (File.Exists(detailFilePath))
                    {
                        //删除文件
                        File.Delete(detailFilePath);
                    }
                    string data = File.ReadAllText(testFilePath);
                    test.Test(data);
                    endTime = System.DateTime.Now;
                    double runTime = (endTime - startTime).TotalSeconds;
                    string runTimes = "-------计算耗时：" + runTime;
                    startTime = System.DateTime.Now;
                    switch (isValidFileContent(outputFilePath, answerFilePath))
                    {
                        case "文件不存在":
                            Console.WriteLine("####测试用例 " + fileName.Substring(0, fileName.Length - 4) + " 未设置参考打法!" + runTimes);
                            break;
                        case "true":
                            Console.WriteLine("测试用例 " + fileName.Substring(0, fileName.Length - 4) + " 通过!" + runTimes);
                            break;
                        case "false":
                            Console.WriteLine("##########测试用例 " + fileName.Substring(0, fileName.Length - 4) + " 与参考打法不符合，请检查或更新结果！#########" + runTimes);
                            break;
                    }
                }
            }
            Console.WriteLine("全部测试完毕，请去.detail文件查看Ai具体计算过程");
            // Console.WriteLine("按回车退出");
            // Console.ReadLine();
        }

        public static string isValidFileContent(string filePath1, string filePath2)
        {
            if (!File.Exists(filePath1) || !File.Exists(filePath2))
            {
                return "文件不存在";
            }
            string[] ls1 = File.ReadAllLines(filePath1);
            string[] ls2 = File.ReadAllLines(filePath2);
            if (ls1.Length != ls2.Length)
                return "false";
            int len = ls1.Length;
            bool start_compare = false;
            for(int i = 0; i < len; i++)
            {

                //if (ls1[i].StartsWith("simulate："))
                if (ls1[i].StartsWith("最终场面："))
                {
                    start_compare = true;
                    continue;
                }
                if (start_compare)
                {
                    if (ls1[i].StartsWith("[我方场面]"))
                    {
                        List<Char> s1 = ls1[i].ToList();
                        List<Char> s2 = ls2[i].ToList();
                        s1.Sort((a, b) => a.CompareTo(b));
                        s2.Sort((a, b) => a.CompareTo(b));
                        String as1 = string.Join("", s1.ToArray());
                        String as2 = string.Join("", s2.ToArray());
                        if (!as1.Equals(as2))
                        {
                            return "false";
                        }
                    }
                    else if (ls1[i] != ls2[i])
                        return "false";
                }
            }
            return "true";
            //创建一个哈希算法对象
            //using (HashAlgorithm hash = HashAlgorithm.Create())
            //{
            //    using (FileStream file1 = new FileStream(filePath1, FileMode.Open), file2 = new FileStream(filePath2, FileMode.Open))
            //    {
            //        byte[] hashByte1 = hash.ComputeHash(file1);//哈希算法根据文本得到哈希码的字节数组
            //        byte[] hashByte2 = hash.ComputeHash(file2);
            //        string str1 = BitConverter.ToString(hashByte1);//将字节数组装换为字符串
            //        string str2 = BitConverter.ToString(hashByte2);
            //        return (str1 == str2 ? "true" : "false");//比较哈希码
            //    }
            //}
        }
    }
}