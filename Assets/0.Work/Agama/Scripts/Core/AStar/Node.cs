using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Agama.Scripts.Core.AStar
{
    public class Node
    {
        public bool canPass;
        public Vector2 worldPosition;
        public int xIndex, yIndex;

        public int forendDistanceCost, euclidStreetCost;
        public int TotalCost => forendDistanceCost + euclidStreetCost;

        public Node parent;

        public Node(bool canPass, Vector2 worldPosition, int xIndex, int yIndex)
        {
            this.canPass = canPass;
            this.worldPosition = worldPosition;
            this.xIndex = xIndex;
            this.yIndex = yIndex;
        }
    }
}
