using System;
using Team801.Tibia2.Server.Services.Contracts;

namespace Team801.Tibia2.Server.Services
{
    public class GameTimer : IGameTimer
    {
        public TimeSpan FrameDeltaTime { get; set; } = TimeSpan.Zero;
    }
}