namespace Chef.Accounts
{
    /// <summary>
    /// Account status, Banned or Approved.
    /// </summary>
    /// <remarks>
    /// A new user is automatically Approved; a mis-behaved user is Banned.
    /// </remarks>
    public enum EAccountStatus : byte
    {
        /// <summary>
        /// Since we are not doing deletes on user accounts, Banned is equivalent
        /// to deleting an account.  If user is banned, 
        /// - he cannot login
        /// - his profile page cannot be viewed 
        /// </summary>
        Banned = 0,
        /// <summary>
        /// OK status, the default.
        /// </summary>
        Approved = 1,
    }
}
