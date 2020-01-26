using System;
using Xamarin.Forms;

namespace PartemProject.Components
{
    public class BiasIcon : Grid
    {
        public BoxView BiasBoxView;
        public PartemLabelBold BiasBoxViewLabel;

        public BiasIcon()
        {
            this.BiasBoxView = new BoxView
            {
                Color = Color.FromHex("#C471ED"),
                WidthRequest = 75,
                HeightRequest = 75,
            };

            this.BiasBoxViewLabel = new PartemLabelBold
            {
                Text = "C",
                HorizontalOptions = LayoutOptions.Center,
                VerticalOptions = LayoutOptions.Center,
                TextColor = Color.White
            };

            Children.Add(this.BiasBoxView, 0, 0);
            Children.Add(this.BiasBoxViewLabel, 0, 0);
        }
    }
}
