using Codeplex.Reactive;
using Codeplex.Reactive.Extensions;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reactive.Subjects;
using System.Text;
using System.Threading.Tasks;

namespace ReactiveCollectionSample
{
    public class MainWindowViewModel
    {
        private readonly Random random = new Random();

        // IO<T>からReacOnlyReactiveCollection<T>
        private Subject<int> source = new Subject<int>();

        public ReadOnlyReactiveCollection<int> SimpleCollection { get; private set; }

        public ReactiveCommand SimpleAddCommand { get; private set; }

        public ReactiveCommand SimpleResetCommand { get; private set; }

        // IO<CollectionChanged<T>>からReadOnlyReactiveCollection<T>
        private Subject<CollectionChanged<int>> collectionChangedSource = new Subject<CollectionChanged<int>>();

        public ReadOnlyReactiveCollection<int> CollectionChangedCollection { get; private set; }


        public ReactiveCommand CollectionChangedAddCommand { get; private set; }
        public ReactiveCommand CollectionChangedRemoveCommand { get; private set; }
        public ReactiveCommand CollectionChangedClearCommand { get; private set; }

        // ObservableCollection<T>からReadOnlyObservableCollection<T>
        private ObservableCollection<string> sourceCollection = new ObservableCollection<string>();
        public ReadOnlyReactiveCollection<string> ObservableCollectionToReadOnlyReactiveCollection { get; private set; }

        public ReactiveCommand SourceCollectionAddCommand { get; private set; }

        public ReactiveCommand SourceCollectionRemoveCommand { get; private set; }

        public ReactiveCommand SourceCollectionResetCommand { get; private set; }

        public MainWindowViewModel()
        {
            this.SimpleAddCommand = new ReactiveCommand();
            this.SimpleAddCommand.Subscribe(_ =>
                {
                    this.source.OnNext(random.Next());
                });
            this.SimpleResetCommand = new ReactiveCommand();

            // IObservaboe<T>からコレクションへ変換。オプションとしてコレクションをリセットするIO<Unit>を渡せる
            this.SimpleCollection = this.source.ToReadOnlyReactiveCollection(this.SimpleResetCommand.ToUnit());

            this.CollectionChangedCollection = this.collectionChangedSource.ToReadOnlyReactiveCollection();
            this.CollectionChangedAddCommand = new ReactiveCommand();
            this.CollectionChangedAddCommand.Subscribe(_ =>
                {
                    this.collectionChangedSource.OnNext(
                        CollectionChanged<int>.Add(0, random.Next()));
                });

            this.CollectionChangedClearCommand = new ReactiveCommand();
            this.CollectionChangedClearCommand.Subscribe(_ =>
                {
                    this.collectionChangedSource.OnNext(
                        CollectionChanged<int>.Reset);
                });

            this.CollectionChangedRemoveCommand = new ReactiveCommand();
            this.CollectionChangedRemoveCommand.Subscribe(_ =>
                {
                    this.collectionChangedSource.OnNext(
                        CollectionChanged<int>.Remove(0));
                });

            // ObservableCollection<T>からReadOnlyObservableCollection<U>への変換
            this.ObservableCollectionToReadOnlyReactiveCollection = this.sourceCollection
                .ToReadOnlyReactiveCollection(x => x + " value.");

            this.SourceCollectionAddCommand = new ReactiveCommand();
            this.SourceCollectionAddCommand.Subscribe(_ =>
                {
                    this.sourceCollection.Add(random.Next().ToString());
                });
            this.SourceCollectionRemoveCommand = new ReactiveCommand();
            this.SourceCollectionRemoveCommand.Subscribe(_ =>
                {
                    this.sourceCollection.RemoveAt(this.sourceCollection.Count - 1);
                });
            this.SourceCollectionResetCommand = new ReactiveCommand();
            this.SourceCollectionResetCommand.Subscribe(_ =>
                {
                    this.sourceCollection.Clear();
                });
        }
    }
}
