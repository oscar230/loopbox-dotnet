using System;
using Loopbox;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace test
{
    [TestClass]
    public class TestConfig
    {
        [TestMethod]
        public void LoadConfigNoFile()
        {
            Config config = new Config("");
        }
    }
}
