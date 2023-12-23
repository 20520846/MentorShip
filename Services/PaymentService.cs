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

        public async Task<Payment> GetPaymentById(string id)
        {
            return await _paymentCollection.Find(payment => payment.Id == id).FirstOrDefaultAsync();
        }

        public async Task<Payment> GetPaymentByMenteeApplicationId(string menteeApplicationId)
        {
            return await _paymentCollection.Find(payment => payment.MenteeApplicationId == menteeApplicationId).FirstOrDefaultAsync();
        }

        public async Task<List<Payment>> GetPaymentsByMentorId(string mentorId)
        {
            return await _paymentCollection.Find(payment => payment.MentorId == mentorId).ToListAsync();
        }

        public async Task<List<Payment>> GetPaymentsByMenteeApplicationId(string menteeApplicationId)
        {
            return await _paymentCollection.Find(payment => payment.MenteeApplicationId == menteeApplicationId).ToListAsync();
        }

        public async Task<Payment> DeletePaymentById(string id)
        {
            return await _paymentCollection.FindOneAndDeleteAsync(payment => payment.Id == id);
        }


    }
}
