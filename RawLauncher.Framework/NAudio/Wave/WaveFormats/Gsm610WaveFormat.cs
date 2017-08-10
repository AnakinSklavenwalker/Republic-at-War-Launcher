﻿using System.IO;
using System.Runtime.InteropServices;

namespace RawLauncher.Framework.NAudio.Wave.WaveFormats
{
    [StructLayout(LayoutKind.Sequential, Pack = 2)]
    public abstract class Gsm610WaveFormat : WaveFormat
    {
        private readonly short samplesPerBlock;

        protected Gsm610WaveFormat()
        {
            waveFormatTag = WaveFormatEncoding.Gsm610;
            channels = 1;
            averageBytesPerSecond = 1625;
            bitsPerSample = 0; // must be zero
            blockAlign = 65;
            sampleRate = 8000;

            extraSize = 2;
            samplesPerBlock = 320;
        }

        public override void Serialize(BinaryWriter writer)
        {
            base.Serialize(writer);
            writer.Write(samplesPerBlock);
        }
    }
}
