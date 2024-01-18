namespace SwipeViewSample
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();
            Routing.RegisterRoute(nameof(SwipeViewSamplePage), typeof(SwipeViewSamplePage));
        }
    }
}
