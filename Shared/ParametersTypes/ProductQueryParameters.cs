using System.ComponentModel;

namespace Shared.ParametersTypes
{
    public class ProductQueryParameters
    {

        public ProductSortOptions? Sort { get; set; }

        //--> using an enum for the Sort property is a better approach for
        //several reasons:
        //1- Type Safety:
        //2- Readability and Maintainability
        //3- Refactoring
        //4- Avoids Errors



        public string? Search { get; set; }


        public int? BrandId { get; set; }
        public int? TypeId { get; set; }
        public int PageIndex { get; set; } = 1; // optional for future paging


        private const int MaxPageSize = 10;


        public const int DefaultPageSize = 5;


        private int _PageSize = DefaultPageSize;


        public int PageSize
        {


            get { return _PageSize; }


            set { _PageSize = value > MaxPageSize ? DefaultPageSize : value; }

        }


    }


    public enum ProductSortOptions
    {


        [Description("Sort by Price Ascending")]
        PriceAsc,

        [Description("Sort by Price Descending")]
        PriceDesc,

        [Description("Sort by Name Ascending")]
        NameAsc,

        [Description("Sort by Name Descending")]
        NameDesc
    }
}
