using InfinityScroll.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace InfinityScroll.Cell
{
    public class LocationCell : ViewCell
    {
        private LocationsModel model;
        protected override void OnBindingContextChanged()
        {
            model = (LocationsModel)BindingContext;
            base.OnBindingContextChanged();
            StackLayout stack = CreateLayout();

            View = stack;
        }

        public StackLayout CreateLayout()
        {

            Label lblTitle = new Label
            {
                Text = model.Title,
                TextColor = Color.Gray
            };

            StackLayout slFinalLayout = new StackLayout { Children = { lblTitle }, Padding = new Thickness(5), Orientation = StackOrientation.Horizontal, HorizontalOptions = LayoutOptions.FillAndExpand };

            return slFinalLayout;
        }
    }
}
