﻿using libc.serial.AtDevice;
using libc.serial.Internal;

namespace libc.serial.Sim900
{
    public class AtCDeleteTest : AtCommand
    {
        private const string cmd = "at+cmgd=?\r";

        public override string ToString()
        {
            return $"Command: {cmd} - State: {State} - Response: {Responses.ConcatString(", ", "(", ")")}";
        }

        protected override void RunCommandAsync()
        {
            port.Write(cmd);
            setTimeout(1000);
        }

        protected override void OnDataUpdate(string buffer)
        {
            buffer = buffer.StartingFrom(cmd);

            if (buffer == null) return;
            var t = buffer.GetBetween("+CMGD: ", "\r\n");

            if (t != null)
            {
                AddResponse(t);
                finish(true, AtCommandStates.Completed);
            }
        }
    }
}