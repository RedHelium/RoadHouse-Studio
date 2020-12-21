using Ookii.Dialogs.Wpf;
using RoadHouse_Studio.Resources;
using System;
using System.IO;

namespace RoadHouse_Studio.Models
{
    public sealed class Samples
    {
        private string pathToSamples; //TODO: Save this field in file
        
        public string[] sampleFilenames { get; private set; }

        public EventHandler ActivateSamplesEvent;
        public EventHandler DiactivateSamplesEvent;
        public EventHandler RefreshSamplesEvent;
        public EventHandler OpenSamplesFolderEvent;

        public void ActivateSamples() => ActivateSamplesEvent?.Invoke(this, EventArgs.Empty);
        public void DiactivateSamples() => DiactivateSamplesEvent?.Invoke(this, EventArgs.Empty);
        public void Refresh()
        {
            LoadFiles();
            RefreshSamplesEvent?.Invoke(this, EventArgs.Empty);
        }

        public void Open()
        {
            OpenDialogFolder();
            LoadFiles();
            
            if(!string.IsNullOrEmpty(pathToSamples) && Directory.Exists(pathToSamples))
                OpenSamplesFolderEvent?.Invoke(this, EventArgs.Empty);

        }

        private void LoadFiles()
        {
            if (string.IsNullOrEmpty(pathToSamples) || !Directory.Exists(pathToSamples)) return;

            sampleFilenames = Directory.GetFiles(pathToSamples, Strings.Sound_Ext);
        }

        private void OpenDialogFolder()
        {
            VistaFolderBrowserDialog dlg = new VistaFolderBrowserDialog();
            dlg.ShowNewFolderButton = true;

            if (dlg.ShowDialog() ?? true)
            {
                pathToSamples = dlg.SelectedPath;
            }
            else pathToSamples = string.Empty;

        }
    }
}
