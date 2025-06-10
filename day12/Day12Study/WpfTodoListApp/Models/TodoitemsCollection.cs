using System.Collections.ObjectModel;
using System.Collections.Specialized;

namespace WpfTodoListApp.Models
{
    public class TodoitemsCollection : ObservableCollection<Todoitem>
    {
        // API로 들어오는 값은 List -> ObservableCollection으로 변환
        public void CopyFrom(IEnumerable<Todoitem> todoItems)
        {
            this.Items.Clear(); // ObservableCollection 자체가 Items 속성 보유

            foreach (Todoitem item in todoItems)
            { 
                this.Items.Add(item);
            }

            // 데이터가 변경되었다고 알려줌
            this.OnCollectionChanged(new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Reset));
        }
    }
}
