namespace Contracts
{
    public interface IRepositoryWrapper
    {
        IDogsRepository Dogs { get; }
        void Save();
    }
}
