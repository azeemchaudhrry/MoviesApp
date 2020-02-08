namespace MovieApp.Core.Contracts
{
    public interface IItemSelectionChange<T>
    {
        void ItemSelectionChanged(T obj);
    }
}
