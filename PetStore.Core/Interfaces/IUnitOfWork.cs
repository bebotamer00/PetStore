namespace PetStore.Core.Interfaces
{
    public interface IUnitOfWork
    {
        public IUserRepository UserRepository { get; }
        public IPetRepository PetRepository { get; }
    }
}
