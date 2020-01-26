using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using PartemProject.Components;
using PartemProject.ViewModels;
using Syncfusion.SfBusyIndicator.XForms;
using Syncfusion.XForms.Buttons;
using Syncfusion.XForms.ParallaxView;
using Syncfusion.XForms.TextInputLayout;
using Xamarin.Forms;
using static PartemProject.APIModels;

namespace PartemProject
{
    public class MainPage : ContentPage
    {
        Image PartemLogo;
        PartemLabelBold TitleLabel;
        SfBusyIndicator ActivityIndicator;
        AnalyzeFrame AnalyzeFrame;
        ResultFrame ResultFrame;

        Grid RootLayout;
        StackLayout MainLayout;
        ScrollView ScrollView;
        Image BackgroundImage;

        PartemLabelLight CopyrightLabel;
        public MainPage()
        {
            this.Visual = VisualMarker.Material;
            NavigationPage.SetHasNavigationBar(this, false);

            this.BindingContext = new BaseViewModel();

            this.PartemLogo = new Image
            {
                Source = ImageSource.FromResource("Icon.png"),
                HeightRequest = 50,
                WidthRequest = 50
            };

            this.TitleLabel = new PartemLabelBold
            {
                Text = "partem.tech",
                TextColor = Color.White
            };

            this.ActivityIndicator = new SfBusyIndicator()
            {
                AnimationType = AnimationTypes.Ball,
                IsBusy = true,
                TextColor = Color.White,
                WidthRequest = 40,
                HeightRequest = 40,
                VerticalOptions = LayoutOptions.Center
            };
            this.ActivityIndicator.SetBinding(SfBusyIndicator.IsBusyProperty, new Binding("IsBusy"));

            this.AnalyzeFrame = new AnalyzeFrame();
            this.ResultFrame = new ResultFrame();

            this.RootLayout = new Grid
            {
                RowDefinitions =
                {
                    new RowDefinition{ Height = new GridLength(1, GridUnitType.Star)}
                }
            };

            this.CopyrightLabel = new PartemLabelLight
            {
                Text = "copyright © partem 2020",
                FontSize = 15,
                TextColor = Color.White,
                HorizontalOptions = LayoutOptions.Center
            };

            this.MainLayout = new StackLayout
            {
                Children =
                {
                    new StackLayout
                    {
                        Orientation = StackOrientation.Horizontal,
                        Children =
                        {
                            this.PartemLogo,
                            this.TitleLabel
                        },
                        Margin = new Thickness(0, 40, 0, 10),
                        HorizontalOptions = LayoutOptions.Center
                    },
                    this.ActivityIndicator,
                    this.AnalyzeFrame,
                    //this.ResultFrame,
                    this.CopyrightLabel
                }
            };

            this.ScrollView = new ScrollView
            {
                Content = this.MainLayout
            };

            this.BackgroundImage = new Image
            {
                Source = ImageSource.FromResource("Background.png"),
                Aspect = Aspect.AspectFill,
            };

            this.RootLayout.Children.Add(this.BackgroundImage, 0, 0);
            this.RootLayout.Children.Add(this.ScrollView, 0, 0);

            this.Content = this.RootLayout;

            this.AnalyzeFrame.AnalyzeButton.Clicked += AnalyzeButton_Clicked;
            this.BindingContextChanged += MainPage_BindingContextChanged;

            MessagingCenter.Subscribe<object, string>(this, "OpenInUrl", (object obj, string s) =>
            {
                Device.BeginInvokeOnMainThread(() => {
                    (this.AnalyzeFrame.URLTextBox.InputView as Entry).Text = s;
                    this.AnalyzeButton_Clicked(this, new EventArgs());

                });
            });

        }

        private void MainPage_BindingContextChanged(object sender, EventArgs e)
        {
            //this.AnalyzeFrame.AnalyzeButton.IsEnabled = !IsBusy;
        }

        private async void AnalyzeButton_Clicked(object sender, EventArgs e)
        {
            this.AnalyzeFrame.URLTextBox.HasError = false;

            if (Uri.IsWellFormedUriString((this.AnalyzeFrame.URLTextBox.InputView as Entry).Text, UriKind.Absolute))
            {
                (this.BindingContext as BaseViewModel).IsBusy = true;
                this.AnalyzeFrame.AnalyzeButton.IsEnabled = false;

                if (this.MainLayout.Children.Contains(this.ResultFrame))
                {
                    this.MainLayout.Children.Remove(this.ResultFrame);
                }
                var request = new ServiceRequest
                {
                    Url = (this.AnalyzeFrame.URLTextBox.InputView as Entry).Text
                };

                await Task.Delay(2000);

                var response = DependencyService.Get<ApiAdapter>().MakeRequest(request).Result;
                if (!response.Success)
                {
                    await DisplayAlert("Error", response.Error, "OK");

                    (this.BindingContext as BaseViewModel).IsBusy = false;
                    await Task.Delay(5000);
                    this.AnalyzeFrame.AnalyzeButton.IsEnabled = true;

                    return;
                }

                this.ResultFrame = new ResultFrame();
                this.ResultFrame.BindingContext = new ResultFrameViewModel
                {
                    LeftPercentage = response.LeftPercentage,
                    CenterPercentage = response.CenterPercentage,
                    RightPercentage = response.RightPercentage,
                    Headline = response.Headline,
                    Source = response.Source,
                    ArticleImage = response.Image
                };
                this.MainLayout.Children.Insert(3, this.ResultFrame);

                this.ResultFrame.FadeTo(0, 0, Easing.CubicOut);
                await this.ResultFrame.TranslateTo(0, 200, 0, Easing.CubicOut);
                this.ResultFrame.TranslateTo(0, 0, 1000, Easing.CubicOut);
                await this.ResultFrame.FadeTo(1, 1000, Easing.CubicOut);

                this.ResultFrame.Graph.Animate();

                (this.BindingContext as BaseViewModel).IsBusy = false;

                this.ScrollView.ScrollToAsync(0, this.ResultFrame.Bounds.Y - 50, true);


                await Task.Delay(5000);
                this.AnalyzeFrame.AnalyzeButton.IsEnabled = true;
            }
            else
            {
                this.AnalyzeFrame.URLTextBox.HasError = true;
            }

        }
    }


}
