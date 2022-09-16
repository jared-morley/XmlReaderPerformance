using System.Diagnostics;
using System.Xml;

namespace IOS;

[Register ("AppDelegate")]
public class AppDelegate : UIApplicationDelegate {
	public override UIWindow? Window {
		get;
		set;
    }
    private MemoryStream _ms;

    public override bool FinishedLaunching (UIApplication application, NSDictionary launchOptions)
	{
		// create a new window instance based on the screen size
		Window = new UIWindow (UIScreen.MainScreen.Bounds);

		// create a UIViewController with a single UILabel
		var vc = new UIViewController ();

        var testButton = new UIButton(new CGRect(100, 200, 100, 45))
        {
            BackgroundColor = UIColor.Blue,
            AutoresizingMask = UIViewAutoresizing.All,
        };
        testButton.SetTitle("Test", UIControlState.Normal);
        testButton.TouchUpInside += TestButton_TouchUpInside;
        vc.View!.AddSubview(testButton);


        Window.RootViewController = vc;

		// make the window visible
		Window.MakeKeyAndVisible ();

		return true;
	}
    private void TestButton_TouchUpInside(object? sender, EventArgs e)
    {
        if (_ms ==null)
        {
            using (Stream fileStream = System.IO.File.OpenRead("Test.xml"))
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

        //Create Alert
        var okAlertController = UIAlertController.Create("time", "read took " + sw.ElapsedMilliseconds, UIAlertControllerStyle.Alert);

        //Add Action
        okAlertController.AddAction(UIAlertAction.Create("OK", UIAlertActionStyle.Default, null));

        // Present Alert
        Window.RootViewController.PresentViewController(okAlertController, true, null);
    }
}
