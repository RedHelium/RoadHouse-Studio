using RoadHouse_Studio.Models;
using RoadHouse_Studio.Resources;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;

namespace RoadHouse_Studio.Pages
{
    
    public partial class SamplesPage : Page
    {
        private Samples samples;

        public SamplesPage()
        {
            InitializeComponent();
            Init();
            InitEvents();
            InitViewerEvents();
        }

        private void Init()
        {
            samples = new Samples();
        }

        private void InitEvents()
        {
            samples.ActivateSamplesEvent += ActiveSwitchSamplesLabel;
            samples.DiactivateSamplesEvent += InactiveSwitchSamplesLabel;
            samples.OpenSamplesFolderEvent += OutputAllSamples;
            samples.RefreshSamplesEvent += OutputAllSamples;
        }

        private void InitViewerEvents()
        {
            SwitchSamplesState.Click += SwitchSamples;
            Refresh.Click += RefreshSamples;
            SelectPath.Click += OpenSamplesFolder;
        }

        private void SwitchSamples(object sender, RoutedEventArgs args)
        {
            bool? isChecked = (sender as ToggleButton).IsChecked;

            if (isChecked ?? false) samples.ActivateSamples();
            else
                samples.DiactivateSamples();
            
        }

        private void OutputAllSamples(object sender, EventArgs args)
        {
            if (SamplesList.Items.Count > 0) SamplesList.Items.Clear();

            for (ushort sampleIndex = 0; sampleIndex < samples.sampleFilenames.Length; sampleIndex++)
            {
                AddSampleInList(samples.sampleFilenames[sampleIndex]);
            }
        }
        private void AddSampleInList(string sampleName) => SamplesList.Items.Add(sampleName);

        private void OpenSamplesFolder(object sender, EventArgs args) => samples.Open();

        private void RefreshSamples(object sender, EventArgs args) => samples.Refresh();

        private void ActiveSwitchSamplesLabel(object sender, EventArgs args) => ChangeSwitchLabel(true, SwitchSamplesLabel);
        private void InactiveSwitchSamplesLabel(object sender, EventArgs args) => ChangeSwitchLabel(false, SwitchSamplesLabel);

        private void ChangeSwitchLabel(bool state, Label label)
        {
            if (state) label.Content = Strings.On_State;
            else label.Content = Strings.Off_State;
        }
    }
}
