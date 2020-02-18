using PizzaShop.IRepository;
using PizzaShop.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace PizzaShop.Repositories
{
    public class FoodTypeRepository:Repository<MatrattTyp>, IFoodType
    {
        public TomasosContext Db
        {
            get { return DatabaseContext as TomasosContext; }
        }
        public FoodTypeRepository(TomasosContext context)

            : base(context) { }



        public MatrattTyp Edit(MatrattTyp editedFoodType)
        {
            var OldFoodtype = Get(editedFoodType.MatrattTyp1);
            Db.Entry(OldFoodtype).CurrentValues.SetValues(editedFoodType);
            Db.SaveChanges();
            return editedFoodType;
        }
    }
}
