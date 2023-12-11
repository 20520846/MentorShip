using MentorShip.Models;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using MongoDB.Bson;
using System.Threading.Tasks;

namespace MentorShip.Services
{
    public class PaymentService : MongoDBService
    {
        private readonly IMongoCollection<Payment> _paymentCollection;
        public PaymentService(IOptions<MongoDBSettings> mongoDBSettings) : base(mongoDBSettings)
        {
            _paymentCollection = database.GetCollection<Payment>("payment");
        }
        public async Task<Payment> GetPaymentById(string id)
        {
            var payment = await _paymentCollection.Find(u => u.Id == id).FirstOrDefaultAsync();
            return payment;
        }

        public async Task<List<Payment>> GetListAsync()
        {
            var payments = await _paymentCollection.Find(new BsonDocument()).ToListAsync();
            return payments;
        }
        public async Task CreateAsync(Payment payment)
        {
            await _paymentCollection.InsertOneAsync(payment);
            return;
        }
    }
}