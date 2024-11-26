using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

using MaroonSeal.Core.DataStructures.LUTs;

namespace Testing.MaroonSeal.Core.DataStructures.LUTs
{
    public class DictionaryLUTTests
    {
        [Test]
        public void Constructor_NoParametres_CountZero() {
            var lut = new DictionaryLUT<int, int>();
            Assert.AreEqual(0, lut.Count);
        }
    }
}
