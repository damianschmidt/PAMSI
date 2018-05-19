using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace warcaby
{
    class Tree
    {
        private Node root;

        public Tree()
        {
            root = null;
        }

        #region Clear method
        public virtual void Clear()
        {
            root = null;
        }
        #endregion

        #region Getter's and setter's
        public Node Root
        {
            get
            {
                return root;
            }
            set
            {
                root = value;
            }
        }
        #endregion
    }
}
