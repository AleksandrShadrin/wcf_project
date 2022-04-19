using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using WcfService1.Models;
using WcfService1.Repository;

namespace Server
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        Service _service;
        IContractsRepository _repo;
        bool catchingData;
        bool deleteState;
        public MainWindow()
        {
            InitializeComponent();
            deleteState = false;
            catchingData = false;
            _service = new Service();
            _repo = new ContractsRepository();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            _service.CreateConnection();
        }
        private async void GetAllContractsHandler(object sender, RoutedEventArgs e)
        {

            await GetAllContacts();
        }

        private void Select_Contract(object sender, MouseButtonEventArgs e)
        {
            if(Data.SelectedItem == default)
            {
                return;
            }
            var contract = Data.SelectedItem as ContractDto;
            LastUpdate.Text = contract.LastUpdate.ToString();
            Date.Text = contract.Date.ToString();
            Number.Text = contract.Number.ToString();
            Id.Content = contract.Id.ToString();
        }

        private async void UpdateRecord(object sender, RoutedEventArgs e)
        {
            var strDateTime = Date.Text;
            var strLastUpdate = LastUpdate.Text;
            var strNumber = Number.Text;
            var strId = Id.Content.ToString();

            if (!DateTime.TryParse(strDateTime, out var dt))
            {
                MessageBox.Show("Wrong Date");
                return;
            }
            if (!DateTime.TryParse(strLastUpdate, out var lu))
            {
                MessageBox.Show("Wrong LastUpdate");
                return;
            }
            if (!int.TryParse(strNumber, out var number))
            {
                MessageBox.Show("Wrong Number");
                return;
            }
            if (!int.TryParse(strId, out var intId))
            {
                MessageBox.Show("Wrong Id");
                return;
            }
            var contract = new Contract { Id= intId, Date = dt, LastUpdate = lu, Number = number };
            _repo.UpdateContract(contract);
            await GetAllContacts();

        }
        private async Task GetAllContacts()
        {
            if (catchingData)
            {
                return;
            }
            Data.Items.Clear();
            var contracts = await Task.Run(() => _repo.GetAllContracts());
            var contracts_dto = contracts.Select(contract =>
            new ContractDto
            {
                Id = contract.Id,
                Date = contract.Date,
                Number = contract.Number,
                LastUpdate = contract.LastUpdate
            });
            foreach (var contract in contracts_dto)
            {
                Data.Items.Add(contract);
            }
        }

        private async void DeleteRecord(object sender, RoutedEventArgs e)
        {
            if (!deleteState) return;

            var strId = Id.Content.ToString();
            if (!int.TryParse(strId, out var intId))
            {
                MessageBox.Show("Wrong Id");
                return;
            }
            _repo.DeleteContractById(intId);
            await GetAllContacts();
        }

        private void ToggleDeleteState(object sender, RoutedEventArgs e)
        {
            deleteState = !deleteState;
            DeleteToggle.IsChecked = deleteState;
        }

        private async void AddNewContract(object sender, RoutedEventArgs e)
        {
            var strDateTime = Date.Text;
            var strLastUpdate = LastUpdate.Text;
            var strNumber = Number.Text;

            if (!DateTime.TryParse(strDateTime, out var dt))
            {
                MessageBox.Show("Wrong Date");
                return;
            }
            if (!DateTime.TryParse(strLastUpdate, out var lu))
            {
                MessageBox.Show("Wrong LastUpdate");
                return;
            }
            if (!int.TryParse(strNumber, out var number))
            {
                MessageBox.Show("Wrong Number");
                return;
            }

            var contract = new Contract {Date = dt, LastUpdate = lu, Number = number };
            _repo.AddContract(contract);
            await GetAllContacts();
        }
    }
    internal class ContractDto
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public int Number { get; set; }
        public DateTime LastUpdate { get; set; }
        public bool Currentness { get { return (DateTime.UtcNow - LastUpdate).Days <= 60; } set { } }
    }
}
