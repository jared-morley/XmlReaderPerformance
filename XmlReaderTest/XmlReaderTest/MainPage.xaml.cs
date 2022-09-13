using System.Diagnostics;
using System.Xml;

namespace XmlReaderTest;

public partial class MainPage : ContentPage
{
    private MemoryStream _ms;
    public MainPage()
    {
        InitializeComponent();
    }

    private async void Load(object sender, EventArgs e)
    {
        using (Stream fileStream = await FileSystem.Current.OpenAppPackageFileAsync("Test.xml"))
        {
            _ms = new MemoryStream();
            fileStream.CopyTo(_ms);
        }
        await DisplayAlert("loaded", "ready to test", "OK");
    }

    private async void TestReader(object sender, EventArgs e)
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

