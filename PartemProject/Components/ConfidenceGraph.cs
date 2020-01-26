using System;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace PartemProject.Components
{
    public class ConfidenceGraph : Grid
    {
        public BiasIcon[] BiasIcons;
        public BoxView[] GraphBars;
        public PartemLabelLight[] GraphLabels;

        public ConfidenceGraph()
        {
            this.BiasIcons = new BiasIcon[3];
            this.GraphBars = new BoxView[3];
            this.GraphLabels = new PartemLabelLight[3];

            this.BiasIcons[0] = new BiasIcon();
            this.BiasIcons[0].BiasBoxView.Color = Color.FromHex("#0053A5");
            this.BiasIcons[0].BiasBoxViewLabel.Text = "L";

            this.BiasIcons[1] = new BiasIcon();
            this.BiasIcons[1].BiasBoxView.Color = Color.FromHex("#C471ED");
            this.BiasIcons[1].BiasBoxViewLabel.Text = "C";

            this.BiasIcons[2] = new BiasIcon();
            this.BiasIcons[2].BiasBoxView.Color = Color.FromHex("#D30B0D");
            this.BiasIcons[2].BiasBoxViewLabel.Text = "R";

            this.GraphBars[0] = new BoxView();
            this.GraphBars[0].Color = Color.FromHex("#0053A5");

            this.GraphBars[1] = new BoxView();
            this.GraphBars[1].Color = Color.FromHex("#C471ED");

            this.GraphBars[2] = new BoxView();
            this.GraphBars[2].Color = Color.FromHex("#D30B0D");

            this.GraphLabels[0] = new PartemLabelLight();
            this.GraphLabels[0].Text = "100%";

            this.GraphLabels[1] = new PartemLabelLight();
            this.GraphLabels[1].Text = "10%";

            this.GraphLabels[2] = new PartemLabelLight();
            this.GraphLabels[2].Text = "75%";


            RowDefinitions.Add(new RowDefinition { Height = new GridLength(20) });
            RowDefinitions.Add(new RowDefinition { Height = new GridLength(20) });
            RowDefinitions.Add(new RowDefinition { Height = new GridLength(20) });

            ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(20) });
            ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) });

            for (int x = 0; x < 3; x++)
            {
                this.BiasIcons[x].WidthRequest = 20;
                this.BiasIcons[x].HeightRequest = 20;
                this.BiasIcons[x].BiasBoxViewLabel.FontSize = 10;
                //this.BiasIcons[x].Margin = new Thickness(0, 0, 5, 0);

                this.GraphBars[x].HeightRequest = 20;
                this.GraphBars[x].WidthRequest = this.Bounds.Width - 40;
                this.GraphBars[x].HorizontalOptions = LayoutOptions.FillAndExpand;

                this.GraphLabels[x].TextColor = Color.White;
                this.GraphLabels[x].WidthRequest = 25;

                Children.Add(this.BiasIcons[x], 0, x);
                Children.Add(this.GraphBars[x], 1, x);
                Children.Add(this.GraphLabels[x], 1, x);
            }

            HorizontalOptions = LayoutOptions.FillAndExpand;


            //Task.Run(async () => Animate());
            //this.BindingContextChanged += ConfidenceGraph_BindingContextChanged;
        }

        //private void ConfidenceGraph_BindingContextChanged(object sender, EventArgs e)
        //{
        //    Animate();
        //}

        public async void Animate()
        {
            var context = this.BindingContext as ResultFrameViewModel;

            var fullwidth = this.GraphBars[0].Bounds.Width;

            // Reset bars to 0 width
            this.GraphBars[0].LayoutTo(new Rectangle(
                this.GraphBars[0].Bounds.X,
                this.GraphBars[0].Bounds.Y,
                0, this.GraphBars[0].Bounds.Height), 1);
            this.GraphBars[1].LayoutTo(new Rectangle(
                this.GraphBars[1].Bounds.X,
                this.GraphBars[1].Bounds.Y,
                0, this.GraphBars[1].Bounds.Height), 1);
            await this.GraphBars[2].LayoutTo(new Rectangle(
                this.GraphBars[2].Bounds.X,
                this.GraphBars[2].Bounds.Y,
                0, this.GraphBars[2].Bounds.Height), 1);

            // Translate the labels
            int lO, cO, rO;
            lO = cO = rO =  -50;
            if (context.LeftPercentage <= 0.25)
            {
                lO = 10;
                this.GraphLabels[0].TextColor = Color.Black;
            }
            if (context.CenterPercentage <= 0.25)
            {
                cO = 10;
                this.GraphLabels[1].TextColor = Color.Black;
            }
            if (context.RightPercentage <= 0.25)
            {
                rO = 10;
                this.GraphLabels[2].TextColor = Color.Black;
            }
            this.GraphLabels[0].Text = Math.Round(context.LeftPercentage * 100, 0).ToString() + "%";
            this.GraphLabels[1].Text = Math.Round(context.CenterPercentage * 100, 0).ToString() + "%";
            this.GraphLabels[2].Text = Math.Round(context.RightPercentage * 100, 0).ToString() + "%";

            this.GraphLabels[0].TranslateTo((fullwidth * context.LeftPercentage) + lO, 0, 1000, Easing.CubicOut);
            this.GraphLabels[1].TranslateTo((fullwidth * context.CenterPercentage) + cO, 0, 1000, Easing.CubicOut);
            this.GraphLabels[2].TranslateTo((fullwidth * context.RightPercentage) + rO, 0, 1000, Easing.CubicOut);


            // Expand the bars
            this.GraphBars[0].LayoutTo(new Rectangle(
                this.GraphBars[0].Bounds.X,
                this.GraphBars[0].Bounds.Y,
                fullwidth * context.LeftPercentage,
                this.GraphBars[1].Bounds.Height), 1000, Easing.CubicOut);
            this.GraphBars[1].LayoutTo(new Rectangle(
                this.GraphBars[1].Bounds.X,
                this.GraphBars[1].Bounds.Y,
                fullwidth * context.CenterPercentage,
                this.GraphBars[2].Bounds.Height), 1000, Easing.CubicOut);
            await this.GraphBars[2].LayoutTo(new Rectangle(
                this.GraphBars[2].Bounds.X,
                this.GraphBars[2].Bounds.Y,
                fullwidth * context.RightPercentage,
                this.GraphBars[2].Bounds.Height), 1000, Easing.CubicOut);
        }
    }
}
