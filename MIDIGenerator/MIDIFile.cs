using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace MIDIGenerator
{
    class MIDIFile
    {
        private static int deltaTime = 0x08;
        private MIDINotes midiNotes = new MIDINotes(deltaTime);


        static int[] MIDIHeader = new int[]
        {
                0x4d, 0x54, 0x68, 0x64, 0x00, 0x00, 0x00, 0x06, //MThd
		        0x00, 0x01, //Type=1
		    	0x00, 0x03, //two tracks
		    	0x00, deltaTime, //Delta Time Frames per Quarter Note
        };
        static readonly int[] trackHeader = new int[]
        {
                0x4D, 0x54, 0x72, 0x6B //MTrk
                
        };
        static readonly int[] trackFooter = new int[]
        {
                0x00, 0xFF, 0x2F, 0x00
        };


        List<int[]> MIDIEvents = new List<int[]>();
        
        public MIDIFile()
        {
            //Add file header
            MIDIEvents.Add(MIDIHeader);
        }

        protected static byte[] intArrayToByteArray(int[] ints)
        {
            int l = ints.Length;
            byte[] output = new byte[ints.Length];
            for (int i = 0; i < ints.Length; i++)
            {
                output[i] = (byte) ints[i];
            }
            return output;
        }

        public void NewTrack(int length, string key, int instrument, int channel, int channelNote)
        {
            if (instrument == 0x00)
            {
                MIDIHeader[11]--;
                return;
            }
            int[] instrumentArray = new int[] {0x00, channel, instrument};
            midiNotes.SetKey(key);
            MIDIEvents.Add(trackHeader);
            //length=number of noteOnNoteOffs. 8 bytes each. +4 bytes for the track footer
            int trackDataLength = (length * 8) + trackFooter.Length + instrumentArray.Length;
            //calculate the higher byte of track data
            int high = trackDataLength / 256;
            //calculate the lower byte of track data
            int low = trackDataLength - (high * 256);
            MIDIEvents.Add(new int[] { 0x00, 0x00, high, low });
            MIDIEvents.Add(instrumentArray);
            for (int i=0; i<length; i++)
            {
                MIDIEvents.Add(NoteOnNoteOff(0x00, midiNotes.GetRandomNote(), midiNotes.GetRandomVolume(), midiNotes.GetRandomNoteType(), channelNote));
            }
            MIDIEvents.Add(trackFooter);
        }

        public void WriteMIDIToFile()
        {
            FileStream writer = new FileStream("e:\\test.midi", FileMode.Create, FileAccess.Write);

            for (int i = 0; i < MIDIEvents.Count; i++)
            {
                byte[] byteEvent = intArrayToByteArray(MIDIEvents[i]);
                for (int j = 0; j < byteEvent.Length; j++)
                    writer.WriteByte(byteEvent[j]);
            }
            writer.Close();
        }

        private int[] NoteOnNoteOff(int timeStamp, int note, int volume, int length, int channelNote)
        {
            int[] noteOnOffEvent = { timeStamp, channelNote, note, volume, length, channelNote, note, 0x00 };
            return noteOnOffEvent;
        }
    }
}
