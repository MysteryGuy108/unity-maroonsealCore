using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

using MaroonSeal.Maths.Cardinals;

namespace MaroonSealTesting.Maths.Cardinal
{
    public class Cardinal2DTests
    {
        #region Constructors
        [Test]
        public void Constructor_NoParametres_EqualsNorth() {
            var cardinal = new Cardinal2D();

            Assert.AreEqual(Cardinal2D.CardinalIndex.North, cardinal.Direction);
            Assert.AreEqual(Cardinal2D.North, cardinal);
        }
        #endregion
    }
}
