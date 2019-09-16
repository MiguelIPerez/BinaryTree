
using BinaryTreeDomain;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace BinaryTreeTests
{
    [TestClass]
    public class NodeTests
    {
        [TestMethod]
        public async Task InsertNode_GivenValidValues_ShouldBuildTree()
        {
            var rootNode = new Node(15);

            Assert.IsTrue(await rootNode.InsertNodeAsync(8));
            Assert.IsTrue(await rootNode.InsertNodeAsync(25));
            Assert.IsTrue(await rootNode.InsertNodeAsync(76));
            Assert.IsTrue(await rootNode.InsertNodeAsync(2));
            Assert.IsTrue(await rootNode.InsertNodeAsync(18));
            Assert.IsTrue(await rootNode.InsertNodeAsync(13));
        }

        [TestMethod]
        public async Task InsertNode_GivenDuplicatedValue_ShouldNotInsertDuplicatedValues()
        {
            // Arrange
            var rootNode = new Node(15);

            // Act, Assert
            Assert.IsTrue(await rootNode.InsertNodeAsync(8));
            Assert.IsTrue(await rootNode.InsertNodeAsync(25));
            Assert.IsTrue(await rootNode.InsertNodeAsync(76));
            Assert.IsTrue(await rootNode.InsertNodeAsync(2));
            Assert.IsTrue(await rootNode.InsertNodeAsync(18));
            Assert.IsTrue(await rootNode.InsertNodeAsync(13));
            Assert.IsFalse(await rootNode.InsertNodeAsync(2));
        }

        [TestMethod]
        public async Task Exist_GivenACollectionOfAlreadyExistingNodes_ShouldValidateAllOfThemAndReturnTrue()
        {
            // Arrange
            int seed = 10000;
            var existingList = await GetExistingList(seed);
            var rootNode = await this.GetTree(seed, existingList);
            var stopWatch = new Stopwatch();
            this.ShiftList(existingList);

            // Act, Assert
            stopWatch.Start();
            foreach (var valueItem in existingList)
            {
                Assert.IsTrue(await rootNode.ExistAsync(valueItem));
            }
            stopWatch.Stop();

            Console.WriteLine(stopWatch.ElapsedMilliseconds);
        }

        [TestMethod]
        public async Task Exist_GivenACollectionOfNoneExistingNodes_ShouldValidateAllOfThemAndReturnFalse()
        {
            // Arrange
            IList<int> noneExistingList = new List<int>();
            int seed = 1000;
            var existingList = await GetExistingList(seed);
            var rootNode = await this.GetTree(seed, existingList);
            var random = new Random();
            for (var i =0; i<200; i++)
            {
                noneExistingList.Add(random.Next(1001, 1100));
            }

            var stopWatch = new Stopwatch();
            stopWatch.Start();
            foreach (var valueItem in noneExistingList)
            {
                Assert.IsFalse(await rootNode.ExistAsync(valueItem));
            }
            stopWatch.Stop();

            Console.WriteLine(stopWatch.Elapsed);
        }

        [TestMethod]
        public async Task Exist_GivenADataThatNotExist_ShouldReturnFalse()
        {
            IList<int> existingList = new List<int> { 36, 48, 47, 9, 16, 11, 2, 8, 33, 3, 18, 34, 49, 27, 19, 1, 41, 28, 5, 20, 39, 32, 7, 42, 37, 10, 31, 29, 30, 24, 15, 23, 17, 54 };
            var rootNode = await new TreeBuilder().CreateTreeStructureAsync(existingList);

            Assert.IsFalse(await rootNode.ExistAsync(45));
            Assert.IsFalse(await rootNode.ExistAsync(21));
            Assert.IsFalse(await rootNode.ExistAsync(6));
            Assert.IsFalse(await rootNode.ExistAsync(14));
            Assert.IsFalse(await rootNode.ExistAsync(22));
        }

        [TestMethod]
        public async Task Exist_GivenADataThatExist_ShouldReturnTrue()
        {
            IList<int> existingList = new List<int> { 36, 48, 47, 9, 16, 11, 2, 8, 33, 3, 18, 34, 49, 27, 19, 1, 41, 28, 5, 20, 39, 32, 7, 42, 37, 10, 31, 29, 30, 24, 15, 23, 17, 54 };
            var rootNode = await new TreeBuilder().CreateTreeStructureAsync(existingList);
            Assert.IsTrue(await rootNode.ExistAsync(27));
            Assert.IsTrue(await rootNode.ExistAsync(31));
            Assert.IsTrue(await rootNode.ExistAsync(24));
            Assert.IsTrue(await rootNode.ExistAsync(7));
            Assert.IsTrue(await rootNode.ExistAsync(54));
        }

        [TestMethod]
        public async Task GetCommonAncestor_GivenOneNumberDoesNotExist_ShouldReturnTheAncestorIfTheyWouldBeInsertedJustNow()
        {
            IList<int> existingList = new List<int> { 36, 48, 47, 9, 16, 11, 2, 8, 33, 3, 18, 34, 49, 27, 19, 1, 41, 28, 5, 20, 39, 32, 7, 42, 37, 10, 31, 29, 30, 24, 15, 23, 17, 54 };
            var rootNode = await new TreeBuilder().CreateTreeStructureAsync(existingList);

            var commonAncestor = await rootNode.GetCommonAncestorAsync(21, 6);

            Assert.IsNotNull(commonAncestor);
            Assert.AreEqual(commonAncestor.Data, 9);
        }

        [TestMethod]
        public async Task GetCommonAncestor_GivenTwoExistingNumberInTheTree_ShouldReturnTheCommonAncestor()
        {
            IList<int> existingList = new List<int> { 36, 48, 47, 9, 16, 11, 2, 8, 33, 3, 18, 34, 49, 27, 19, 1, 41, 28, 5, 20, 39, 32, 7, 42, 37, 10, 31, 29, 30, 24, 15, 23, 17, 54 };
            var rootNode = await new TreeBuilder().CreateTreeStructureAsync(existingList);

            var commonAncestor = await rootNode.GetCommonAncestorAsync(10, 29);

            Assert.IsNotNull(commonAncestor);
            Assert.AreEqual(commonAncestor.Data, 16);
        }

        [TestMethod]
        public async Task GetCommonAncestor_GivenOneOfTheNumbersIsAncestorOfTheOther_ShouldReturnTheParentNodeOfTheAncestorNumber()
        {
            IList<int> existingList = new List<int> { 36, 48, 47, 9, 16, 11, 2, 8, 33, 3, 18, 34, 49, 27, 19, 1, 41, 28, 5, 20, 39, 32, 7, 42, 37, 10, 31, 29, 30, 24, 15, 23, 17, 54 };

            var rootNode = await new TreeBuilder().CreateTreeStructureAsync(existingList);

            var commonAncestor = await rootNode.GetCommonAncestorAsync(33, 32);

            Assert.IsNotNull(commonAncestor);
            Assert.AreEqual(commonAncestor.Data, 16);
        }

        [TestMethod]
        public async Task GetCommonAncestor_GivenBothNumbersAreEqual_ShouldReturnTheAncestor()
        {
            IList<int> existingList = new List<int> { 36, 48, 47, 9, 16, 11, 2, 8, 33, 3, 18, 34, 49, 27, 19, 1, 41, 28, 5, 20, 39, 32, 7, 42, 37, 10, 31, 29, 30, 24, 15, 23, 17, 54 };

            var rootNode = await new TreeBuilder().CreateTreeStructureAsync(existingList);

            var commonAncestor = await rootNode.GetCommonAncestorAsync(27, 27);

            Assert.IsNotNull(commonAncestor);
            Assert.AreEqual(commonAncestor.Data, 18);
        }

        [TestMethod]
        public async Task GetCommonAncestor_GivenOneNumberIsTheRoot_ShouldReturnTheRootNode()
        {
            IList<int> existingList = new List<int> { 36, 48, 47, 9, 16, 11, 2, 8, 33, 3, 18, 34, 49, 27, 19, 1, 41, 28, 5, 20, 39, 32, 7, 42, 37, 10, 31, 29, 30, 24, 15, 23, 17, 54 };

            var rootNode = await new TreeBuilder().CreateTreeStructureAsync(existingList);

            var commonAncestor = await rootNode.GetCommonAncestorAsync(36, 27);

            Assert.IsNotNull(commonAncestor);
            Assert.AreEqual(commonAncestor.Data, 36);
        }

        private async Task<IList<int>> GetExistingList(int seed)
        {
            var random = new Random();
            var rootNode = new Node(random.Next(0, seed));

            var existintList = await Task.Run(() => {
                var existingAsyncList = new List<int>();
                foreach (var item in Enumerable.Range(0, seed))
                {
                    var valueToInsert = random.Next(0, seed);
                    if (!(existingAsyncList.Any(x => x == valueToInsert)))
                    {
                        existingAsyncList.Add(valueToInsert);
                    }
                }

                return existingAsyncList;
            });

            return existintList;
        }

        private async Task<Node> GetTree(int seed, IList<int> existingList)
        {
            var random = new Random();
            var rootNode = new Node(random.Next(0, seed));

            foreach (var valueItem in existingList)
            {
                await rootNode.InsertNodeAsync(valueItem);
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
