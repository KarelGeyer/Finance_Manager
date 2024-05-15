namespace Common.Models.ProductModels.Loans
{
    public class CreateLoan
    {
        /// <summary>
        /// Gets or sets the owner id of the loan.
        /// </summary>
        public int OwnerId { get; set; }

        /// <summary>
        /// Gets or sets the Id of a user who this is owned to of the loan.
        /// 0 means the user is not in the system
        /// </summary>
        public int OwnToId { get; set; }

        /// <summary>
        /// Gets or sets the value of the loan.
        /// </summary>
        public float Value { get; set; }

        /// <summary>
        /// Gets or sets the name of the loan.
        /// </summary>
        public string Name { get; set; }
    }
}
