using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BinaryTreeAPI.Request;
using BinaryTreeDomain.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BinaryTreeAPI.Controller
{
    [Route("v1/")]
    [ApiController]
    public class BinaryTreeController : ControllerBase
    {
        private ITreeBuilder _treeBuilder { get; set; }

        public BinaryTreeController(ITreeBuilder treeBuilder)
        {
            this._treeBuilder = treeBuilder;
        }

        [HttpPost]
        [Route("CreateBinaryTreeStructure")]
        public async Task<ActionResult<INode>> CreateBinaryTreeAsync(TreeStructureRequest dataRequest)
        {
            if (dataRequest == null || dataRequest.DataList == null || !dataRequest.DataList.Any())
            {
                return BadRequest("The request has invalid data or does not have any data at all");
            }
            var rootNode = await this._treeBuilder.CreateTreeStructureAsync( dataRequest.DataList);

            if (rootNode == null)
            {
                return Ok(null);
            }

            return Ok(rootNode);
        }

        [HttpPost]
        [Route("LowestCommonAncestor")]
        public async Task<ActionResult<INode>> GetLowestCommonAncestorAsync(TreeStructureRequest dataRequest)
        {
            if (dataRequest == null || dataRequest.Node == null)
            {
                return BadRequest("The request has invalid data or does not have any data at all");
            }
            var rootNode = await this._treeBuilder.GetClosestCommonAncestorAsync(dataRequest.Node, dataRequest.FirstDescendant, dataRequest.SecondDescendant);

            if (rootNode == null)
            {
                return Ok("Is possible that one or both descendants does not exist in the tree");
            }

            return Ok(rootNode);
        }
    }
}