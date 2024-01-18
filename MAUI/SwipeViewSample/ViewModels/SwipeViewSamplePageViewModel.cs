using Reactive.Bindings;
using Reactive.Bindings.Disposables;
using Reactive.Bindings.Extensions;
using SwipeViewSample.Extensions;

namespace SwipeViewSample.ViewModels
{
    public class SwipeViewSamplePageViewModel : BindableObject, IDisposable
    {
        private readonly CompositeDisposable _disposables = new();
        private bool disposedValue;

        public ReactiveCollection<string> Entries { get; } = new();

        public ReactiveCommandSlim<string> LeftSwipeItemCommand { get; } = new();

        public ReactiveCommandSlim<string> RightSwipeItemCommand { get; } = new();

        public SwipeViewSamplePageViewModel()
        {
            Entries.AddRange(["12345", "67890", "壱弐参肆伍", "陸漆捌玖拾"]);
            LeftSwipeItemCommand.Subscribe(async str =>
            {
                await Shell.Current.CurrentPage.DisplayAlert("LeftSwipeItem", "LeftSwipeItem がタップされました。", "OK");
            }).AddTo(_disposables);
            RightSwipeItemCommand.Subscribe(async str =>
            {
                await Shell.Current.CurrentPage.DisplayAlert("RightSwipeItem", "RightSwipeItem がタップされました。", "OK");
            }).AddTo(_disposables);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    // マネージド状態を破棄します (マネージド オブジェクト)
                    _disposables.Dispose();
                }

                disposedValue = true;
            }
        }

        public void Dispose()
        {
            // このコードを変更しないでください。クリーンアップ コードを 'Dispose(bool disposing)' メソッドに記述します
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }
    }
}
