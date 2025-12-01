namespace Contract.Interfaces.IRepository
{
    public interface IRepositoryManager
    {
        IEventRepository Event { get; }
        Task SaveAsync();
    }
}
