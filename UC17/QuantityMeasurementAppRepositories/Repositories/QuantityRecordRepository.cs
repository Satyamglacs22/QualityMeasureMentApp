using QuantityMeasurementAppModels.Entities;
using QuantityMeasurementAppRepositories.Context;
using QuantityMeasurementAppRepositories.Interfaces;

namespace QuantityMeasurementAppRepositories.Repositories
{
    public class QuantityRecordRepository : IQuantityRecordRepository
    {
        private readonly AppDbContext _db;

        public QuantityRecordRepository(AppDbContext db)
        {
            _db = db;
        }

        public void Save(QuantityMeasurementEntity record)
        {
            _db.QuantityMeasurements.Add(record);
            _db.SaveChanges();
        }

        public List<QuantityMeasurementEntity> GetAll()
            => LatestFirst(_db.QuantityMeasurements);

        public List<QuantityMeasurementEntity> GetByOperation(string operation)
            => LatestFirst(_db.QuantityMeasurements
                .Where(r => r.Operation == operation));

        public List<QuantityMeasurementEntity> GetByMeasurementType(string measurementType)
            => LatestFirst(_db.QuantityMeasurements
                .Where(r => r.MeasurementType == measurementType));

        public List<QuantityMeasurementEntity> GetErrorHistory()
            => LatestFirst(_db.QuantityMeasurements
                .Where(r => r.IsError));

        public int GetOperationCount(string operation)
            => _db.QuantityMeasurements
                .Count(r => r.Operation == operation && !r.IsError);

        public List<QuantityMeasurementEntity> GetByCreatedAfter(DateTime cutoff)
            => LatestFirst(_db.QuantityMeasurements
                .Where(r => r.CreatedAt > cutoff));

        private static List<QuantityMeasurementEntity> LatestFirst(
            IQueryable<QuantityMeasurementEntity> query)
            => query.OrderByDescending(r => r.CreatedAt).ToList();
    }
}