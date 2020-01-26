using System;
using PartemProject.ViewModels;
using Syncfusion.XForms.AvatarView;
using Syncfusion.XForms.Buttons;
using Syncfusion.XForms.Graphics;
using Xamarin.Forms;

namespace PartemProject.Components
{
    public class ResultFrame : Frame
    {
        PartemLabelBold TitleLabel;

        BiasIcon BiasIcon;

        PartemLabelBold BiasLabel;

        ArticleFrame ArticleFrame;

        PartemLabelLight BiasStatementLabel;
        PartemLabelLight GraphLabel;

        SfButton ShareButton;

        public ConfidenceGraph Graph;

        public ResultFrame()
        {
            this.Visual = VisualMarker.Material;
            Margin = new Thickness(20, 25, 20, 10);
            CornerRadius = 20;

            this.TitleLabel = new PartemLabelBold
            {
                Text = "Here's what we found:",
                LineBreakMode = Xamarin.Forms.LineBreakMode.WordWrap
            };

            this.BiasIcon = new BiasIcon();

            this.BiasLabel = new PartemLabelBold
            {
                Text = "Center",
                VerticalOptions = LayoutOptions.Center,
                FontSize = 35
            };

            this.ArticleFrame = new ArticleFrame();

            this.BiasStatementLabel = new PartemLabelLight
            {
                Text = "Using the power of AI, we determined that this article contains little to no bias.",
                LineBreakMode = Xamarin.Forms.LineBreakMode.WordWrap,
                Margin = new Thickness(0, 0, 0, 15)

            };

            this.GraphLabel = new PartemLabelLight
            {
                Text = "Here’s how confident we are on each bias side:",
                LineBreakMode = Xamarin.Forms.LineBreakMode.WordWrap
            };

            this.Graph = new ConfidenceGraph();

            this.ShareButton = new SfButton
            {
                Text = "Share",
                FontFamily = Device.RuntimePlatform == Device.iOS ? "Nexa-Bold" : null,
                FontSize = 20,
                TextColor = Color.White,
                CornerRadius = 20,
                HeightRequest = 50,
                BackgroundGradient = new SfLinearGradientBrush
                {
                    GradientStops =
                    {
                        new SfGradientStop { Color = Color.FromHex("#2980B9"), Offset = 0 },
                        new SfGradientStop { Color = Color.FromHex("#6DD5FA"), Offset = 0.5 },
                        new SfGradientStop { Color = Color.FromHex("#DDF4FC"), Offset = 1 }
                    }
                },
                HasShadow = true,
            };

            this.Content = new StackLayout
            {
                Children =
                {
                    this.TitleLabel,
                    new StackLayout
                    {
                        Orientation = StackOrientation.Horizontal,
                        Children =
                        {
                            this.BiasIcon,
                            this.BiasLabel
                        }
                    },
                    this.ArticleFrame,
                    this.BiasStatementLabel,
                    this.GraphLabel,
                    this.Graph,
                    this.ShareButton
                }
            };
            this.BindingContextChanged += ResultFrame_BindingContextChanged;
        }

        private void ResultFrame_BindingContextChanged(object sender, EventArgs e)
        {
            var context = this.BindingContext as ResultFrameViewModel;
            double[] percs = { context.LeftPercentage, context.CenterPercentage, context.RightPercentage };

            if (percs[0] > percs[1])
            {
                if (percs[0] > percs[2])
                {
                    this.BiasIcon.BiasBoxViewLabel.Text = "L";
                    this.BiasIcon.BiasBoxView.Color = Color.FromHex("#C471ED");
                    this.BiasLabel.Text = "Left-Wing";
                    this.BiasStatementLabel.Text = "Using the power of AI, we determined that this article contains left-winged bias.";

                }
                else
                {
                    this.BiasIcon.BiasBoxViewLabel.Text = "R";
                    this.BiasIcon.BiasBoxView.Color = Color.FromHex("#F64F59");
                    this.BiasLabel.Text = "Right-Wing";
                    this.BiasStatementLabel.Text = "Using the power of AI, we determined that this article contains right-winged bias.";
                }
                
            }
            else if (percs[1] > percs[2])
            {
                this.BiasIcon.BiasBoxViewLabel.Text = "C";
                this.BiasIcon.BiasBoxView.Color = Color.FromHex("#12C2E9");
                this.BiasLabel.Text = "Right-Wing";
                this.BiasStatementLabel.Text = "Using the power of AI, we determined that this article contains little to no bias.";
            }
            else
            {
                this.BiasIcon.BiasBoxViewLabel.Text = "R";
                this.BiasIcon.BiasBoxView.Color = Color.FromHex("#F64F59");
                this.BiasLabel.Text = "Right-Wing";
                this.BiasStatementLabel.Text = "Using the power of AI, we determined that this article contains right-winged bias.";
            }
        }
    }

    public class ResultFrameViewModel : BaseViewModel
    {
        double leftPercetage = new double();
        public double LeftPercentage
        {
            get { return this.leftPercetage; }
            set { SetProperty(ref this.leftPercetage, value); }
        }

        double centerPercentage = new double();
        public double CenterPercentage
        {
            get { return this.centerPercentage; }
            set { SetProperty(ref this.centerPercentage, value); }
        }

        double rightPercentage = new double();
        public double RightPercentage
        {
            get { return this.rightPercentage; }
            set { SetProperty(ref this.rightPercentage, value); }
        }

        string headline = "";
        public string Headline
        {
            get { return this.headline; }
            set { SetProperty(ref this.headline, value); }
        }

        string source = "";
        public string Source
        {
            get { return this.source; }
            set { SetProperty(ref this.source, value); }
        }

        string articleImage = "";
        public string ArticleImage
        {
            get { return this.articleImage; }
            set { SetProperty(ref this.articleImage, value); }
        }
    }
}
