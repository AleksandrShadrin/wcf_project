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
using WcfService1.Repository;

namespace Client
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        ServerConnection _server;
        IClientFeatures _service;
        bool catchingData;
        public MainWindow()
        {
            InitializeComponent();
            _server = new ServerConnection();
            catchingData = false;
        }

        private void Connect(object sender, RoutedEventArgs e)
        {
            if(_service == default)
            {
                _service = _server.CreateConnection();
            }
        }
        private async void Consume(object sender, RoutedEventArgs e)
        {
            await GetAllContacts();
        }
        private async Task GetAllContacts()
        {
            if (catchingData)
            {
                return;
            }
            Data.Items.Clear();
            var contracts = await Task.Run(() => _service.GetAllContracts());
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
        internal class ContractDto
        {
            public int Id { get; set; }
            public DateTime Date { get; set; }
            public int Number { get; set; }
            public DateTime LastUpdate { get; set; }
            public bool Currentness { get { return (DateTime.UtcNow - LastUpdate).Days <= 60; } set { } }
        }
    }
}
