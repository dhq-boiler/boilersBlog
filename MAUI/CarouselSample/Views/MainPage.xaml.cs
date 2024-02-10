using CarouselSample.ViewModels;

namespace CarouselSample.Views
{
    public partial class MainPage : ContentPage
    {
        int count = 0;

        public MainPage(MainPageViewModel vm)
        {
            InitializeComponent();
            BindingContext = vm;
            vm.Initialize(0);
            this.NavigatingFrom += OnNavigatingFrom;
        }

        private void OnNavigatingFrom(object? sender, NavigatingFromEventArgs e)
        {
            this.CarouselTablet.LeftControl.Opacity = 0;
        }
    }

}
