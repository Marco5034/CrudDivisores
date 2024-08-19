namespace CrudDivisores
{
    public partial class MainPage : ContentPage
    {
        private LocalDbService _dbServices;
        public int _editRegistroId;
        private List<int> _divisors;
        public MainPage(LocalDbService service)
        {
            InitializeComponent();
            _dbServices = service;
            LoadRecords();
        }

        private void OnGetDivisorsClicked(object sender, EventArgs e)
        {
            if (int.TryParse(numberEntry.Text, out int number))
            {
                _divisors = ObtenerDivisores.GetDivisors(number);
                divisorsListView.ItemsSource = _divisors.OrderDescending();
            }
            else
            {
                DisplayAlert("Error", "Please enter a valid number", "OK");
            }
        }

        private async void OnSaveRecordClicked(object sender, EventArgs e)
        {
            if (_editRegistroId == 0)
            {
                if (_divisors != null && _divisors.Any())
                {
                    await _dbServices.SaveRecordAsync( new TRegistros
                    {
                        Numero = int.Parse(numberEntry.Text),
                        Divisores = string.Join(",", _divisors.OrderDescending())
                    });

                   
                }

                _editRegistroId = 0;
            }
            else
            {
                    await _dbServices.UpdateRecordAsync(new TRegistros
                    {
                        Id = _editRegistroId,
                        Numero = int.Parse(numberEntry.Text),
                        Divisores = string.Join(",", _divisors.OrderDescending())
                    });


            }
            _editRegistroId = 0; 
            numberEntry.Text = string.Empty;
            divisorsListView.ItemsSource = string.Empty;

            LoadRecords();

        }
    


        private async void LoadRecords()
        {
            var records = await _dbServices.GetRecordsAsync();
            recordsListView.ItemsSource = records;
        }

        private async void OnRecord_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            var registro = (TRegistros)e.Item;
            var action = await DisplayActionSheet("Action", "Cancel", null, "Edit", "Delete");


            switch (action)
            {
                case "Edit":
                    _editRegistroId = registro.Id;
                    numberEntry.Text = registro.Numero.ToString();
                    divisorsListView.ItemsSource = registro.Divisores;
                    LoadRecords();


                    break;

                case "Delete":
                    await _dbServices.DeleteRecordAsync(registro);
                    recordsListView.ItemsSource = await _dbServices.GetRecordsAsync();
                    break;
            }
        }
    }

}
