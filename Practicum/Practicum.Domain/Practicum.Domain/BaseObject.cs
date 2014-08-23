using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Practicum.Domain
{
    public class BaseObject
    {
        #region Locals
        private int iD;
        private string name;
        #endregion

        #region Constructors
        public BaseObject()
        {
        }

        public BaseObject(int iD, string name)
        {
            this.iD = iD;
            this.name = name;
        }
        #endregion

        #region Public Properties
        public int ID
        {
            get
            {
                return this.iD;
            }
            set
            {
                this.iD = value;
            }
        }
        public string Name
        {
            get
            {
                return this.name;
            }
            set
            {
                this.name = value;
            }
        }
        #endregion
    }
}

