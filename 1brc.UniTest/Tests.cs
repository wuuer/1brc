namespace _1brc.UniTest
{
    public class Tests
    {
        [Test]
        [MethodDataSource(typeof(MyTestDataSources), nameof(MyTestDataSources.AdditionTestData))]
        public async Task Test(string[] paths)
        {
            for (int i = 0; i < paths.Length; i++)
            {
                var result = Program.Run(paths[i]);
                var expectedResult = File.ReadAllText(paths[i].Replace("txt", "out"));
                await Assert.That(result).IsEqualTo(expectedResult);
            }

        }

    }

    public static class MyTestDataSources
    {
        public static Func<string[]> AdditionTestData()
        {
            return () => ["..\\..\\..\\..\\1brc\\data\\measurements-1.txt",
            "..\\..\\..\\..\\1brc\\data\\measurements-2.txt",
            "..\\..\\..\\..\\1brc\\data\\measurements-3.txt",
            "..\\..\\..\\..\\1brc\\data\\measurements-10.txt",
            "..\\..\\..\\..\\1brc\\data\\measurements-20.txt",
            "..\\..\\..\\..\\1brc\\data\\measurements-complex-utf8.txt",
            "..\\..\\..\\..\\1brc\\data\\measurements-rounding.txt",
            "..\\..\\..\\..\\1brc\\data\\measurements-short.txt",
            "..\\..\\..\\..\\1brc\\data\\measurements-shortest.txt",
            "..\\..\\..\\..\\1brc\\data\\measurements-44k.txt"];
        }
    }
}
