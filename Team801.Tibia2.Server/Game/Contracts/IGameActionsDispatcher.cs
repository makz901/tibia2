using System.Threading;

namespace Team801.Tibia2.Server.Game.Contracts
{
    public interface IGameActionsDispatcher
    {
        void AddAction(IGameAction action, bool hasPriority = false);

        void Start(CancellationToken token);
    }
}