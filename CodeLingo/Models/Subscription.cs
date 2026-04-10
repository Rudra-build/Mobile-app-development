namespace CodeLingo.Models
{
    public class Subscription
    {
        public string Id { get; set; }
        public string UserId { get; set; }
        public string Plan { get; set; } = "free"; // "free" or "premium"
        public DateTime StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public bool IsActive { get; set; } = true;
        public string PaymentStatus { get; set; } = "simulated";

        // Helper property
        public bool IsPremium => Plan == "premium" && IsActive;
    }
}
