using Reactive.Bindings;
using Reactive.Bindings.Disposables;
using Reactive.Bindings.Extensions;
using System.Reflection;

namespace CarouselSample.ViewModels
{
    public class MainPageViewModel : BindableObject
    {
        //private CompositeDisposable _disposables = new();

        public ReactivePropertySlim<int> Index { get; } = new(0);
        public ReactiveCollection<ImageSource> Contents { get; } = new();
        //public ReactiveCommandSlim SwipeUpCommand { get; } = new();

        public MainPageViewModel()
        {
            //埋め込みリソースからImageSourceインスタンスを取得し、Contentsに装填します
            var assembly = typeof(App).GetTypeInfo().Assembly;
            Contents.Add(ImageSource.FromResource("CarouselSample.Assets.Images.m1.png", assembly));
            Contents.Add(ImageSource.FromResource("CarouselSample.Assets.Images.m2.png", assembly));
            Contents.Add(ImageSource.FromResource("CarouselSample.Assets.Images.m3.png", assembly));
            Contents.Add(ImageSource.FromResource("CarouselSample.Assets.Images.m4.png", assembly));
            Contents.Add(ImageSource.FromResource("CarouselSample.Assets.Images.m5.png", assembly));
            Contents.Add(ImageSource.FromResource("CarouselSample.Assets.Images.m5r.png", assembly));
            Contents.Add(ImageSource.FromResource("CarouselSample.Assets.Images.m6.png", assembly));
            Contents.Add(ImageSource.FromResource("CarouselSample.Assets.Images.m7.png", assembly));
            Contents.Add(ImageSource.FromResource("CarouselSample.Assets.Images.m8.png", assembly));
            Contents.Add(ImageSource.FromResource("CarouselSample.Assets.Images.m9.png", assembly));
            Contents.Add(ImageSource.FromResource("CarouselSample.Assets.Images.p1.png", assembly));
            Contents.Add(ImageSource.FromResource("CarouselSample.Assets.Images.p2.png", assembly));
            Contents.Add(ImageSource.FromResource("CarouselSample.Assets.Images.p3.png", assembly));
            Contents.Add(ImageSource.FromResource("CarouselSample.Assets.Images.p4.png", assembly));
            Contents.Add(ImageSource.FromResource("CarouselSample.Assets.Images.p5.png", assembly));
            Contents.Add(ImageSource.FromResource("CarouselSample.Assets.Images.p5r.png", assembly));
            Contents.Add(ImageSource.FromResource("CarouselSample.Assets.Images.p6.png", assembly));
            Contents.Add(ImageSource.FromResource("CarouselSample.Assets.Images.p7.png", assembly));
            Contents.Add(ImageSource.FromResource("CarouselSample.Assets.Images.p8.png", assembly));
            Contents.Add(ImageSource.FromResource("CarouselSample.Assets.Images.p9.png", assembly));
            Contents.Add(ImageSource.FromResource("CarouselSample.Assets.Images.s1.png", assembly));
            Contents.Add(ImageSource.FromResource("CarouselSample.Assets.Images.s2.png", assembly));
            Contents.Add(ImageSource.FromResource("CarouselSample.Assets.Images.s3.png", assembly));
            Contents.Add(ImageSource.FromResource("CarouselSample.Assets.Images.s4.png", assembly));
            Contents.Add(ImageSource.FromResource("CarouselSample.Assets.Images.s5.png", assembly));
            Contents.Add(ImageSource.FromResource("CarouselSample.Assets.Images.s5r.png", assembly));
            Contents.Add(ImageSource.FromResource("CarouselSample.Assets.Images.s6.png", assembly));
            Contents.Add(ImageSource.FromResource("CarouselSample.Assets.Images.s7.png", assembly));
            Contents.Add(ImageSource.FromResource("CarouselSample.Assets.Images.s8.png", assembly));
            Contents.Add(ImageSource.FromResource("CarouselSample.Assets.Images.s9.png", assembly));
            Contents.Add(ImageSource.FromResource("CarouselSample.Assets.Images.ton.png", assembly));
            Contents.Add(ImageSource.FromResource("CarouselSample.Assets.Images.nan.png", assembly));
            Contents.Add(ImageSource.FromResource("CarouselSample.Assets.Images.sha.png", assembly));
            Contents.Add(ImageSource.FromResource("CarouselSample.Assets.Images.pe.png", assembly));
            Contents.Add(ImageSource.FromResource("CarouselSample.Assets.Images.haku.png", assembly));
            Contents.Add(ImageSource.FromResource("CarouselSample.Assets.Images.hatsu.png", assembly));
            Contents.Add(ImageSource.FromResource("CarouselSample.Assets.Images.chun.png", assembly));

            //SwipeUpCommand.Subscribe(async () =>
            //{
            //    await Shell.Current.Navigation.PopAsync();
            //}).AddTo(_disposables);
        }

        public async Task Initialize(int index)
        {
            Index.Value = index;
        }
    }
}
