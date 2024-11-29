using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

using MaroonSeal.Core.DataStructures.LUTs;

namespace DataStructures.LUTs
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
        public void AddItem_TwoItemsWithDifferenceKeys_SortedItems() {
            var lut = new OrderedLUT<int>();
            lut.AddItem(new LUTItem<float, int>(5.0f, 2));

            Assert.AreEqual(5.0f, lut.GetItemKey(0)); // Checking Key
            Assert.AreEqual(2, lut.GetItemData(0)); // Checking Stored Value

            lut.AddItem(new LUTItem<float, int>(2.0f, 5));

            Assert.AreEqual(2.0f, lut.GetItemKey(0)); // Checking Key
            Assert.AreEqual(5, lut.GetItemData(0)); // Checking Stored Value

            Assert.AreEqual(5.0f, lut.GetItemKey(1)); // Checking Key
            Assert.AreEqual(2, lut.GetItemData(1)); // Checking Stored Value

            Assert.LessOrEqual(lut.GetItemKey(0), lut.GetItemKey(1));
        }
        #endregion

        #region Add Item Range
        [Test]
        public void AddItemRange_DisorderedLUTItemList_OrderedKeys() {
            var lut = new OrderedLUT<int>();

            List<LUTItem<float, int>> itemList = new() {
                new(0.1f, 23),
                new(0.1f, 3),
                new(-1.0f, 2),
                new(10.0f, 6),
                new (5.0f, 1)
            };

            lut.AddItemRange(itemList);

            Assert.AreEqual(itemList.Count, lut.Count, "Count is not equal to item list");
            for(int i = 0; i < lut.Count-1; i++) {
                Assert.LessOrEqual(lut.GetItemKey(i), lut.GetItemKey(i+1));
            }

            lut.Clear();
            Assert.AreEqual(0, lut.Count);
        }
        #endregion

        #region Searching Data
        [Test]
        public void SearchForFloorData_TwoItems_LowestItem() {
            List<LUTItem<float, int>> itemList = new() {
                new(0.0f, 23),
                new(1.0f, 3),
            };

            var lut = new OrderedLUT<int>(itemList);

            Assert.AreEqual(23, lut.SearchForFloorData(0.5f));
        }

        [Test]
        public void SearchForCeilData_TwoItems_HighestItem() {
            List<LUTItem<float, int>> itemList = new() {
                new(0.0f, 23),
                new(1.0f, 3),
            };

            var lut = new OrderedLUT<int>(itemList);

            Assert.AreEqual(3, lut.SearchForCeilData(0.5f));
        }

        [Test]
        public void SearchForNearestData_TwoItems_LowestThenHighestItem() {
            List<LUTItem<float, int>> itemList = new() {
                new(0.0f, 23),
                new(1.0f, 3),
            };

            var lut = new OrderedLUT<int>(itemList);

            Assert.AreEqual(23, lut.SearchForNearestData(0.25f), "Lowest item was not found.");
            Assert.AreEqual(3, lut.SearchForNearestData(0.75f), "Highest item was not found.");
        }

        [Test]
        public void SearchForLerpData_TwoItems_LerpValue() {
            List<LUTItem<float, int>> itemList = new() {
                new(0.0f, 23),
                new(10.0f, 3),
            };

            var lut = new OrderedLUT<int>(itemList);

            float lerpValue = lut.SearchForLerpData(2.5f, out (int, int) lerpAnchors);

            Assert.AreEqual(0.25f, lerpValue, "Lerp value is incorrect.");
            Assert.AreEqual((23, 3), lerpAnchors, "Lerp anchors is incorrect.");
        }
        #endregion
    }
}
