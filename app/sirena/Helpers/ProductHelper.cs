using sirena.Models.DBModels;
using sirena.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace sirena.Helpers
{
    public static class ProductHelper
    {
        public static void CompareRelations(ref List<Guid> firstList, ref List<Guid> secondList)
        {

            List<Guid> oldList = firstList;
            List<Guid> newList = secondList;

            firstList = new List<Guid>();
            secondList = new List<Guid>();

            foreach (var item in oldList)
            {
                if (!newList.Equals(item))
                {
                    firstList.Add(item);
                }
            }

            foreach (var item in newList)
            {
                if (!oldList.Equals(item))
                {
                    secondList.Add(item);
                }
            }
        }
    }
}
