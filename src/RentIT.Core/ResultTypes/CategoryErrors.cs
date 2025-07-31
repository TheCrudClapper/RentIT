namespace RentIT.Core.ResultTypes
{
    public static class CategoryErrors
    {
        public static readonly Error CategoryNotFound = new Error(
            404, "Category of given ID not found");

        public static readonly Error CategoryNotUnique = new Error(
            400, "Category of given Name already exists, please correct name");
    }
}
