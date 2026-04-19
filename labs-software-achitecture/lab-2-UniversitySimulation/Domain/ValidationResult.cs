namespace UniversitySimulation.Domain
{
    public sealed class ValidationResult
    {
        public bool IsSuccess { get; }
        public string Reason { get; }

        private ValidationResult(bool isSuccess, string reason)
        {
            IsSuccess = isSuccess;
            Reason = reason;
        }

        public static ValidationResult Success() =>
            new ValidationResult(true, null);

        public static ValidationResult Failure(string reason) =>
            new ValidationResult(false, reason);

        public override string ToString() =>
            IsSuccess ? "OK" : Reason;
    }
}
