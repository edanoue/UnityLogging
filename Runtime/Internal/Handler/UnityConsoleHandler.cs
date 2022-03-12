#nullable enable

using UnityEngine;
using Edanoue.Logging.Interfaces;

namespace Edanoue.Logging.Internal
{
    public class UnityConsoleHandler : IHandler
    {
        private int _level;
        private IFormatter? _formatter = null;
        private readonly static IFormatter _defaultFormatter = new Formatter();

        #region Constructors

        public UnityConsoleHandler()
        {
            _level = (int)LogLevel.NotSet;
        }

        public UnityConsoleHandler(int level)
        {
            _level = level;
        }

        public UnityConsoleHandler(LogLevel level)
        {
            _level = (int)level;
        }

        #endregion

        #region IHandler impls

        public int Level => _level;

        public void SetLevel(int level)
        {
            _level = level;
        }

        public void SetLevel(LogLevel level) => SetLevel((int)level);

        public void SetFormatter(IFormatter formatter)
        {
            _formatter = formatter;
        }

        void IHandler.Emit(ILogRecord record)
        {
            var level = record.Level;
            var formatMessage = this.Format(record);

            // Get context from extra. if not exist set null
            record.TryGetExtra(CONST.UNITY_CONTEXT_KEY, out Object? context);

            if (level < (int)LogLevel.Warning)
            {
                Debug.Log(formatMessage, context);
                return;
            }

            if (level < (int)LogLevel.Error)
            {
                Debug.LogWarning(formatMessage, context);
                return;
            }

            // Above error
            Debug.LogError(formatMessage, context);
        }

        #endregion

        string Format(ILogRecord record)
        {
            IFormatter fmt = _formatter ?? _defaultFormatter;
            return fmt.Format(record);
        }

    }

}