namespace HireRank.Application.Filtering
{
    public class SortingViewModel
    {
        private string _sortingOrder;

        public string SortingProperty { get; set; }

        public string SortingOrder {
            get => _sortingOrder ?? "desc";
            set => _sortingOrder = value ?? "desc";
        }
    }
}
