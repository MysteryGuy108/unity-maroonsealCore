using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

using MaroonSeal.Maths;

namespace MaroonSealTesting.Maths.PolarCoordinates
{
    public class PolarCoordinate2DTests
    {
        #region Constructors
        [Test]
        public void Constructor_NoParametres_EqualsOrigin()
        {
            var coordinate = new PolarVector2();
            Assert.AreEqual(0.0f, coordinate.theta);
        }
        #endregion
    }
}
