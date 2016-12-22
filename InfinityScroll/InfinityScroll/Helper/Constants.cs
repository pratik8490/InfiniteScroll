using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace InfinityScroll.Helper
{
    public class Constants
    {
        public const string ServiceURL = "https://api.flavortownusa.com";
        public const string FacebookIcon = "icon_facebook.png";
        public const string LeftMenuIcon = "line_menu.png";
        public const string SlideOut = "slideout.png";
        public const string FbColor = "#3d509f";
        public static Color BarBackGroundColor = Color.FromHex("#428BCA");
        public static Color BarBackTextColor = Color.White;
    }

    #region Layout Helper
    public class LayoutHelper
    {
        public static Color PageBackgroundColor = Color.White;
        public static Color FacebookButtonColor = Color.FromHex(Constants.FbColor);

        /// <summary>
        /// Set padding for ios.
        /// </summary>
        /// <param name="left"></param>
        /// <param name="top"></param>
        /// <param name="right"></param>
        /// <param name="bottom"></param>
        /// <returns></returns>
        public static Thickness IOSPadding(int left, int top, int right, int bottom)
        {
            var padding = new Thickness(0, 0, 0, 0);
            if (Device.OS == TargetPlatform.iOS)
            {
                padding = new Thickness(left, top, right, bottom);
            }
            return padding;
        }

        public static Button NavButton(Button Button)
        {
            Button.FontSize = 14;
            Button.TextColor = Color.White;
            Button.BackgroundColor = FacebookButtonColor;
            Button.BorderRadius = 0;
            Button.HeightRequest = 35;

            return Button;
        }
    }
    #endregion

}
