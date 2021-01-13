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
        public Samples samples { get; private set; }

        private string selectedSample;

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
            SamplesList.SelectionChanged += SelectSample;
            Play.Click += PlaySample;
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

        private void ActiveSwitchSamplesLabel(object sender, EventArgs args) => ChangeSamplesStateLabel(true, SwitchSamplesLabel);
        private void InactiveSwitchSamplesLabel(object sender, EventArgs args) => ChangeSamplesStateLabel(false, SwitchSamplesLabel);

        private void ChangeSamplesStateLabel(bool state, Label label)
        {
            if (state) label.Content = Strings.On_State;
            else label.Content = Strings.Off_State;
        }

        private void SelectSample(object sender, RoutedEventArgs args)
        {
            object selectedItem = (sender as ListBox).SelectedItem;

            if(selectedItem != null)
            selectedSample = selectedItem.ToString();
        }

        private void PlaySample(object sender, RoutedEventArgs args)
        =>  samples.Play(selectedSample);
        
    }
}
