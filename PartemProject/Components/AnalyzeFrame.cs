using System;
using Syncfusion.SfBusyIndicator.XForms;
using Syncfusion.XForms.Buttons;
using Syncfusion.XForms.Graphics;
using Syncfusion.XForms.TextInputLayout;
using Xamarin.Forms;

namespace PartemProject.Components
{
    public class AnalyzeFrame : Frame
    {
        PartemLabelLight DirectionsLabel;
        public SfTextInputLayout URLTextBox;
        public SfButton AnalyzeButton;

        public AnalyzeFrame()
        {
            this.Visual = VisualMarker.Material;
            
            this.CornerRadius = 20;

            this.DirectionsLabel = new PartemLabelLight
            {
                Text = "Enter the Url of an article to analyze its political bias.",
                Margin = new Thickness(0, 0, 0, 15),
                LineBreakMode = Xamarin.Forms.LineBreakMode.WordWrap
            };

            this.URLTextBox = new SfTextInputLayout
            {
                Hint = "Article Url",
                InputView = new Entry(),
                Margin = new Thickness(0, 0, 0, 15),
                Visual = VisualMarker.Default,
                HintLabelStyle = new LabelStyle
                {
                    FontFamily = Device.RuntimePlatform == Device.iOS ? "Nexa-Light" : null,
                    FontSize = 20
                },
                ErrorText = "Entered Url is invalid."

            };

            this.AnalyzeButton = new SfButton
            {
                Text = "Analyze",
                FontFamily = Device.RuntimePlatform == Device.iOS ? "Nexa-Bold" : null,
                FontSize = 20,
                TextColor = Color.White,
                CornerRadius = 20,
                HeightRequest = 50,
                BackgroundGradient = new SfLinearGradientBrush
                {
                    GradientStops =
                    {
                        new SfGradientStop { Color = Color.FromHex("#12C2E9"), Offset = 0 },
                        new SfGradientStop { Color = Color.FromHex("#C471ED"), Offset = 0.5 },
                        new SfGradientStop { Color = Color.FromHex("#F64F59"), Offset = 1 }
                    }
                },
                HasShadow = true,
            };

            Content = new StackLayout
            {
                Children =
                {
                    this.DirectionsLabel,
                    this.URLTextBox,
                    this.AnalyzeButton
                }
            };

            Padding = new Thickness(20, 40);
            Margin = new Thickness(20, 10, 20, 10);
        }
    }
}
