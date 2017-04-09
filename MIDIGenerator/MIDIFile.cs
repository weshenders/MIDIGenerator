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


        static readonly int[] MIDIHeader = new int[]
        {
                0x4d, 0x54, 0x68, 0x64, 0x00, 0x00, 0x00, 0x06, //MThd
		        0x00, 0x01, //Type=0
		    	0x00, 0x01, //One track
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

        public void NewTrack(int length, string key)
        {
            MIDIEvents.Add(trackHeader);
            //length=number of noteOnNoteOffs. 8 bytes each. +4 bytes for the track footer
            int trackDataLength = (length * 8) + 4;
            //calculate the higher byte of track data
            int high = trackDataLength / 256;
            //calculate the lower byte of track data
            int low = trackDataLength - (high * 256);
            MIDIEvents.Add(new int[] { 0x00, 0x00, high, low });

            for (int i=0; i<length; i++)
            {

                MIDIEvents.Add(NoteOnNoteOff(0x00, midiNotes.GetRandomCS(), 0x60, midiNotes.GetRandomNote()));
            }
            MIDIEvents.Add(trackFooter);
            FileStream writer = new FileStream("e:\\test.midi", FileMode.Create, FileAccess.Write);

            for (int i=0; i< MIDIEvents.Count; i++)
            {
                byte[] byteEvent = intArrayToByteArray(MIDIEvents[i]);
                for (int j=0; j<byteEvent.Length; j++)
                    writer.WriteByte(byteEvent[j]);
            }
            writer.Close();
        }

        private int[] NoteOnNoteOff(int timeStamp, int note, int volume, int length)
        {
            int[] noteOnOffEvent = { timeStamp, 0x90, note, volume, length, 0x90, note, 0x00 };
            return noteOnOffEvent;
        }
    }
}
