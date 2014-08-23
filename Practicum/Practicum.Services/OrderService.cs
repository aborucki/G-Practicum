using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Practicum.Domain.ServiceInterfaces;
using Practicum.Domain;
using Microsoft.Practices.Unity;

namespace Practicum.Services
{
    public class OrderService : BaseService, IOrderService
    {
        
        private List<DishType> dishTypes;
        private List<MealType> mealTypes;

        #region Constructors
        //parameterless constructor is used only because of the localhost web service issues in the development environment
        public OrderService() : base()
        {
            //In a real world app, the Dishtypes would be read in from a look up table
            mealTypes = new List<MealType>();
            mealTypes.Add(new MealType(1, "morning"));
            mealTypes.Add(new MealType(2, "night"));
        }
        #endregion

        #region Methods
        public void SetDishChoice(MealType meal)
        {
            dishTypes = new List<DishType>();
            if (meal.Name == "morning")
            {
                dishTypes.Add(new DishType(1, "entrée", "eggs",false));
                dishTypes.Add(new DishType(2, "side", "toast", false));
                dishTypes.Add(new DishType(3, "drink", "coffee", true));
                dishTypes.Add(new DishType(4, "dessert", null, false));
            }
            else
            {
                dishTypes.Add(new DishType(1, "entrée", "steak", false));
                dishTypes.Add(new DishType(2, "side", "potato", true));
                dishTypes.Add(new DishType(3, "drink", "wine", false));
                dishTypes.Add(new DishType(4, "dessert", "cake", false));
            }

        }

        public string ProcessOrder(string inputStr)
        {
            string[] input = null;
            string[] delim = new string[] { "," };
            input = inputStr.Split(delim, StringSplitOptions.None);

            MealType chosenMeal = null;
            string output = "";
            List<DishType> chosenDishTypes = new List<DishType>();
            bool invalidItem = false;

            try
            {
                //check for correct mealtype
                if (!this.MealTypes.Any(c => c.Name == input[0]))
                {
                    output = input[0] + " is invalid - error!";
                    throw new Exception();
                }
                else
                {
                    chosenMeal = mealTypes.FirstOrDefault(c => c.Name == input[0]);
                    SetDishChoice(chosenMeal);
                }

                //Get each order item and process
                int index = 0;
                foreach (string item in input)
                {
                    if (index != 0)
                    {
                        int itemNum;
                        if (Int32.TryParse(item, out itemNum))
                        {
                            DishType chosenDish = dishTypes.FirstOrDefault(c => c.ID == itemNum);
                            if (chosenDish == null)
                                invalidItem = true;
                            else
                            {
                                if (chosenDish.Item != null)
                                    chosenDishTypes.Add(chosenDish);
                                else
                                    invalidItem = true;
                            }
                        }
                        else
                        {
                            invalidItem = true;
                            throw new Exception();
                        }
                    }
                    index++;
                }

                //Sort and setup output
                chosenDishTypes = chosenDishTypes.OrderBy(o => o.ID).ToList();
                int lastDish = 0;
                int itemCount = 1;
                int previousMultiple = 0;
                bool multipleError = false;
                bool inMultiple = true;

                foreach (DishType sortedDish in chosenDishTypes)
                {
                    if (!multipleError)
                    {
                        //multiple
                        if (lastDish == sortedDish.ID)
                        {
                            inMultiple = true;
                            previousMultiple++;
                            //ok for multiple
                            if (sortedDish.Multiple)
                            {
                                if (previousMultiple < 2)
                                {
                                    if (itemCount == 1)
                                        output = sortedDish.Item;
                                    else
                                        output = output + ", " + sortedDish.Item;
                                }
                            }
                            else
                            {
                                multipleError = true;
                            }
                        }
                        //Single Item
                        else
                        {
                            inMultiple = false;
                            if (!inMultiple)
                            {
                                //deal with multiple value setting
                                if (previousMultiple > 1)
                                {
                                    output = output + "(x" + previousMultiple.ToString() + ")";
                                    previousMultiple = 0;
                                }
                            }
                            previousMultiple = 1;
                            if (itemCount == 1)
                                output = sortedDish.Item;
                            else
                                output = output + ", " + sortedDish.Item;
                        }

                        lastDish = sortedDish.ID;
                        //previousMultiple++;
                        itemCount++;
                    }
                }

                if (previousMultiple > 1 && !multipleError)
                    output = output + "(x" + previousMultiple.ToString() + ")";
                

                //Invalid item
                if (multipleError)
                    output = output + ", error";
                else
                    if (invalidItem)
                        output = output + ", error";
            }
            catch (Exception ex) 
            {}

            return output;
        }

        #endregion

        [Dependency]
        public List<DishType> DishTypes
        {
            get { return dishTypes; }
        }

        [Dependency]
        public List<MealType> MealTypes
        {
            get { return mealTypes; }
        }

    } 
}
