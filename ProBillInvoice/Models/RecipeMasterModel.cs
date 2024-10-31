using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProBillInvoice.Models
{
    public class RecipeMasterModel
    {
        public List<RecipeHeaderModel>RecipeHeaderList { get; set; }

        public RecipeHeaderModel RecipeHeader { get; set; }

        public RecipeDetailModel RecipeDetail { get; set; }

        public List<RecipeDetailModel> RecipeDetails { get; set; }
    }
}