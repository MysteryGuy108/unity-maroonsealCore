using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

using MaroonSeal.Core.DataStructures.LUTs;

namespace DataStructures.LUTs
{
    public class OrderedValuesLUTTests : OrderedLUTTests
    {
        #region Searching Keys
        [Test]
        public void SearchForFloorKey_TwoItems_LowestItem() {
            List<LUTItem<float, float>> itemList = new() {
                new(0.0f, 4.0f),
                new(1.0f, 16.0f),
            };

            var lut = new OrderedValuesLUT(itemList);
            Assert.AreEqual(0.0f, lut.SearchForFloorKey(10.0f));
        }

        [Test]
        public void SearchForCeilKey_TwoItems_HighestItem() {
            List<LUTItem<float, float>> itemList = new() {
                new(0.0f, 4.0f),
                new(1.0f, 16.0f),
            };

            var lut = new OrderedValuesLUT(itemList);

            Assert.AreEqual(1.0f, lut.SearchForCeilKey(10.0f));
        }

        [Test]
        public void SearchForNearestKey_TwoItems_LowestThenHighestItem() {
            List<LUTItem<float, float>> itemList = new() {
                new(0.0f, 4.0f),
                new(1.0f, 16.0f),
            };

            var lut = new OrderedValuesLUT(itemList);

            Assert.AreEqual(0.0f, lut.SearchForNearestKey(6.0f), "Lowest item was not found.");
            Assert.AreEqual(1.0f, lut.SearchForNearestKey(12.0f), "Highest item was not found.");
        }

        [Test]
        public void SearchForLerpKey_TwoItems_LerpValue() {
            List<LUTItem<float, float>> itemList = new() {
                new(0.0f, 4.0f),
                new(1.0f, 16.0f),
            };

            var lut = new OrderedValuesLUT(itemList);

            float lerpValue = lut.SearchForLerpKey(10.0f, out (float, float) lerpAnchors);

            Assert.AreEqual((0.0f, 1.0f), lerpAnchors, "Lerp anchors is incorrect.");
            Assert.AreEqual(0.5f, lerpValue, "Lerp value is incorrect.");
        }
        #endregion
    }
}
