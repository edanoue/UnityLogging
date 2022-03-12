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

        #region IHandler impls

        public int Level => _level;

        public void SetLevel(int level)
        {
            _level = level;
        }

        public void SetFormatter(IFormatter formatter)
        {
            _formatter = formatter;
        }

        void IHandler.Emit(ILogRecord record)
        {
            var level = record.Level;
            var formatMessage = this.Format(record);
            // UnityEngine.Object context = record. Fixme

            if (level < (int)LogLevel.Warning)
            {
                Debug.Log(formatMessage);
                // Debug.Log(formatMessage, context);
                return;
            }

            if (level < (int)LogLevel.Error)
            {
                Debug.LogWarning(formatMessage);
                // Debug.LogWarning(formatMessage, context);
                return;
            }

            // Above error
            Debug.LogError(formatMessage);
            // Debug.LogError(formatMessage, context);
        }

        #endregion

        string Format(ILogRecord record)
        {
            IFormatter fmt;
            if (_formatter is null)
            {
                // FIXME
                fmt = new Formatter();
            }
            else
            {
                fmt = _formatter;
            }
            return fmt.Format(record);
        }

    }

}