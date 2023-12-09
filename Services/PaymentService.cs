using MentorShip.Models;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MentorShip.Services
{
    public class PaymentService : MongoDBService
    {
        private readonly IMongoCollection<Payment> _paymentCollection;

        public PaymentService(IOptions<MongoDBSettings> mongoDBSettings) : base(mongoDBSettings)
        {
            _paymentCollection = database.GetCollection<Payment>("payments");
        }

        public async Task<Payment> CreatePayment(Payment payment)
        {
          
            await _paymentCollection.InsertOneAsync(payment);
            return payment;
        }

        public async Task<List<Payment>> GetAllPayments()
        {
            return await _paymentCollection.Find(payment => true).ToListAsync();
        }

        public async Task<List<Payment>> GetPaymentsByUserId(string userId)
        {
            return await _paymentCollection.Find(payment => payment.MenteeId == userId).ToListAsync();
        }
    }
}
