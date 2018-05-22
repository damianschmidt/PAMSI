using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace warcaby
{
    public class Node
    {
        private object data;        // Hold data in node
        //private Node left, right;   // Reference to left and right child

        #region Constructors
        public Node() : this(null) { }
        public Node(object data) : this(data, null, null) { }
        public Node(object data, Node l, Node right)
        {
            this.data = data;
          //  this.left = left;
          //  this.right = right;
        }
        #endregion

        #region Getter's & setter's
        public object Value
        {
            get
            {
                return data;
            }
            set
            {
                data = value;
            }
        }

        public Node left
        {
            get
            {
                return left;
            }
            set
            {
                left = value;
            }
        }

        public Node right
        {
            get
            {
                return right;
            }
            set
            {
                right = value;
            }
        }
        #endregion
    }
}
