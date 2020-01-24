namespace Nudes.Identity
{
    public class ConsentOptions
    {
        public bool EnableOfflineAccess { get; set; } = true;
        public string OfflineAccessDisplayName { get; set; } = "Offline Access";
        public string OfflineAccessDescription { get; set; } = "Access to your applications and resources, even when you are offline";

        public string MustChooseOneErrorMessage { get; set; } = "You must pick at least one permission";
        public string InvalidSelectionErrorMessage { get; set; } = "Invalid selection";
    }
}
