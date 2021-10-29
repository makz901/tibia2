namespace Team801.Tibia2.Server.Services.Contracts
{
    public interface IGameTimer
    {
        // seconds per frame
        double FrameDelta { get; set; }
    }
}