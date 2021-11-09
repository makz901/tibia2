namespace Team801.Tibia2.Server.Events
{
    public abstract class ServerEvents<TObject>
    {
        public abstract void Add(TObject obj);
        public abstract void Remove(TObject obj);
    }
}