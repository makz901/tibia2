using System;

namespace Team801.Tibia2.Server.Services.Contracts
{
    public interface IGameTimer
    {
        TimeSpan FrameDeltaTime { get; set; }
    }
}