using System;
using System.Collections.Generic;
using System.IO;
using Loopbox;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace test
{
    [TestClass]
    public class TestConfig
    {
        List<string> configs;

        public TestConfig()
        {
        }

        [TestMethod]
        [ExpectedException(typeof(NullReferenceException))]
        public void LoadConfigNoFile()
        {
            Config config = new Config("");
        }
    }
}