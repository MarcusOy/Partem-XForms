using System;
using FFImageLoading.Forms;
using Xamarin.Forms;

namespace PartemProject.Components
{
    public class ArticleFrame : Frame
    {
        PartemLabelBold HeadlineLabel;
        PartemLabelLight SourceLabel;
        CachedImage ArticleImage;

        public ArticleFrame()
        {
            this.Visual = VisualMarker.Material;
            this.HeightRequest = 60;
            this.CornerRadius = 20;

            this.HeadlineLabel = new PartemLabelBold
            {
                Text = "The quick brown fox jumped over the lazy dog.",
                FontSize = 15,
                LineBreakMode = LineBreakMode.TailTruncation,
                MaxLines = 2
            };
            this.HeadlineLabel.SetBinding(Label.TextProperty, new Binding("Headline"));

            this.SourceLabel = new PartemLabelLight
            {
                Text = "Roblox.com",
                FontSize = 13
            };
            this.SourceLabel.SetBinding(Label.TextProperty, new Binding("Source"));

            this.ArticleImage = new CachedImage
            {
                Source = ImageSource.FromResource("Error.png"),
                HeightRequest = 35,
                WidthRequest = 35,
                Aspect = Aspect.AspectFill,
                ErrorPlaceholder = ImageSource.FromResource("Error.png"),
                CacheDuration = TimeSpan.FromDays(1),
                DownsampleToViewSize = true,
                RetryCount = 0,
                RetryDelay = 250,
            };
            //this.ArticleImage.SetBinding(CachedImage.SourceProperty, new Binding("ArticleImage"));

            this.Content = new Grid
            {
                RowDefinitions =
                {
                    new RowDefinition{ Height = new GridLength(1, GridUnitType.Star)}
                },
                ColumnDefinitions =
                {
                    new ColumnDefinition{ Width = new GridLength(0.7, GridUnitType.Star)},
                    new ColumnDefinition{ Width = new GridLength(0.3, GridUnitType.Star)}
                },
                Children =
                {
                    {
                        new StackLayout
                        {
                            Children =
                            {
                                this.HeadlineLabel,
                                this.SourceLabel
                            }
                        },
                        0, 0
                    },
                    {
                        this.ArticleImage,
                        1, 0
                    }

                },
                VerticalOptions = LayoutOptions.Center,
                HorizontalOptions = LayoutOptions.Center
            };
        }
    }
}
