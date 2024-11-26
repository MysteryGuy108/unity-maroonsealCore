using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

using MaroonSeal.Core.DataStructures.LUTs;

namespace Testing.MaroonSeal.Core.DataStructures.LUTs
{
    public class OrderedLUTTests
    {
        #region Constructors
        [Test]
        public void Constructor_NoParametres_CountZero() {
            var lut = new OrderedLUT<bool>();
            Assert.AreEqual(0, lut.Count);
        }
        #endregion

        #region Add Item
        [Test]
        public void AddItem_TwoItemsWithDifferenceWeights_SortedItems() {
            var lut = new OrderedLUT<int>();
            lut.AddItem(new LUTItem<float, int>(5.0f, 2));

            Assert.AreEqual(5.0f, lut.GetItemWeight(0)); // Checking Key
            Assert.AreEqual(2, lut.GetItemData(0)); // Checking Stored Value

            lut.AddItem(new LUTItem<float, int>(2.0f, 5));

            Assert.AreEqual(2.0f, lut.GetItemWeight(0)); // Checking Key
            Assert.AreEqual(5, lut.GetItemData(0)); // Checking Stored Value

            Assert.AreEqual(5.0f, lut.GetItemWeight(1)); // Checking Key
            Assert.AreEqual(2, lut.GetItemData(1)); // Checking Stored Value

            Assert.LessOrEqual(lut.GetItemWeight(0), lut.GetItemWeight(1));
        }

        [Test]
        public void AddItemRange_DisorderedLUTItemList_OrderedWeights() {
            var lut = new OrderedLUT<int>();

            List<LUTItem<float, int>> itemList = new() {
                new(0.1f, 23),
                new(0.1f, 3),
                new(-1.0f, 2),
                new(10.0f, 6),
                new (5.0f, 1)
            };

            lut.AddItemRange(itemList);

            Assert.AreEqual(itemList.Count, lut.Count); // Checking Count
            for(int i = 0; i < lut.Count-1; i++) {
                Assert.LessOrEqual(lut.GetItemWeight(i), lut.GetItemWeight(i+1));
            }

            lut.Clear();
            Assert.AreEqual(0, lut.Count);
        }
        #endregion
    }
}
