using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Practicum.Domain
{
    public class MealType : BaseObject
    {

        #region Constructors
        public MealType() : base()
        {
        }

        public MealType(int iD, string name) : base(iD,name)
        {
        }
        #endregion
    }
}

