using System;
using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Columns;
using BenchmarkDotNet.Jobs;
using BenchmarkDotNet.Configs;
using BenchmarkDotNet.Loggers;
using BenchmarkDotNet.Running;
using BenchmarkDotNet.Exporters;
using BenchmarkDotNet.Analysers;
using BenchmarkDotNet.Diagnosers;
using System.Collections.Generic;
using System.Text;

namespace hell_Work_1
{
    class Work4
    {


        [Config(typeof(MyConfig))]

        public class BenchmarkClass
        {
            private const int ELEMENTS = 10000;
            private const int LENGTH = 20;
            private const string CHECKSTRING = "HL4JH53KJ45H324H52LK";

            public string[] array = new string[ELEMENTS];
            public HashSet<string> hashset = new HashSet<string>();

            private Random rnd = new Random();

            private class MyConfig : ManualConfig
            {
                public MyConfig()
                {
                    AddJob(Job.ShortRun);
                    AddLogger(ConsoleLogger.Default);
                    AddColumn(
                        TargetMethodColumn.Method,
                        StatisticColumn.Mean,
                        StatisticColumn.StdErr,
                        StatisticColumn.StdDev,
                        StatisticColumn.Max,
                        StatisticColumn.Median,
                        BaselineRatioColumn.RatioMean);
                    AddExporter(
                        AsciiDocExporter.Default,
                        MarkdownExporter.GitHub,
                        HtmlExporter.Default);
                    AddAnalyser(EnvironmentAnalyser.Default);
                    AddDiagnoser(new DisassemblyDiagnoser(new DisassemblyDiagnoserConfig(maxDepth: 4, exportDiff: false)));
                    UnionRule = ConfigUnionRule.AlwaysUseLocal;

                }
            }

            public BenchmarkClass()
            {

                for (int i = 0; i < array.Length; i++)
                {
                    array[i] = GenerateString();
                    hashset.Add(GenerateString());
                }
            }

            public string GenerateString()
            {
                string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
                StringBuilder newString = new StringBuilder(LENGTH);

                for (int i = 0; i < LENGTH; ++i)
                    newString.Append(chars[rnd.Next(chars.Length)]);

                return newString.ToString();
            }

            public bool CheckStringInCollection(string[] arr, string checkString)
            {
                bool isStringInArray = false;
                for (int i = 0; i < arr.Length; i++)
                {
                    if (arr[i] == checkString)
                        isStringInArray = true;
                }
                return isStringInArray;
            }
            public bool CheckStringInCollection(HashSet<string> set, string checkString)
            {
                return set.Contains(checkString);
            }

            [Benchmark(Description = "Тест массива", Baseline = true)]
            public void TestArray()
            {
                CheckStringInCollection(array, CHECKSTRING);
            }

            [Benchmark(Description = "Тест хэш таблицы")]
            public void TestHashSet()
            {
                CheckStringInCollection(hashset, CHECKSTRING);
            }

        }
    }
}
