using InfinityScroll.Cell;
using InfinityScroll.Helper;
using InfinityScroll.Models;
using InfinityScroll.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace InfinityScroll.Pages
{
    public class LocationListing : ContentPage
    {
        ListView lvLocations;
        int _NumberOfRecords = 15;
        LoadingIndicator _LoaderImage;
        bool _IsLoad;
        List<LocationsModel> LocationList = new List<LocationsModel>();
        public int _PageNo = 0;
        /// <summary>
        /// Constructor
        /// </summary>
        public LocationListing()
        {
            Device.BeginInvokeOnMainThread(async () =>
            {
                LocationList = await LocationService.GetLocations(_PageNo, _NumberOfRecords);
                Items = new ObservableCollection<LocationsModel>(LocationList);

                PageLayout();
            });

        }

        /// <summary>
        /// page content
        /// </summary>
        public void PageLayout()
        {
            try
            {
                //if (Items.Count < _NumberOfRecords)
                //{
                //    _IsLoad = true;
                //}

                lvLocations = new ListView
                {
                    HasUnevenRows = true
                };

                lvLocations.ItemsSource = Items;
                lvLocations.ItemTemplate = new DataTemplate(() => new LocationCell());

                lvLocations.ItemAppearing += (sender, e) =>
                {
                    if (_IsLoad || Items.Count == 0)
                        return;


                    //hit bottom!
                    if (e.Item == Items[Items.Count - 1])
                    {
                        AppendItems();
                    }
                };

                lvLocations.ItemSelected += (sender, e) =>
                {
                    if (e.SelectedItem == null) return;
                    LocationsModel obj = (LocationsModel)e.SelectedItem;

                    ((ListView)sender).SelectedItem = null;
                    //Navigation.PushAsync(App.PartDetailsPage(obj.Make, obj.Part, obj.PartImageUrl));
                };

                _LoaderImage = new LoadingIndicator();

                StackLayout slLayout = new StackLayout
                {
                    Children =
                    {
                        lvLocations,_LoaderImage
                    },
                    VerticalOptions = LayoutOptions.FillAndExpand,
                    HorizontalOptions = LayoutOptions.FillAndExpand,
                    BackgroundColor = LayoutHelper.PageBackgroundColor
                };
                slLayout.Padding = LayoutHelper.IOSPadding(0, 20, 0, 0);

                ScrollView scrollContent = new ScrollView
                {
                    Content = slLayout
                };

                this.Content = scrollContent;
            }
            catch (Exception ex)
            {
            }
        }

        #region Paging

        private ObservableCollection<LocationsModel> _items = new ObservableCollection<LocationsModel>();
        public ObservableCollection<LocationsModel> Items
        {
            get { return _items; }
            set
            {
                _items = value;
                OnPropertyChanged();
            }
        }

        public void AppendItems()
        {
            //_LoaderImage.IsVisible = true;
            _LoaderImage.IsShowLoading = true;

            _IsLoad = true;
            _PageNo++;

            Device.BeginInvokeOnMainThread(async () =>
            {
                try
                {
                    List<LocationsModel> lstPart = new List<LocationsModel>();
                    lstPart = await LocationService.GetLocations(_PageNo, _NumberOfRecords);

                    foreach (LocationsModel item in lstPart)
                    {
                        Items.Add(new LocationsModel
                        {
                            City = item.City,
                            Id = item.Id,
                            State = item.State,
                            Street = item.Street,
                            ZipCode = item.ZipCode,
                            DateCreated = item.DateCreated,
                            Title = item.Title
                        });
                    }

                    AfterLoad(lstPart.Count);
                }
                catch (Exception ex)
                {
                    DisplayAlert(string.Empty, "Error Ocurred when loading more data", "OK");
                    //_LoaderImage.IsVisible = false;
                    _LoaderImage.IsShowLoading = false;

                    _IsLoad = false;
                    _PageNo = _PageNo - 1;
                }
            });
        }

        private void AfterLoad(int count)
        {
            _IsLoad = false;
            //_LoaderImage.IsVisible = false;
            _LoaderImage.IsShowLoading = false;

            if (count < _NumberOfRecords)
            {
                _IsLoad = true;
            }
        }

        #endregion
    }
}
