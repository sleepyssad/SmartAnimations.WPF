using System.Collections;
using System.Collections.ObjectModel;

namespace SmartAnimations.WPF
{
    public class SmAnimationCollection : ICollection<SmAnimationBase>, IEnumerable<SmAnimationBase>, IEnumerable, IList<SmAnimationBase>, ICollection, IList
    {
        public event EventHandler<ObservableCollection<SmAnimationBase>> OnChanged;

        private readonly ObservableCollection<SmAnimationBase> innerList;

        public SmAnimationCollection()
        {
            innerList = new ObservableCollection<SmAnimationBase>();
            innerList.CollectionChanged += InnerList_CollectionChanged;
        }

        public SmAnimationCollection(IEnumerable<SmAnimationBase> collection) : this()
        {
            foreach (var item in collection)
            {
                innerList.Add(item);
            }
        }

        private void InnerList_CollectionChanged(object? sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            OnChanged.Invoke(this, innerList);
        }

        public SmAnimationBase this[int index]
        {
            get => innerList[index];
            set => innerList[index] = value;
        }

        object? IList.this[int index]
        {
            get => this[index];
            set => this[index] = (SmAnimationBase)value!;
        }

        public int Count => innerList.Count;

        public bool IsReadOnly => false;

        public bool IsSynchronized => false;

        public object SyncRoot => this;

        public bool IsFixedSize => false;

        public void Add(SmAnimationBase item)
        {
            innerList.Add(item);
        }

        public int Add(object? value)
        {
            Add((SmAnimationBase)value!);
            return Count - 1;
        }

        public void Clear()
        {
            innerList.Clear();
        }

        public bool Contains(SmAnimationBase item)
        {
            return innerList.Contains(item);
        }

        public bool Contains(object? value)
        {
            return value is SmAnimationBase testClass && Contains(testClass);
        }

        public void CopyTo(SmAnimationBase[] array, int arrayIndex)
        {
            innerList.CopyTo(array, arrayIndex);
        }

        public void CopyTo(Array array, int index)
        {
            innerList.CopyTo((SmAnimationBase[])array, index);
        }

        public IEnumerator<SmAnimationBase> GetEnumerator()
        {
            return innerList.GetEnumerator();
        }

        public int IndexOf(SmAnimationBase item)
        {
            return innerList.IndexOf(item);
        }

        public int IndexOf(object? value)
        {
            return IndexOf((SmAnimationBase)value!);
        }

        public void Insert(int index, SmAnimationBase item)
        {
            innerList.Insert(index, item);
        }

        public void Insert(int index, object? value)
        {
            Insert(index, (SmAnimationBase)value!);
        }

        public bool Remove(SmAnimationBase item)
        {
            return innerList.Remove(item);
        }

        public void Remove(object? value)
        {
            Remove((SmAnimationBase)value!);
        }

        public void RemoveAt(int index)
        {
            innerList.RemoveAt(index);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
