using System;
using System.Threading;
using System.Threading.Channels;
using System.Threading.Tasks;
using Team801.Tibia2.Server.Game.Contracts;
using Team801.Tibia2.Server.Services.Contracts;

namespace Team801.Tibia2.Server.Game
{
    public class GameActionsDispatcher : IGameActionsDispatcher
    {
        private readonly ChannelReader<IGameAction> _reader;
        private readonly ChannelWriter<IGameAction> _writer;

        /// <summary>
        ///     A queue responsible for process events
        /// </summary>
        public GameActionsDispatcher()
        {
            var channel = Channel.CreateUnbounded<IGameAction>(new UnboundedChannelOptions {SingleReader = true});
            _reader = channel.Reader;
            _writer = channel.Writer;
        }

        /// <summary>
        ///     Adds an event to dispatcher queue
        /// </summary>
        /// <param name="action"></param>
        /// <param name="hasPriority"></param>
        public void AddAction(IGameAction action, bool hasPriority = false)
        {
            _writer.TryWrite(action);
        }

        /// <summary>
        ///     Starts dispatcher processing queue
        /// </summary>
        /// <param name="token"></param>
        public void Start(CancellationToken token)
        {
            Task.Run(async () =>
            {
                while (await _reader.WaitToReadAsync(token))
                {
                    if (token.IsCancellationRequested) _writer.Complete();
                    // Fast loop around available jobs
                    while (_reader.TryRead(out var action))
                        if (!action.HasExpired || action.HasNoTimeout)
                            try
                            {
                                action.Invoke();
                            }
                            catch (Exception ex)
                            {
                                Console.WriteLine(ex.Message);
                                Console.WriteLine(ex.StackTrace);
                            }
                }
            }, token);
        }
    }
}