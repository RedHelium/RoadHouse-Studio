using NAudio.Wave;
using Ookii.Dialogs.Wpf;
using RoadHouse_Studio.Resources;
using System;
using System.IO;
using System.Windows;

namespace RoadHouse_Studio.Models
{
    public sealed class Samples
    {

        public string[] sampleFilenames { get; private set; }

        public EventHandler ActivateSamplesEvent;
        public EventHandler DiactivateSamplesEvent;
        public EventHandler RefreshSamplesEvent;
        public EventHandler OpenSamplesFolderEvent;
        public EventHandler FoundSampleEvent;
        public EventHandler NotFoundSampleEvent;

        public void ActivateSamples() => ActivateSamplesEvent?.Invoke(this, EventArgs.Empty);
        public void DiactivateSamples() => DiactivateSamplesEvent?.Invoke(this, EventArgs.Empty);
       
        public void Refresh()
        {
            LoadFiles();

            if (!string.IsNullOrEmpty(MainSettings.Default.PathToSamples) && Directory.Exists(MainSettings.Default.PathToSamples))
                RefreshSamplesEvent?.Invoke(this, EventArgs.Empty);

        }

        public void Open()
        {
            OpenDialogFolder();
            LoadFiles();
            
            if(!string.IsNullOrEmpty(MainSettings.Default.PathToSamples) && Directory.Exists(MainSettings.Default.PathToSamples))
                OpenSamplesFolderEvent?.Invoke(this, EventArgs.Empty);

        }

        private void LoadFiles()
        {
            if (string.IsNullOrEmpty(MainSettings.Default.PathToSamples) || !Directory.Exists(MainSettings.Default.PathToSamples)) return;

            try
            {
                sampleFilenames = Directory.GetFiles(MainSettings.Default.PathToSamples, Strings.Sound_Ext);
            }
            catch(IOException) { MessageBox.Show("[Сэмплы] " + Strings.Directory_Exception_Message); }
        }

        private void OpenDialogFolder()
        {
            VistaFolderBrowserDialog dlg = new VistaFolderBrowserDialog();
            dlg.ShowNewFolderButton = true;

            if (dlg.ShowDialog() ?? true)
            {
                MainSettings.Default.PathToSamples = dlg.SelectedPath;
            }
            else MainSettings.Default.PathToSamples = string.Empty;

            MainSettings.Default.Save();
        }

        public void Play(string path)
        {
            if (string.IsNullOrEmpty(path)) return;

            Mp3FileReader reader = new Mp3FileReader(path);
            WaveOut waveOut = new WaveOut();
            waveOut.Init(reader);
            waveOut.Play();
        }
        
        public void Find(string name)
        {
            string samplePath = FindSample(name);

            if (!string.IsNullOrEmpty(samplePath))
            {
                Play(samplePath);
                FoundSampleEvent?.Invoke(this, EventArgs.Empty);
            }
            else NotFoundSampleEvent?.Invoke(this, EventArgs.Empty);
                
        }

        private string FindSample(string name)
        {
            for (ushort sampleIndex = 0; sampleIndex < sampleFilenames.Length; sampleIndex++)
            {
                if (sampleFilenames[sampleIndex].Contains(name)) return sampleFilenames[sampleIndex];
            }

            return string.Empty;
        }
    }
}
