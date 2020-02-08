namespace MovieApp.Core.Contracts
{
    public interface IFavoriteButtonImplementor<T>
    {
        void FavoriteButtonTapped(T obj);
    }
}
