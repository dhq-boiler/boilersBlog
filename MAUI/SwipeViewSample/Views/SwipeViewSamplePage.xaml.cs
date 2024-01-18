using SwipeViewSample.ViewModels;

namespace SwipeViewSample
{
    public partial class SwipeViewSamplePage : ContentPage
    {
        public SwipeViewSamplePage(SwipeViewSamplePageViewModel vm)
        {
            InitializeComponent();
            BindingContext = vm;
        }
    }

}
