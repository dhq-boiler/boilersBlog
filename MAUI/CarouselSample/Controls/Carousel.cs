using Reactive.Bindings;
using Reactive.Bindings.Disposables;
using Reactive.Bindings.Extensions;
using System.Collections.ObjectModel;

namespace CarouselSample.Controls
{
    public class Carousel : AbsoluteLayout
    {
        private static readonly CompositeDisposable _disposables = new();
        private bool initGuard = true;

        public static readonly BindableProperty LeftObjectProperty = BindableProperty.Create(nameof(LeftObject), typeof(ImageSource), typeof(Carousel));

        public static readonly BindableProperty CenterObjectProperty = BindableProperty.Create(nameof(CenterObject), typeof(ImageSource), typeof(Carousel));

        public static readonly BindableProperty RightObjectProperty = BindableProperty.Create(nameof(RightObject), typeof(ImageSource), typeof(Carousel));

        public static readonly BindableProperty ImageSourcesProperty = BindableProperty.Create(nameof(ImageSources), typeof(ObservableCollection<ImageSource>), typeof(Carousel), propertyChanged: ImageSources_PropertyChanged);

        public static readonly BindableProperty IndexProperty = BindableProperty.Create(nameof(Index), typeof(int), typeof(Carousel), propertyChanged: Index_PropertyChanged);

        private static void Index_PropertyChanged(BindableObject bindable, object oldvalue, object newvalue)
        {
            var newIndex = (int)newvalue;
            if (bindable is Carousel { initGuard:true } c)
            {
                var collection = c.ImageSources;
                c.LeftControl.Source = newIndex - 1 >= 0 ? collection[newIndex - 1] : collection.Last();
                c.CenterControl.Source = collection[newIndex];
                c.RightControl.Source = newIndex + 1 > collection.Count - 1 ? collection.First() : collection[newIndex + 1];
                c.initGuard = false;
            }
        }

        private static void ImageSources_PropertyChanged(BindableObject bindable, object oldvalue, object newvalue)
        {
            var collection = newvalue as ObservableCollection<ImageSource>;
            if (bindable is Carousel c)
            {
                c.LeftControl.Source = c.Index - 1 >= 0 ? collection[c.Index - 1] : collection.Last();
                c.CenterControl.Source = collection[c.Index];
                c.RightControl.Source = c.Index + 1 > collection.Count - 1 ? collection.First() : collection[c.Index + 1];
            }
        }

        public ImageSource LeftObject
        {
            get { return (ImageSource)GetValue(LeftObjectProperty); }
            set { SetValue(LeftObjectProperty, value); }
        }

        public ImageSource CenterObject
        {
            get { return (ImageSource)GetValue(CenterObjectProperty); }
            set { SetValue(CenterObjectProperty, value); }
        }

        public ImageSource RightObject
        {
            get { return (ImageSource)GetValue(RightObjectProperty); }
            set { SetValue(RightObjectProperty, value); }
        }

        public ObservableCollection<ImageSource> ImageSources
        {
            get { return (ObservableCollection<ImageSource>)GetValue(ImageSourcesProperty); }
            set { SetValue(ImageSourcesProperty, value); }
        }

        public int Index
        {
            get { return (int)GetValue(IndexProperty); }
            set { SetValue(IndexProperty, value); }
        }

        public ReactiveCommandSlim LoadedCommand { get; } = new();

        public static readonly BindableProperty SwipeLeftCommandProperty = BindableProperty.Create(nameof(SwipeLeftCommand), typeof(ReactiveCommandSlim), typeof(Carousel));

        public static readonly BindableProperty SwipeRightCommandProperty = BindableProperty.Create(nameof(SwipeRightCommand), typeof(ReactiveCommandSlim), typeof(Carousel));

        public ReactiveCommandSlim SwipeLeftCommand
        {
            get { return (ReactiveCommandSlim)GetValue(SwipeLeftCommandProperty); }
            set { SetValue(SwipeLeftCommandProperty, value); }
        }

        public ReactiveCommandSlim SwipeRightCommand
        {
            get { return (ReactiveCommandSlim)GetValue(SwipeRightCommandProperty); }
            set { SetValue(SwipeRightCommandProperty, value); }
        }

        private ReactiveCommandSlim _SwipeLeftCommand { get; } = new();
        private ReactiveCommandSlim _SwipeRightCommand { get; } = new();

        public Image LeftControl { get; } = new();
        public Image CenterControl { get; } = new();
        public Image RightControl { get; } = new();

        public HorizontalStackLayout HorizontalStackLayout { get; } = new();

        private bool init = true;

        public Carousel()
        {
            SwipeLeftCommand = _SwipeLeftCommand = new();
            SwipeRightCommand = _SwipeRightCommand = new();
            
            HorizontalStackLayout.Children.Add(LeftControl);
            HorizontalStackLayout.Children.Add(CenterControl);
            HorizontalStackLayout.Children.Add(RightControl);
            this.Children.Add(HorizontalStackLayout);
            
            this.SizeChanged += (sender, args) =>
            {
                // Carouselのサイズに合わせて、コントロールのサイズを更新
                LeftControl.WidthRequest = this.Width;
                LeftControl.HeightRequest = this.Height;
                CenterControl.WidthRequest = this.Width;
                CenterControl.HeightRequest = this.Height;
                RightControl.WidthRequest = this.Width;
                RightControl.HeightRequest = this.Height;

                if (init)
                {
                    LeftControl.TranslationX = -LeftControl.Width;
                    CenterControl.TranslationX = 0;
                    RightControl.TranslationX = CenterControl.Width;

                    HorizontalStackLayout.TranslationX = -this.Width;
                    init = false;
                }
            };
            
            this.Loaded += (sender, args) =>
            {
                LeftControl.Aspect = Aspect.AspectFit;
                CenterControl.Aspect = Aspect.AspectFit;
                RightControl.Aspect = Aspect.AspectFit;

                initGuard = true;
            };
            this.Unloaded += (sender, args) =>
            {
                initGuard = true;
            };

            this.GestureRecognizers.Add(new SwipeGestureRecognizer() { Command = SwipeLeftCommand, Direction = SwipeDirection.Left });
            this.GestureRecognizers.Add(new SwipeGestureRecognizer() { Command = SwipeRightCommand, Direction = SwipeDirection.Right });

            //右から左にスワイプする
            _SwipeLeftCommand.Subscribe(async () =>
            {
                initGuard = false;

                RightControl.Opacity = 1;
                // アニメーションの完了を待機
                await HorizontalStackLayout.TranslateTo(-this.Width * 2, 0, 250, Easing.SinInOut);
                
                // Indexを更新
                Index = (Index + 1) % ImageSources.Count;
                
                // RightControlとCenterControlを更新
                LeftControl.Source = CenterControl.Source;
                
                CenterControl.Source = RightControl.Source;

                await Task.Delay(50);
                
                HorizontalStackLayout.TranslationX = -this.Width;
                
                // 新しいRightControlを設定
                RightControl.Source = ImageSources[(Index + 1) % ImageSources.Count];
                
            }).AddTo(_disposables);

            //左から右にスワイプする
            _SwipeRightCommand.Subscribe(async () =>
            {
                initGuard = false;

                LeftControl.Opacity = 1;
                // アニメーションの完了を待機
                await HorizontalStackLayout.TranslateTo(0, 0, 250, Easing.SinInOut);
                
                // Indexを更新
                Index = Index - 1 >= 0 ? Index - 1 : ImageSources.Count - 1;
                
                // LeftControlとCenterControlを更新
                RightControl.Source = CenterControl.Source;
                
                CenterControl.Source = LeftControl.Source;

                await Task.Delay(50);
                
                HorizontalStackLayout.TranslationX = -this.Width;

                // 新しいLeftControlを設定
                LeftControl.Source = ImageSources[Index - 1 >= 0 ? Index - 1 : ImageSources.Count - 1];

            }).AddTo(_disposables);
        }
    }
}
