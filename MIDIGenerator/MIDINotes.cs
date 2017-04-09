using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MIDIGenerator
{
    class MIDINotes
    {
        Random random = new Random();
        private int deltaTime;
        private int quarterNote;
        private int eigthNote;
        private int sixteenthNote;
        private int[] noteTypes;

        static readonly int[] CKeys = new int[]
        {
            0x00,0x0C,0x18,0x24,0x30,0x3C,0x48,0x54,0x60,0x6C
        };
        static readonly int[] CSKeys = new int[]
        {
            0x01,0x0D,0x19,0x25,0x31,0x3D,0x49,0x55,0x61,0x6D
        };
        static readonly int[] DKeys = new int[]
        {
            0x02,0x0E,0x1A,0x26,0x32,0x3E,0x4A,0x56,0x62,0x6E
        };
        static readonly int[] EKeys = new int[]
        {
            0x04,0x10,0x1C,0x28,0x34,0x40,0x4C,0x58,0x64,0x70
        };
        static readonly int[] EbKeys = new int[]
        {
            0x03,0x0F,0x1B,0x27,0x33,0x3F,0x4B,0x57,0x63,0x6F
        };
        static readonly int[] FKeys = new int[]
        {
            0x05,0x11,0x1D,0x29,0x35,0x41,0x4D,0x59,0x65,0x71
        };
        static readonly int[] FSKeys = new int[]
        {
            0x06,0x12,0x1E,0x2A,0x36,0x42,0x4E,0x5A,0x66,0x72
        };
        static readonly int[] GKeys = new int[]
        {
            0x07,0x13,0x1F,0x2B,0x37,0x43,0x4F,0x5B,0x67,0x73
        };
        static readonly int[] GSKeys = new int[]
        {
            0x08,0x14,0x20,0x2C,0x38,0x44,0x50,0x5C,0x68,0x74
        };
        static readonly int[] AKeys = new int[]
        {
            0x09,0x15,0x21,0x2D,0x39,0x45,0x51,0x5D,0x69,0x75
        };
        static readonly int[] BKeys = new int[]
        {
            0x0B,0x17,0x23,0x2F,0x3B,0x47,0x53,0x5F,0x6B,0x77
        };
        static readonly int[] BbKeys = new int[]
        {
            0x0A,0x16,0x22,0x2E,0x3A,0x46,0x52,0x5E,0x6A,0x76
        };
        private int[] notesInKey = new int[30];

        public MIDINotes(int deltaTime)
        {
            this.deltaTime = deltaTime;
            this.quarterNote = deltaTime;
            this.eigthNote = deltaTime / 2;
            this.sixteenthNote = deltaTime / 4;
            this.noteTypes = new int[]
            {
                this.quarterNote, this.eigthNote, this.sixteenthNote
            };
        }

        public void SetKey(string key)
        {
            switch (key)
            {
                case "C":
                    CKeys.CopyTo(notesInKey, 0);
                    EKeys.CopyTo(notesInKey, 10);
                    GKeys.CopyTo(notesInKey, 20);
                    break;
                case "C#":
                case "Db":
                    CSKeys.CopyTo(notesInKey, 0);
                    FKeys.CopyTo(notesInKey, 10);
                    GSKeys.CopyTo(notesInKey, 20);
                    break;
                case "D":
                    DKeys.CopyTo(notesInKey, 0);
                    FSKeys.CopyTo(notesInKey, 10);
                    AKeys.CopyTo(notesInKey, 20);
                    break;
                case "D#":
                case "Eb":
                    EbKeys.CopyTo(notesInKey, 0);
                    GKeys.CopyTo(notesInKey, 10);
                    BbKeys.CopyTo(notesInKey, 20);
                    break;
                case "E":
                    EKeys.CopyTo(notesInKey, 0);
                    GSKeys.CopyTo(notesInKey, 10);
                    BKeys.CopyTo(notesInKey, 20);
                    break;
                case "F":
                    FKeys.CopyTo(notesInKey, 0);
                    AKeys.CopyTo(notesInKey, 10);
                    CKeys.CopyTo(notesInKey, 20);
                    break;
                case "F#":
                case "Gb":
                    FSKeys.CopyTo(notesInKey, 0);
                    BbKeys.CopyTo(notesInKey, 10);
                    CSKeys.CopyTo(notesInKey, 20);
                    break;
                case "G":
                    GKeys.CopyTo(notesInKey, 0);
                    BKeys.CopyTo(notesInKey, 10);
                    DKeys.CopyTo(notesInKey, 20);
                    break;
                case "G#":
                case "Ab":
                    GSKeys.CopyTo(notesInKey, 0);
                    CKeys.CopyTo(notesInKey, 10);
                    EbKeys.CopyTo(notesInKey, 20);
                    break;
                case "A":
                    AKeys.CopyTo(notesInKey, 0);
                    CSKeys.CopyTo(notesInKey, 10);
                    EKeys.CopyTo(notesInKey, 20);
                    break;
                case "A#":
                case "Bb":
                    BbKeys.CopyTo(notesInKey, 0);
                    DKeys.CopyTo(notesInKey, 10);
                    FKeys.CopyTo(notesInKey, 20);
                    break;
                case "B":
                    BKeys.CopyTo(notesInKey, 0);
                    EbKeys.CopyTo(notesInKey, 10);
                    FSKeys.CopyTo(notesInKey, 20);
                    break;
            }
        }

        public int GetRandomNote(string key)
        {
            int randomKey;
            randomKey = notesInKey[random.Next(0, notesInKey.Length - 1)];
            return randomKey;
        }

        public int GetRandomC()
        {
            var randomC = new int[CKeys.Length + EKeys.Length + GKeys.Length];
            CKeys.CopyTo(randomC, 0);
            EKeys.CopyTo(randomC, CKeys.Length);
            GKeys.CopyTo(randomC, CKeys.Length+EKeys.Length);
            return randomC[random.Next(0, randomC.Length - 1)];
        }
        public int GetRandomCS()
        {
            var randomCS = new int[CSKeys.Length + EKeys.Length + GSKeys.Length];
            CSKeys.CopyTo(randomCS, 0);
            EKeys.CopyTo(randomCS, CKeys.Length);
            GSKeys.CopyTo(randomCS, CKeys.Length + EKeys.Length);
            return randomCS[random.Next(0, randomCS.Length - 1)];
        }

        public int GetRandomNote()
        {
            return noteTypes[random.Next(0, noteTypes.Length - 1)];
        }
    }
}
