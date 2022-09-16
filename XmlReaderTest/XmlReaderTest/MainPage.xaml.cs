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

    private async void TestReader(object sender, EventArgs e)
    {
        if (_ms == null)
        {
            using (Stream fileStream = await FileSystem.Current.OpenAppPackageFileAsync("Test.xml"))
            {
                _ms = new MemoryStream();
                fileStream.CopyTo(_ms);
            }
        }
        _ms.Position = 0;
        var sw = Stopwatch.StartNew();

        using (var reader = XmlReader.Create(_ms))
        {
            reader.MoveToContent();

            while (reader.Read())
            {
                //hi
            }
        }
        sw.Stop();
        await DisplayAlert("time", "read took " + sw.ElapsedMilliseconds, "OK");
    }
}

