using TrackingAdmin.Validators;

namespace TrackingAdmin.ViewModels
{
    public class RoadMapFiltersRequestViewModel
    {
        public int CurrentPage { get; set; } = 1;
        public int SizeLimit { get; set; } = 10;
        
        [RoadMapStatusValidation]
        public string Status { get; set; } = string.Empty;
        public string License { get; set; } = string.Empty;
    }
}
