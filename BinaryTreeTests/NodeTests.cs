
using BinaryTreeDomain;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace BinaryTreeTests
{
    [TestClass]
    public class NodeTests
    {
        [TestMethod]
        public void InsertNode_GivenValidValues_ShouldBuildTree()
        {
            var rootNode = new Node(15);

            Assert.IsTrue(rootNode.InsertNode(8));
            Assert.IsTrue(rootNode.InsertNode(25));
            Assert.IsTrue(rootNode.InsertNode(76));
            Assert.IsTrue(rootNode.InsertNode(2));
            Assert.IsTrue(rootNode.InsertNode(18));
            Assert.IsTrue(rootNode.InsertNode(13));
        }

        [TestMethod]
        public void InsertNode_GivenDuplicatedValue_ShouldNotInsertDuplicatedValues()
        {
            // Arrange
            var rootNode = new Node(15);

            // Act, Assert
            Assert.IsTrue(rootNode.InsertNode(8));
            Assert.IsTrue(rootNode.InsertNode(25));
            Assert.IsTrue(rootNode.InsertNode(76));
            Assert.IsTrue(rootNode.InsertNode(2));
            Assert.IsTrue(rootNode.InsertNode(18));
            Assert.IsTrue(rootNode.InsertNode(13));
            Assert.IsFalse(rootNode.InsertNode(2));
        }

        [TestMethod]
        public void Exist_GivenACollectionOfAlreadyExistingNodes_ShouldValidateAllOfThem()
        {
            // Arrange
            IList<int> existingList;
            var rootNode = this.GetTree(10000, out existingList);
            var stopWatch = new Stopwatch();
            this.ShiftList(existingList);

            // Act, Assert
            stopWatch.Start();
            foreach (var valueItem in existingList)
            {
                Assert.IsTrue(rootNode.Exist(valueItem));
            }
            stopWatch.Stop();

            Console.WriteLine(stopWatch.ElapsedMilliseconds);
        }

        [TestMethod]
        public void Exist_GivenACollectionOfNoneExistingNodes_ShouldValidateAllOfThem()
        {
            // Arrange
            IList<int> existingList;
            IList<int> noneExistingList = new List<int>();
            var rootNode = this.GetTree(1000, out existingList);
            var random = new Random();
            for (var i =0; i<200; i++)
            {
                noneExistingList.Add(random.Next(1001, 1100));
            }

            var stopWatch = new Stopwatch();
            stopWatch.Start();
            foreach (var valueItem in noneExistingList)
            {
                Assert.IsFalse(rootNode.Exist(valueItem));
            }
            stopWatch.Stop();

            Console.WriteLine(stopWatch.Elapsed);
        }

        [TestMethod]
        public void Exist_GivenADataThatNotExist_ShouldReturnFalse()
        {
            IList<int> existingList = new List<int> { 36, 48, 47, 9, 16, 11, 2, 8, 33, 3, 18, 34, 49, 27, 19, 1, 41, 28, 5, 20, 39, 32, 7, 42, 37, 10, 31, 29, 30, 24, 15, 23, 17, 54 };
            var rootNode = new Tree().CreateTreeStructure(existingList);

            Assert.IsFalse(rootNode.Exist(45));
            Assert.IsFalse(rootNode.Exist(21));
            Assert.IsFalse(rootNode.Exist(6));
            Assert.IsFalse(rootNode.Exist(14));
            Assert.IsFalse(rootNode.Exist(22));
        }

        [TestMethod]
        public void Exist_GivenADataThatExist_ShouldReturnTrue()
        {
            IList<int> existingList = new List<int> { 36, 48, 47, 9, 16, 11, 2, 8, 33, 3, 18, 34, 49, 27, 19, 1, 41, 28, 5, 20, 39, 32, 7, 42, 37, 10, 31, 29, 30, 24, 15, 23, 17, 54 };
            var rootNode = new Tree().CreateTreeStructure(existingList);
            Assert.IsTrue(rootNode.Exist(27));
            Assert.IsTrue(rootNode.Exist(31));
            Assert.IsTrue(rootNode.Exist(24));
            Assert.IsTrue(rootNode.Exist(7));
            Assert.IsTrue(rootNode.Exist(54));
        }
        
        [TestMethod]
        public void GetCommonAncestor_GivenOneNumberDoesNotExist_ShouldReturnTheAncestorIfTheyWouldBeInsertedJustNow()
        {
            IList<int> existingList = new List<int> { 36, 48, 47, 9, 16, 11, 2, 8, 33, 3, 18, 34, 49, 27, 19, 1, 41, 28, 5, 20, 39, 32, 7, 42, 37, 10, 31, 29, 30, 24, 15, 23, 17, 54 };
            var rootNode = new Tree().CreateTreeStructure(existingList);

            var commonAncestor = rootNode.GetCommonAncestor(21, 6);

            Assert.IsNotNull(commonAncestor);
            Assert.AreEqual(commonAncestor.Data, 9);
        }

        [TestMethod]
        public void GetCommonAncestor_GivenTwoExistingNumberInTheTree_ShouldReturnTheCommonAncestor()
        {
            IList<int> existingList = new List<int> { 36, 48, 47, 9, 16, 11, 2, 8, 33, 3, 18, 34, 49, 27, 19, 1, 41, 28, 5, 20, 39, 32, 7, 42, 37, 10, 31, 29, 30, 24, 15, 23, 17, 54 };
            var rootNode = new Tree().CreateTreeStructure(existingList);

            var commonAncestor = rootNode.GetCommonAncestor(10, 29);

            Assert.IsNotNull(commonAncestor);
            Assert.AreEqual(commonAncestor.Data, 16);
        }

        [TestMethod]
        public void GetCommonAncestor_GivenOneOfTheNumbersIsAncestorOfTheOther_ShouldReturnTheParentNodeOfTheAncestorNumber()
        {
            IList<int> existingList = new List<int> { 36, 48, 47, 9, 16, 11, 2, 8, 33, 3, 18, 34, 49, 27, 19, 1, 41, 28, 5, 20, 39, 32, 7, 42, 37, 10, 31, 29, 30, 24, 15, 23, 17, 54 };
            
            var rootNode = new Tree().CreateTreeStructure(existingList);

            var commonAncestor = rootNode.GetCommonAncestor(33, 32);

            Assert.IsNotNull(commonAncestor);
            Assert.AreEqual(commonAncestor.Data, 16);
        }

        [TestMethod]
        public void GetCommonAncestor_GivenBothNumbersAreEqual_ShouldReturnTheAncestor()
        {
            IList<int> existingList = new List<int> { 36, 48, 47, 9, 16, 11, 2, 8, 33, 3, 18, 34, 49, 27, 19, 1, 41, 28, 5, 20, 39, 32, 7, 42, 37, 10, 31, 29, 30, 24, 15, 23, 17, 54 };

            var rootNode = new Tree().CreateTreeStructure(existingList);

            var commonAncestor = rootNode.GetCommonAncestor(27, 27);

            Assert.IsNotNull(commonAncestor);
            Assert.AreEqual(commonAncestor.Data, 18);
        }

        [TestMethod]
        public void GetCommonAncestor_GivenOneNumberIsTheRoot_ShouldReturnTheRootNode()
        {
            IList<int> existingList = new List<int> { 36, 48, 47, 9, 16, 11, 2, 8, 33, 3, 18, 34, 49, 27, 19, 1, 41, 28, 5, 20, 39, 32, 7, 42, 37, 10, 31, 29, 30, 24, 15, 23, 17, 54 };

            var rootNode = new Tree().CreateTreeStructure(existingList);

            var commonAncestor = rootNode.GetCommonAncestor(36, 27);

            Assert.IsNotNull(commonAncestor);
            Assert.AreEqual(commonAncestor.Data, 36);
        }

        private Node GetTree(int seed, out IList<int> existingList)
        {
            var random = new Random();
            var rootNode = new Node(random.Next(0, seed));
            existingList = new List<int>();
            foreach (var item in Enumerable.Range(0, seed))
            {
                var valueToInsert = random.Next(0, seed);
                if (!(existingList.Any(x => x == valueToInsert)))
                {
                    existingList.Add(valueToInsert);
                }
            }

            foreach (var valueItem in existingList)
            {
                rootNode.InsertNode(valueItem);
            }

            return rootNode;
        }

        private void ShiftList(IList<int> listToShift)
        {
            if (listToShift.Any())
            {
                var random = new Random();
                for (int i = 0; i < listToShift.Count; i++)
                {
                    var randomIndex = random.Next(0, listToShift.Count);
                    var tempItem = listToShift[i];
                    listToShift[i] = listToShift[randomIndex];
                    listToShift[randomIndex] = tempItem;
                }
            }
        }
    }
}
