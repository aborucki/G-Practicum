using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Practicum.Domain.ServiceInterfaces
{

    //This interface is in the domain project so that if another seperate service project is created, the referencing of the interfaces works out. It could be in it's own project too
    public interface IOrderService
    {
        List<DishType> DishTypes {get;}
        List<MealType> MealTypes { get; }
        string ProcessOrder(string input);
        
    }
}
