using System;
using Loopbox;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace test
{
    [TestClass]
    public class LoopboxTest
    {
        [TestMethod]
        public void Only4Tracks()
        {
            LoopboxLib lbl = new LoopboxLib();
            lbl.Load(SetupXML.Get4Tracks);
            Assert.IsTrue(lbl.IsLoaded());
            Assert.AreEqual(lbl.GetTracksCount(), 4);
            Assert.AreEqual(lbl.GetAllPlaylistsCount(), 0);
            Assert.AreEqual(lbl.GetTracksExistsCount(), 0);
        }
        [TestMethod]
        public void Large()
        {
            LoopboxLib lbl = new LoopboxLib();
            lbl.Load(SetupXML.GetLarge);
            Assert.IsTrue(lbl.IsLoaded());
        }
        [TestMethod]
        public void Playlists2()
        {
            LoopboxLib lbl = new LoopboxLib();
            lbl.Load(SetupXML.Get2Playlists);
            Assert.IsTrue(lbl.IsLoaded());
            Assert.AreEqual(lbl.GetAllPlaylistsCount(), 2);
        }
    }
}