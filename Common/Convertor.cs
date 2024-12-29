using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common
{
    public static class Convertor
    {
        public static string CategoryIdToString(int categoryId)
        {
            switch (categoryId)
            {
                case 1:
                    return "Real Estate";
                case 2:
                case 6:
                    return "Investment";
                case 3:
                case 8:
                case 15:
                    return "Other";
                case 4:
                    return "Job";
                case 5:
                    return "Freelancing";
                case 7:
                    return "Passive Income";
                case 9:
                    return "Groceries";
                case 10:
                    return "Householding";
                case 11:
                    return "Free Time";
                case 12:
                    return "Clothings";
                case 13:
                    return "Transportation";
                case 14:
                    return "Traveling";
                default:
                    return "Error";
            }
        }

        public static int CategoryIdNameToInt(string categoryId, int categoryGroup)
        {
            if ((categoryGroup > 3 || categoryGroup < 1) || categoryId == string.Empty)
                throw new Exception("Both category id name and group has to be filled in");

            if (categoryGroup == 1)
            {
                switch (categoryId)
                {
                    case "Real Estate":
                        return 1;
                    case "Investment":
                        return 2;
                    case "Other":
                        return 3;
                    default:
                        throw new Exception("Wrong combination of category name and group");
                }
            }

            if (categoryGroup == 2)
            {
                switch (categoryId)
                {
                    case "Investment":
                        return 6;
                    case "Other":
                        return 8;
                    case "Job":
                        return 4;
                    case "Freelancing":
                        return 5;
                    case "Passive Income":
                        return 7;
                    default:
                        throw new Exception("Wrong combination of category name and group");
                }
            }

            if (categoryGroup == 3)
            {
                switch (categoryId)
                {
                    case "Other":
                        return 3;
                    case "Groceries":
                        if (categoryGroup == 3)
                            throw new Exception("Wrong combination of category name and group");
                        return 9;
                    case "Householding":
                        if (categoryGroup == 3)
                            throw new Exception("Wrong combination of category name and group");
                        return 10;
                    case "Free Time":
                        if (categoryGroup == 3)
                            throw new Exception("Wrong combination of category name and group");
                        return 11;
                    case "Clothings":
                        if (categoryGroup == 3)
                            throw new Exception("Wrong combination of category name and group");
                        return 12;
                    case "Transportation":
                        if (categoryGroup == 3)
                            throw new Exception("Wrong combination of category name and group");
                        return 13;
                    case "Traveling":
                        if (categoryGroup == 3)
                            throw new Exception("Wrong combination of category name and group");
                        return 14;
                    default:
                        throw new Exception("Wrong combination of category name and group");
                }
            }

            throw new Exception("Something went wrong");
        }
    }
}
