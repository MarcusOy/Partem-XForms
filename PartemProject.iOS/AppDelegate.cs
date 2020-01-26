using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Foundation;
using Syncfusion.SfBusyIndicator.XForms.iOS;
using Syncfusion.XForms.iOS.TextInputLayout;
using UIKit;
using Xamarin.Forms;

namespace PartemProject.iOS
{
    // The UIApplicationDelegate for the application. This class is responsible for launching the 
    // User Interface of the application, as well as listening (and optionally responding) to 
    // application events from iOS.
    [Register("AppDelegate")]
    public partial class AppDelegate : global::Xamarin.Forms.Platform.iOS.FormsApplicationDelegate
    {
        //
        // This method is invoked when the application has loaded and is ready to run. In this 
        // method you should instantiate the window, load the UI into it and then make the window
        // visible.
        //
        // You have 17 seconds to return from this method, or iOS will terminate your application.
        //
        public override bool FinishedLaunching(UIApplication app, NSDictionary options)
        {
            global::Xamarin.Forms.Forms.Init();
            FormsMaterial.Init();

            new SfBusyIndicatorRenderer();
            SfTextInputLayoutRenderer.Init();
            Syncfusion.XForms.iOS.ParallaxView.SfParallaxViewRenderer.Init();
            Syncfusion.XForms.iOS.Core.SfAvatarViewRenderer.Init();
            Syncfusion.XForms.iOS.Border.SfBorderRenderer.Init();
            Syncfusion.XForms.iOS.Buttons.SfButtonRenderer.Init();
            FFImageLoading.Forms.Platform.CachedImageRenderer.Init();
            LoadApplication(new App());

            //var allFonts = UIFont.FamilyNames.SelectMany(UIFont.FontNamesForFamilyName).ToList();
            //allFonts.Sort();
            //Console.WriteLine(string.Join("\n", allFonts));

            return base.FinishedLaunching(app, options);
        }

        public override bool OpenUrl(UIApplication app, NSUrl url, NSDictionary options)
        {
            Task.Run(async () => OpenInUrl(url));
            return true;
        }

        public async Task<bool> OpenInUrl(NSUrl url)
        {
            Task.Delay(5000);
            MessagingCenter.Send<object, string>(this, "OpenInUrl", url.AbsoluteString.Substring(9));

            return true;

        }
    }
}
