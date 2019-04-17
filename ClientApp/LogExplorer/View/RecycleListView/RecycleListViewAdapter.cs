using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClientApp.LogExplorer.View.RecycleListView
{
    public class RecycleListViewAdapter<T>
    {
        public event Action OnDataChanged;

        public Func<RecycleLIstViewItemControl<T>> ControlSpawner { get; private set; }
        public Action OnDataDirty { get; private set; }

        private List<T> data;
        public IReadOnlyList<T> Data => data;

        public void UpdateData(IEnumerable<T> newData)
        {
            data = newData.ToList();
            if (OnDataChanged != null)
                OnDataChanged();
        }

        public RecycleListViewAdapter(Func<RecycleLIstViewItemControl<T>> controlSpawner, Action onDataDirty)
        {
            ControlSpawner = controlSpawner;
            OnDataDirty = onDataDirty;
        }
    }
}
