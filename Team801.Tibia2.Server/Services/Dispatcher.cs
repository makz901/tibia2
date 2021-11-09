using System;
using System.Threading;
using System.Threading.Channels;
using System.Threading.Tasks;
using Team801.Tibia2.Server.Tasks;

namespace Team801.Tibia2.Server.Services
{
    public interface IGameEventsDispatcher
    {
        void AddEvent(IGameEvent evt, bool hasPriority = false);

        void Start(CancellationToken token);
    }

    public class GameEventsDispatcher : IGameEventsDispatcher
    {
        private readonly ChannelReader<IGameEvent> _reader;
        private readonly ChannelWriter<IGameEvent> _writer;

        /// <summary>
        ///     A queue responsible for process events
        /// </summary>
        public GameEventsDispatcher()
        {
            var channel = Channel.CreateUnbounded<IGameEvent>(new UnboundedChannelOptions {SingleReader = true});
            _reader = channel.Reader;
            _writer = channel.Writer;
        }

        /// <summary>
        ///     Adds an event to dispatcher queue
        /// </summary>
        /// <param name="evt"></param>
        /// <param name="hasPriority"></param>
        public void AddEvent(IGameEvent evt, bool hasPriority = false)
        {
            _writer.TryWrite(evt);
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
                    while (_reader.TryRead(out var evt))
                        if (!evt.HasExpired || evt.HasNoTimeout)
                            try
                            {
                                evt.Action?.Invoke(); //execute event
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