namespace CRUDSelectorMulti
{
    public interface IElement<T>
    {
        public abstract T AddNew(string arg1, string password);
        public abstract void LoadOne(T eleme/nt);
    }
}