using System;
using System.Diagnostics;
using System.Threading;
using System.Speech.Synthesis;

namespace Talking_PerfMon
{
    public class TTSEngine
    {
        public TTSEngine() {}

        public void RunJarvisEngine()
        {

            /// <summary>
            /// This method creates and implements our new synthesizer
            /// and creates new performance counters for cpu and ram
            /// </summary>

            #region Instantiation 
            SpeechSynthesizer synth = new SpeechSynthesizer();
            // Creating Performance Counters that get the current CPU usage and current memory Available
            PerformanceCounter cpuMonitor = new PerformanceCounter("Processor Information", "% Processor Utility","_Total");
            PerformanceCounter memMonitor = new PerformanceCounter("Memory", "Available MBytes");
            synth.Speak("Welcome to talking perf mon version one point O");
            #endregion //Creating Things and initializing the tts engine
            #region While Loop
            while (true) //While loop that runs over the performance counters and speaks them out to the user
            {
                float cpuMonitorValue = cpuMonitor.NextValue();
                float memMonitorValue = memMonitor.NextValue();
                Console.WriteLine($"Available Percentage of CPU: {Convert.ToInt32(cpuMonitorValue)}%");
                Console.WriteLine($"Available Percentage of CPU: {Convert.ToInt32(memMonitorValue)} MB");
                #region Logic
                if (cpuMonitorValue > 80.0f)
                {
                    synth.Speak($"You only have {Convert.ToInt32(100 - cpuMonitorValue)} of CPU left!");
                } 
                else if (memMonitorValue < 1000.0f)
                {
                    synth.Speak($"You only have {memMonitorValue} megabytes of memory left!");
                }
                #endregion
                Thread.Sleep(2500);
                
            }
            #endregion
        }
    }
}
