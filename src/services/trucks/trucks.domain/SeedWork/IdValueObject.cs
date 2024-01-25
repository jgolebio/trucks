namespace trucks.domain.SeedWork
{
    public abstract class  IdValueObject : ValueObject
    {
        public Guid Value { get; }

        public IdValueObject(Guid id)
        {
            Value = id;
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Value;
        }
    }
}
