using BinaryTreeDomain;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BinaryTreeTests
{
    [TestClass]
    public class TreeBuilderTests
    {
        public TreeBuilder Tree { get; set; }

        [TestInitialize]
        public void SetUp()
        {
            Tree = new TreeBuilder();
        }

        [TestCleanup]
        public void TearDown()
        {
            Tree = null;
        }

        [TestMethod]
        public void CreateTreeStructure_GivenNoArguments_ShouldCreateRootNodeWithDefaultIntData()
        {
            // Act
            var rootNode = Tree.CreateTreeStructure();

            // Assert
            Assert.IsNotNull(rootNode);
            Assert.AreEqual(rootNode.Data, default(int));
        }

        [TestMethod]
        public void CreateTreeStructure_GivenAListOfData_ShouldCreateTreeStructure()
        {
            var dataList = new List<int>();
            var random = new Random();
            foreach (var item in Enumerable.Range(0, 20))
            {
                dataList.Add(random.Next(0, 20));
            }

            var rootNode = Tree.CreateTreeStructureAsync(dataList);
            Assert.IsNotNull(rootNode.Result);
            Assert.IsNotNull(rootNode.IsCompleted);
            Assert.IsNotNull(rootNode.IsCompletedSuccessfully);
            Assert.IsNotNull(rootNode.Result);
        }

        [TestMethod]
        public async Task Insert_GivenOneNonExistingDataInTheTree_ShouldReturnTheNodeWithTheDataInserted()
        {
            var rootNode = new Node(21);
            await Tree.InsertNodeAsync(rootNode, 58);
            Assert.IsNotNull(rootNode.RightChild);
            Assert.AreEqual(rootNode.RightChild.Data, 58);
        }

        [TestMethod]
        public async Task Insert_GivenOneExistingDataInTheTree_ShouldReturnTheNodeWithoutInsertingTheDuplicatedData()
        {
            var rootNode = new Node(21);
            await Tree.InsertNodeAsync(rootNode, 58);
            await Tree.InsertNodeAsync(rootNode, 21);
            await Tree.InsertNodeAsync(rootNode, 20);

            Assert.AreEqual(rootNode.RightChild.Data, 58);
            Assert.AreEqual(rootNode.LeftChild.Data, 20);

            Assert.IsNull(rootNode.RightChild.RightChild);
            Assert.IsNull(rootNode.RightChild.LeftChild);

            Assert.IsNull(rootNode.LeftChild.RightChild);
            Assert.IsNull(rootNode.LeftChild.LeftChild);
        }

        [TestMethod]
        public async Task Insert_GivenRootNodeIsNull_ShouldCreateTheNodeWithGivenDataAsARoot()
        {
            var rootNode = await Tree.InsertNodeAsync(null, 85);
            Assert.IsNotNull(rootNode);
            Assert.AreEqual(rootNode.Data, 85);
        }

        [TestMethod]
        public async Task Insert_GivenAListOfData_ShouldCreateTreeStructure()
        {
            var rootNode = new Node(25);
            foreach (var dataNode in new List<int> { 20, 39, 32, 7, 42, 37, 10 })
            {
                await Tree.InsertNodeAsync(rootNode, dataNode);
            }

            Assert.AreEqual(rootNode.LeftChild.Data, 20);
            Assert.AreEqual(rootNode.RightChild.Data, 39);

            Assert.AreEqual(rootNode.LeftChild.LeftChild.Data, 7);
            Assert.IsNull(rootNode.LeftChild.RightChild);

            Assert.AreEqual(rootNode.LeftChild.LeftChild.RightChild.Data, 10);
            Assert.IsNull(rootNode.LeftChild.LeftChild.LeftChild);

            Assert.AreEqual(rootNode.RightChild.LeftChild.Data, 32);
            Assert.AreEqual(rootNode.RightChild.RightChild.Data, 42);

            Assert.AreEqual(rootNode.RightChild.LeftChild.RightChild.Data, 37);
            Assert.IsNull(rootNode.RightChild.LeftChild.LeftChild);

            Assert.IsNull(rootNode.RightChild.RightChild.RightChild);
            Assert.IsNull(rootNode.RightChild.RightChild.LeftChild);
        }

        [TestMethod]
        public async Task Insert_GivenNullRootAndAListOfData_ShouldCreateTreeStructureAccordingly()
        {
            var rootNode = await Tree.InsertNodeAsync(null, 25);

            foreach (var dataNode in new List<int> { 20, 39, 32, 7, 42, 37, 10 })
            {
                await Tree.InsertNodeAsync(rootNode, dataNode);
            }

            Assert.IsNotNull(rootNode);
            Assert.AreEqual(rootNode.Data, 25);

            Assert.AreEqual(rootNode.LeftChild.Data, 20);
            Assert.AreEqual(rootNode.RightChild.Data, 39);

            Assert.AreEqual(rootNode.LeftChild.LeftChild.Data, 7);
            Assert.IsNull(rootNode.LeftChild.RightChild);

            Assert.AreEqual(rootNode.LeftChild.LeftChild.RightChild.Data, 10);
            Assert.IsNull(rootNode.LeftChild.LeftChild.LeftChild);

            Assert.AreEqual(rootNode.RightChild.LeftChild.Data, 32);
            Assert.AreEqual(rootNode.RightChild.RightChild.Data, 42);

            Assert.AreEqual(rootNode.RightChild.LeftChild.RightChild.Data, 37);
            Assert.IsNull(rootNode.RightChild.LeftChild.LeftChild);

            Assert.IsNull(rootNode.RightChild.RightChild.RightChild);
            Assert.IsNull(rootNode.RightChild.RightChild.LeftChild);
        }

        [TestMethod]
        public async Task Insert_GivenRootNodeNullAndDefaultInt_ShouldReturnRootNodeWithDefauiltData()
        {
            var rootNode = await Tree.InsertNodeAsync(null, default(int));

            Assert.IsNotNull(rootNode);
            Assert.AreEqual(rootNode.Data, 0);
        }

        [TestMethod]
        public async Task GetClosestCommonAncestor_GivenTwoExistingNodes_ShouldReturnAncestor()
        {
            var rootNode = await Tree.CreateTreeStructureAsync(new List<int> { 67, 39, 76, 28, 44, 29, 74, 85, 83, 87});
            var ancestor = await Tree.GetClosestCommonAncestorAsync(rootNode, 29, 44);
            var ancestor2 = await Tree.GetClosestCommonAncestorAsync(rootNode, 44, 85);
            var ancestor3 = await Tree.GetClosestCommonAncestorAsync(rootNode, 83, 87);

            Assert.IsNotNull(ancestor);
            Assert.IsNotNull(ancestor2);
            Assert.IsNotNull(ancestor3);

            Assert.AreEqual(ancestor.Data, 39);
            Assert.AreEqual(ancestor2.Data, 67);
            Assert.AreEqual(ancestor3.Data, 85);
        }

        [TestMethod ]
        public async Task GetClosestCommonAncestor_GivenDataDoesNotExistOnTheTree_ShouldReturnNullAncestor()
        {
            var rootNode = await Tree.CreateTreeStructureAsync(new List<int> { 67, 39, 76, 28, 44, 29, 74, 85, 83, 87 });
            var ancestor = await Tree.GetClosestCommonAncestorAsync(rootNode, 19, 44);
            var ancestor2 = await Tree.GetClosestCommonAncestorAsync(rootNode, 44, 60);
            var ancestor3 = await Tree.GetClosestCommonAncestorAsync(rootNode, 19, 60);

            Assert.IsNull(ancestor);
            Assert.IsNull(ancestor2);
            Assert.IsNull(ancestor3);
        }

        [TestMethod]
        public async Task GetClosestCommonAncestor_GivenNullTree_ShouldReturnNullAncestor()
        {
            var ancestor = await Tree.GetClosestCommonAncestorAsync(null, 19, 58);

            Assert.IsNull(ancestor);
        }

        [TestMethod]
        public async Task GetClosestCommonAncestor_GivenParametersAreTheSame_ShouldReturnAncestor()
        {
            var rootNode = await Tree.CreateTreeStructureAsync(new List<int> { 67, 39, 76, 28, 44, 29, 74, 85, 83, 87 });
            var ancestor = await Tree.GetClosestCommonAncestorAsync(rootNode, 83, 83);

            Assert.IsNotNull(ancestor);
            Assert.AreEqual(ancestor.Data, 85);
        }

        [TestMethod]
        public async Task GetClosestCommonAncestor_GivenParameterIsRoot_ShouldReturnRootAsCommonAncestor()
        {
            var rootNode = await Tree.CreateTreeStructureAsync(new List<int> { 67, 39, 76, 28, 44, 29, 74, 85, 83, 87 });
            var ancestor = await Tree.GetClosestCommonAncestorAsync(rootNode, 83, 67);

            Assert.IsNotNull(ancestor);
            Assert.AreEqual(ancestor.Data, 67);
        }

        [TestMethod]
        public async Task GetClosestCommonAncestor_GivenOneParameterIsAncestorOfTheOther_ShouldReturnTheParentOfTheAncestorParameterAsCommon()
        {
            var rootNode = await Tree.CreateTreeStructureAsync(new List<int> { 67, 39, 76, 28, 44, 29, 74, 85, 83, 87 });
            var ancestor = await Tree.GetClosestCommonAncestorAsync(rootNode, 85, 87);

            Assert.IsNotNull(ancestor);
            Assert.AreEqual(ancestor.Data, 76);
        }
    }
}
