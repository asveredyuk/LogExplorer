using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ClientApp.LogExplorer.View.RecycleListView
{
    public class RecycleLIstViewItemControl<T> : UserControl
    {
        /// <summary>
        /// Called by control, if data changed\deleted
        /// </summary>
        public event Action OnDataDirty;
        /// <summary>
        /// Bind data to item control. Control must refresh
        /// </summary>
        /// <param name="data"></param>
        public virtual void BindData(T data)
        {
            throw new NotImplementedException();
        }

        protected void RaiseOnDataDirty()
        {
            if (OnDataDirty != null)
            {
                OnDataDirty();
            }
        }
    }
}
