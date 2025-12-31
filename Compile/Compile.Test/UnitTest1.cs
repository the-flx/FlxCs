namespace FlxCs.Test
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestWord()
        {
            Assert.AreEqual(true, Flx.Word('c'));
            Assert.AreEqual(false, Flx.Word(' '));
        }

        [TestMethod]
        public void TestCapital()
        {
            Assert.AreEqual(true, Flx.Capital('C'));
            Assert.AreEqual(false, Flx.Capital('c'));
        }

        [TestMethod]
        public void TestScore()
        {
            Result result = Flx.Score("switch-to-buffer", "stb");
            Assert.AreEqual(237, result.score);

            result = Flx.Score("TestSomeFunctionExterme", "met");
            Assert.AreEqual(57, result.score);

            result = Flx.Score("MetaX_Version", "met");
            Assert.AreEqual(211, result.score);
        }
    }
}
