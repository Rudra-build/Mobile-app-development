using CodeLingo.Models;
using Plugin.Firebase.Firestore;

namespace CodeLingo.Services
{
    public class SubscriptionService
    {
        private readonly IFirestoreDb _db;
        private const string Collection = "Subscriptions";

        public SubscriptionService(IFirestoreDb db)
        {
            _db = db;
        }

        // GET current subscription for a user
        public async Task<Subscription> GetSubscriptionAsync(string userId)
        {
            var snapshot = await _db
                .Collection(Collection)
                .WhereEqualsTo("UserId", userId)
                .GetDocumentsAsync<Subscription>();

            return snapshot.FirstOrDefault() ?? new Subscription
            {
                UserId = userId,
                Plan = "free",
                IsActive = true
            };
        }

        // UPGRADE to premium (simulated)
        public async Task UpgradeAsync(string userId)
        {
            var sub = await GetSubscriptionAsync(userId);

            sub.Plan = "premium";
            sub.StartDate = DateTime.UtcNow;
            sub.EndDate = DateTime.UtcNow.AddMonths(1);
            sub.IsActive = true;
            sub.PaymentStatus = "simulated";

            if (sub.Id == null)
                await _db.Collection(Collection).AddDocumentAsync(sub);
            else
                await _db.Collection(Collection).Document(sub.Id).SetDataAsync(sub);
        }

        // DOWNGRADE to free
        public async Task DowngradeAsync(string userId)
        {
            var sub = await GetSubscriptionAsync(userId);

            sub.Plan = "free";
            sub.EndDate = DateTime.UtcNow;
            sub.PaymentStatus = "simulated";

            await _db.Collection(Collection).Document(sub.Id).SetDataAsync(sub);
        }

        // CHECK if user can access a feature
        public async Task<bool> CanAccessFeatureAsync(string userId, string feature)
        {
            var sub = await GetSubscriptionAsync(userId);

            return feature switch
            {
                "ai_quiz"           => sub.IsPremium,
                "analytics"         => sub.IsPremium,
                "unlimited_quizzes" => sub.IsPremium,
                _                   => true
            };
        }
    }
}
