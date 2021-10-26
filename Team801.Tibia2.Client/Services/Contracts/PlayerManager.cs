using Team801.Tibia2.Core.Models;

namespace Team801.Tibia2.Client.Services.Contracts
{
    public class PlayerManager : IPlayerManager
    {
        public Player Player { get; } = new Player();
    }
}