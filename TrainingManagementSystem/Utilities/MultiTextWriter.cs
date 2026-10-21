using System;
using System.IO;
using System.Text;

namespace TrainingManagementSystem.Utilities
{
    public class MultiTextWriter : TextWriter
    {
        private TextWriter consoleWriter;
        private StreamWriter fileWriter;

        public MultiTextWriter(TextWriter consoleWriter, StreamWriter fileWriter)
        {
            this.consoleWriter = consoleWriter;
            this.fileWriter = fileWriter;
        }

        public override Encoding Encoding
        {
            get { return consoleWriter.Encoding; }
        }

        public override void Write(string? value)
        {
            consoleWriter.Write(value);
            fileWriter.Write(value);
            fileWriter.Flush();
        }

        public override void Write(char value)
        {
            consoleWriter.Write(value);
            fileWriter.Write(value);
            fileWriter.Flush();
        }

        public override void WriteLine(string? value)
        {
            consoleWriter.WriteLine(value);
            fileWriter.WriteLine(value);
            fileWriter.Flush();
        }

        public override void WriteLine()
        {
            consoleWriter.WriteLine();
            fileWriter.WriteLine();
            fileWriter.Flush();
        }

        public override void Flush()
        {
            consoleWriter.Flush();
            fileWriter.Flush();
        }
    }
}