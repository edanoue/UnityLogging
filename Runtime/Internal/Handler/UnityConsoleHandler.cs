#nullable enable

using Edanoue.Logging.Interfaces;
using UnityEngine;

namespace Edanoue.Logging.Internal
{
    public class UnityConsoleHandler : IHandler
    {
        private static readonly IFormatter DefaultFormatter = new Formatter();
        private IFormatter? _formatter;

        private string Format(ILogRecord record)
        {
            var fmt = _formatter ?? DefaultFormatter;
            return fmt.Format(record);
        }

        #region Constructors

        public UnityConsoleHandler()
        {
            Level = (int) LogLevel.NotSet;
        }

        public UnityConsoleHandler(int level)
        {
            Level = level;
        }

        public UnityConsoleHandler(LogLevel level)
        {
            Level = (int) level;
        }

        #endregion

        #region IHandler impls

        public int Level { get; private set; }

        public void SetLevel(int level)
        {
            Level = level;
        }

        public void SetLevel(LogLevel level)
        {
            SetLevel((int) level);
        }

        public void SetFormatter(IFormatter formatter)
        {
            _formatter = formatter;
        }

        void IHandler.Emit(ILogRecord record)
        {
            var level = record.Level;
            var formatMessage = Format(record);

            // Get context from extra. if not exist set null
            record.TryGetExtra(CONST.UNITY_CONTEXT_KEY, out Object? context);

            if (level < (int) LogLevel.Warning)
            {
                Debug.Log(formatMessage, context);
                return;
            }

            if (level < (int) LogLevel.Error)
            {
                Debug.LogWarning(formatMessage, context);
                return;
            }

            // Above error
            Debug.LogError(formatMessage, context);
        }

        #endregion
    }
}