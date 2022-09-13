using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace XmlReaderTestXamarinForms
{
    public partial class MainPage : ContentPage
    {
        private MemoryStream _ms;
        public MainPage()
        {
            InitializeComponent();
        }

        private async void Load_Clicked(object sender, EventArgs e)
        {
            using (Stream fileStream = await FileSystem.OpenAppPackageFileAsync("Test.xml"))
            {
                _ms = new MemoryStream();
                fileStream.CopyTo(_ms);
            }
            await DisplayAlert("loaded", "ready to test", "OK");
        }

        private async void Test_Clicked(object sender, EventArgs e)
        {
            _ms.Position = 0;
            var sw = Stopwatch.StartNew();

            using (var reader = XmlReader.Create(_ms))
            {
                reader.MoveToContent();

                reader.Skip();
            }
            sw.Stop();
            await DisplayAlert("time", "read took " + sw.ElapsedMilliseconds, "OK");
        }
    }
}
