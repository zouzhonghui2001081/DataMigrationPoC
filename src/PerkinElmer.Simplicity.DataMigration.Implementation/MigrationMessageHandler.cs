
using System;
using System.Threading.Tasks;
using System.Threading.Tasks.Dataflow;

namespace PerkinElmer.Simplicity.DataMigration.Implementation
{
    internal class MigrationMessageHandler : ITargetBlock<string>
    {
        private readonly ActionBlock<string> _targetMessageHandler;

        public MigrationMessageHandler()
        {
            _targetMessageHandler = new ActionBlock<string>(HandlerMessage);
        }

        #region ITargetBlock<string> members

        public DataflowMessageStatus OfferMessage(DataflowMessageHeader messageHeader, string messageValue, ISourceBlock<string> source,
            bool consumeToAccept)
        {
            return ((ITargetBlock<string>)_targetMessageHandler).OfferMessage(messageHeader, messageValue, source, consumeToAccept);
        }
        #endregion

        #region IDataflowBlock members
        public void Complete()
        {
            _targetMessageHandler.Complete();
        }

        public void Fault(Exception error)
        {
            ((IDataflowBlock)_targetMessageHandler).Fault(error);
        }

        public Task Completion => _targetMessageHandler.Completion;

        #endregion

        private void HandlerMessage(string message)
        {
            //TODO : Handler process update, migration audit message here.
        }

    }
}
