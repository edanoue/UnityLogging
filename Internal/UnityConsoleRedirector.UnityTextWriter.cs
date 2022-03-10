#nullable enable

using System.IO;
using System.Text;
using UnityEngine;

namespace Edanoue.Logging.Internal
{
    partial class UnityConsoleRedirector
    {
        class UnityTextWriter : TextWriter
        {
            private readonly StringBuilder buffer = new();

            public override void Flush()
            {
                // Call UnityEngine Debug Log
                Debug.Log(buffer.ToString());
                buffer.Length = 0;
            }

            public override void Write(string? value)
            {
                buffer.Append(value);
                if (value != null)
                {
                    var len = value.Length;
                    if (len > 0)
                    {
                        var lastChar = value[len - 1];
                        if (lastChar == '\n')
                        {
                            Flush();
                        }
                    }
                }
            }

            public override void Write(char value)
            {
                buffer.Append(value);
                if (value == '\n')
                {
                    Flush();
                }
            }

            public override void Write(char[] value, int index, int count)
            {
                Write(new string(value, index, count));
            }

            public override Encoding Encoding => Encoding.Default;
        }
    }
}