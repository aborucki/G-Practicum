using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Practicum.Domain
{
    public class DishType:BaseObject
    {
        #region Locals
        private string item;
        private bool multiple;

        #endregion

        #region Constructors
        
        public DishType(int iD, string name, string item, bool multiple):base(iD,name)
        {
            this.item = item;
            this.multiple = multiple;
        }
        #endregion

        #region Public Properties
        public string Item
        {
            get
            {
                return this.item;
            }
            set
            {
                this.item = value;
            }
        }
        public bool Multiple
        {
            get
            {
                return this.multiple;
            }
            set
            {
                this.multiple = value;
            }
        }
        #endregion
    }
}

