using System;
using System.Windows.Controls;
using HardwareHelperLib;
using System.Diagnostics;

namespace DisplaySwitcher
{
    /// <summary>
    /// Interaktionslogik für Switcher.xaml
    /// </summary>
    public partial class Switcher : UserControl
    {
        Process gopher;
        String[] audioDevice = { "Realtek High Definition Audio" };
        HH_Lib hwh;


        public Switcher()
        {
            InitializeComponent();
            hwh = new HH_Lib();
            hwh.GetAll();
        }

        private void DisableAudioDevice()
        {
            hwh.SetDeviceState(audioDevice, false);
        }

        private void EnableAudioDevice()
        {
            hwh.SetDeviceState(audioDevice, true);
        }


        private void SwitchToInternalDisplay()
        {

            //DisableAudioDevice();
            using (Process processDisplaySwitch = new Process())
            {
                processDisplaySwitch.StartInfo.FileName = "C:/Windows/Sysnative/DisplaySwitch.exe";
                processDisplaySwitch.StartInfo.Arguments = "/internal";
                processDisplaySwitch.Start();
            }
        }

        private void SwitchToExternalDisplay()
        {
            //EnableAudioDevice();
            using (Process processDisplaySwitch = new Process())
            {
                processDisplaySwitch.StartInfo.FileName = "C:/Windows/Sysnative/DisplaySwitch.exe";
                processDisplaySwitch.StartInfo.Arguments = "/external";
                processDisplaySwitch.Start();
            }
        }

        private void StartGopher()
        {
            if (Process.GetProcessesByName("gopher").Length == 0)
            {
                using (gopher = new Process())
                {
                    gopher.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
                    gopher.StartInfo.FileName = "G:/Downloads/Gopher.exe";
                    gopher.StartInfo.Arguments = "/external";
                    gopher.Start();
                }
            }
        }


        private void StopGopher()
        {
            try
            {
                gopher.CloseMainWindow();
                gopher.Close();
                gopher.Kill();
            }
            catch (Exception)
            {

            }
        }
        private void btnSwitchToInternal_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            StartGopher();
            SwitchToInternalDisplay();
        }

        private void btnSwitchToExternal_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            StopGopher();
            SwitchToExternalDisplay();
        }
    }
}
