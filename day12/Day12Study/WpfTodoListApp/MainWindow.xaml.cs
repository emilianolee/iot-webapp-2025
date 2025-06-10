using MahApps.Metro.Controls;
using MahApps.Metro.Controls.Dialogs;
using System.Diagnostics;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using WpfTodoListApp.Models;

namespace WpfTodoListApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : MetroWindow
    {
        HttpClient client = new HttpClient();
        // ObservableCollection을 굳이 사용하지 않아도 
        TodoitemsCollection todoitems = new TodoitemsCollection();

        public MainWindow()
        {
            InitializeComponent();
        }

        private void MetroWindow_Loaded(object sender, System.Windows.RoutedEventArgs e)
        {
            var comboPairs = new List<KeyValuePair<string, int>> {
                new KeyValuePair<string, int>("완료", 1),
                new KeyValuePair<string, int>("미완료", 0),
            };

            cboIsComplete.ItemsSource = comboPairs;

            // RestAPI 호출 준비
            client.BaseAddress = new System.Uri("http://localhost:6200");   // API 서버 URL
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            // 데이터 가져오기
            GetDatas();
        }

        private async Task GetDatas()
        {
            // /api/Todoitems GET 메서드 호출
            GridTodoitems.ItemsSource = todoitems;

            try // API 호출
            {
                // http://localhost:6200/api/Todoitems
                HttpResponseMessage? response = await client.GetAsync("/api/Todoitems");
                response.EnsureSuccessStatusCode(); // 상태코드 확인

                var items = await response.Content.ReadAsAsync<IEnumerable<Todoitem>>();
                todoitems.CopyFrom(items);  // ObservableCollection으로 형변환
                //GridTodoitems.ItemsSource = items;

                // 성공 메시지
                await this.ShowMessageAsync("API 호출", "로드 성공!");
            }
            catch (Exception ex)
            {
                // 예외 메시지
                await this.ShowMessageAsync("API 호출 에러", ex.Message);            
            }
        }

        // async시 Task가 리턴값이지만 버튼클릭이벤트 메서드와 연결시는 Task -> void로 변경해줘야 연결 가능
        private async void btnInsert_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            try
            {
                var todoitem = new Todoitem
                {
                    Id = 0, // Id는 테이블에서 자동생성 AutoIncrement
                    Title = txtTitle.Text,
                    TodoDate = Convert.ToDateTime(dtpTodoDate.SelectedDate).ToString("yyyyMMdd"),
                    IsComplete = Convert.ToBoolean(cboIsComplete.SelectedValue)
                };

                // 데이터 입력 확인
                //Debug.WriteLine(todoitem.Title);

                // POST 메서드 API 호출
                var response = await client.PostAsJsonAsync("/api/Todoitems", todoitem);
                response.EnsureSuccessStatusCode() ;

                GetDatas();

                // 입력양식 초기화
                InitControls();
            }
            catch (Exception ex)
            {
                // 예외 메시지
                await this.ShowMessageAsync("API 호출 에러", ex.Message);
            }
        }

        private async void GridTodoitems_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {

            try
            {
                //await this.ShowMessageAsync("클릭", "클릭확인");
                var id = (GridTodoitems.SelectedItem as Todoitem)?.Id;      // ?. -> Null이 생겨도 예외발생안함

                if (id == null) return;     // 이 구문을 만나야 아래 로직이 실행 안됨

                // /api/TodoItems/{id} GET 메서드 API 호출
                var response = await client.GetAsync($"/api/TodoItems/{id}");
                response.EnsureSuccessStatusCode() ;

                var item = await response.Content.ReadAsAsync<Todoitem>();

                txtId.Text = item.Id.ToString();
                txtTitle.Text = item.Title.ToString();
                dtpTodoDate.SelectedDate = DateTime.Parse(item.TodoDate.Insert(4, "-").Insert(7, "-"));
                cboIsComplete.SelectedValue = item.IsComplete;
            }
            catch (Exception ex)
            {
                // 예외 메시지
                await this.ShowMessageAsync("API 호출 에러", ex.Message);
            }
        }

        private async void btnUpdate_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            try
            {
                var todoitem = new Todoitem
                {
                    Id = Convert.ToInt32(txtId.Text),
                    Title = txtTitle.Text,
                    TodoDate = Convert.ToDateTime(dtpTodoDate.SelectedDate).ToString("yyyyMMdd"),
                    IsComplete = Convert.ToBoolean(cboIsComplete.SelectedValue)
                };

                var response = await client.PutAsJsonAsync($"/api/Todoitems/{todoitem.Id}", todoitem);
                response.EnsureSuccessStatusCode() ;

                GetDatas();

                // 입력양식 초기화
                InitControls();
            }
            catch (Exception ex)
            {
                // 예외 메시지
                await this.ShowMessageAsync("API 호출 에러", ex.Message);
            }
        }

        private void InitControls()
        {
            // 입력양식 초기화
            txtId.Text = string.Empty;
            txtTitle.Text = string.Empty;
            dtpTodoDate.Text = string.Empty;
            cboIsComplete.Text = string.Empty;
        }

        private async void btnDelete_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            try
            {
                var Id = Convert.ToInt32(txtId.Text);   // 삭제는 Id만 파라미터로 전송

                var response = await client.DeleteAsync($"/api/Todoitems/{Id}");
                response.EnsureSuccessStatusCode() ;

                GetDatas();

                // 입력양식 초기화
                InitControls();

            }
            catch (Exception ex)
            {
                // 예외 메시지
                await this.ShowMessageAsync("API 호출 에러", ex.Message);
            }
        }
    }
}