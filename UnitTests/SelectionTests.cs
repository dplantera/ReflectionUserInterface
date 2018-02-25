﻿using AutoUIConsole.Components.DataTypes;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace UnitTests
{
    [TestClass]
    public class SelectionTests
    {
        [TestMethod]
        public void GetNextPathLevel()
        {
            string fullPath = "base.lev1.lev2.leaf";
            string targetLevel = "lev2";

            PathLevel path = new PathLevel(fullPath, targetLevel);

            Assert.AreEqual("leaf", path.NextLevel);
        }

        [TestMethod]
        public void GetNextPathLevelAtBaseLevel()
        {
            string fullPath = "base.lev1.lev2.leaf";
            string targetLevel = "base";

            PathLevel path = new PathLevel(fullPath, targetLevel);

            Assert.AreEqual("lev1", path.NextLevel);
        }

        [TestMethod]
        public void GetBasePathLevel()
        {
            string fullPath = "base.lev1.lev2.leaf";
            string targetLevel = "lev2";

            PathLevel path = new PathLevel(fullPath, targetLevel);

            Assert.AreEqual("lev2", path.BaseLevel);
        }

        [TestMethod]
        public void GetPreviousPathLevel()
        {
            string fullPath = "base.lev1.lev2.leaf";
            string targetLevel = "lev2";

            PathLevel path = new PathLevel(fullPath, targetLevel);

            Assert.AreEqual("lev1", path.PreviousLevel);
        }

        [TestMethod]
        public void GetPreviousPathLevelAtBaseLevel()
        {
            string fullPath = "base.lev1.lev2.leaf";
            string targetLevel = "base";

            PathLevel path = new PathLevel(fullPath, targetLevel);

            Assert.IsNull(path.PreviousLevel);
        }

        [TestMethod]
        public void GetPreviousPathLevelAtLeafLevel()
        {
            string fullPath = "base.lev1.lev2.leaf";
            string targetLevel = "leaf";

            PathLevel path = new PathLevel(fullPath, targetLevel);

            Assert.AreEqual("lev2", path.PreviousLevel);

        }

        [TestMethod]
        public void GetNextPathLevelAtLeafLevel()
        {
            string fullPath = "base.lev1.lev2.leaf";
            string targetLevel = "leaf";

            PathLevel path = new PathLevel(fullPath, targetLevel);

            Assert.IsNull(path.NextLevel);
        }
    }
}