using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Documents.DocumentStructures;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Xml.Linq;
using _2022_10_22_ITHS_Uppgift3;
using System.IO;
using System.IO.Enumeration;
using Microsoft.Win32;
using System.Text.Json;

namespace _2022_10_22_ITHS_Uppgift3
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public int[] tables { get; set; }
        public string[] time { get; set; }
        List<Booking> bookings = new List<Booking>();
        public MainWindow()
        {
            InitializeComponent();

            tables = new int[] { 1, 2, 3, 4, 5 };
            time = new string[] { "16:00-19:00", "19:00-22:00" };
            tableCombo.Items.Add(1);
            tableCombo.Items.Add(2);
            tableCombo.Items.Add(3);
            tableCombo.Items.Add(4);
            tableCombo.Items.Add(5);
            timeCombo.Items.Add("16:00-19:00");
            timeCombo.Items.Add("19:00-22:00");
            bookings.Add(new Booking("2022-11-16", time[0], "Saga", tables[0]));
            bookings.Add(new Booking("2022-11-16", time[1], "Emma", tables[1]));
            bookings.Add(new Booking("2022-11-17", time[1], "Lea", tables[2]));
            bookings.Aggregate("", (values, nextvalue) => values += bokningsBox.Items.Add($"{bookings.IndexOf(nextvalue) + 1}, {nextvalue.date} {nextvalue.times} {nextvalue.name} bord: {nextvalue.table}" + "\n"));
            DataContext = this;
            bokningsBox.Items.Clear();


        }

        private async void btn_ShowBookings_Click(object sender, RoutedEventArgs e)
        {
            await showAllBookings();
        }

        private void btn_Book_Click(object sender, RoutedEventArgs e)
        {

            bookTable();
        }

        private void btn_Cancel_Click(object sender, RoutedEventArgs e)
        {
            cancelBooking();

        }

        private void btn_saveFile_Click(object sender, RoutedEventArgs e)
        {
            saveToFile();

        }
        private async void btn_readFile_Click(object sender, RoutedEventArgs e)
        {
            await readFromFile();
        }


        public void bookTable()
        {
            bool fieldMissing = false;
            bool tableAvailable = true;
            DateTime chosenDate = DateTime.Parse(datePick.Text);
            DateTime dateNow = DateTime.Parse(DateTime.Now.ToString());
            if (chosenDate < dateNow || String.IsNullOrEmpty(datePick.Text.ToString()) || String.IsNullOrEmpty(timeCombo.Text.ToString()) || String.IsNullOrEmpty(nameBox.Text.ToString()) || String.IsNullOrEmpty(tableCombo.Text.ToString()))
            {

                fieldMissing = true;
            }
            

            foreach (var bokning in bookings)
            {

                if (datePick.Text.ToString() == bokning.date && timeCombo.Text.ToString() == bokning.times && tableCombo.Text.ToString() == bokning.table.ToString())
                {
                    tableAvailable = false;
                }
            }

            if (fieldMissing)
            {

                MessageBox.Show("Du skrev inte in alla fält, eller bokade en tid som redan varit. Gör ett nytt försök");

            }
            else
            {

                if (tableAvailable)
                {
                    bookings.Add(new Booking(datePick.Text.ToString(), timeCombo.Text.ToString(), nameBox.Text.ToString(), Int32.Parse(tableCombo.Text)));
                    MessageBox.Show("Ditt bord är bokat");
                    datePick.Text = "";
                    timeCombo.Text = "";
                    tableCombo.Text = "";
                    nameBox.Text = "";
                    bokningsBox.Items.Clear();
                }
                else
                {
                    MessageBox.Show("Tiden är redan upptagen försök att göra en ny bokning på en annan tid");
                    bokningsBox.Items.Clear();
                }
            }
        }


        public void cancelBooking()
        {
            if (bokningsBox.Items.Count > 0)
            {
                int val = bokningsBox.SelectedIndex;
                bookings.RemoveAt(val);
                bokningsBox.Items.RemoveAt(bokningsBox.SelectedIndex);
            }
            else
            {
                MessageBox.Show("Det finns inga bokningar");
            }
        }
        public void saveToFile()
        {
            SaveFileDialog dlg = new SaveFileDialog();
            dlg.Filter = "(*.txt) | *.txt";

            if (dlg.ShowDialog() == true)
            {
                StreamWriter sw = new StreamWriter(dlg.FileName);

                foreach (var bokning in bookings)
                {
                    sw.WriteLine(bokning.date + " " + bokning.times + " " + bokning.table + " " + bokning.name);
                }

                sw.Close();
            }
        }
        public async Task readFromFile()
        {
            bokningsBox.Items.Clear();
            OpenFileDialog dlg = new OpenFileDialog();

            dlg.Filter = "Text Files(*.txt) | *.txt";
            var result1 = dlg.ShowDialog();
            Loading.Content = "Laddar bokningar från fil...";
            disableButtons();
            await Task.Delay(2000);
            enableButtons();
            Loading.Content = "";
            if (result1 == true)
            {
                var lines = File.ReadAllLines(dlg.FileName);
                foreach (var line in lines)
                {
                    bokningsBox.Items.Add(line);
                }
            }
        }
        public async Task showAllBookings()
        {
            bokningsBox.Items.Clear();
            Loading.Content = "Laddar bokningar...";
            disableButtons();

            await Task.Delay(2000);
            enableButtons();

            Loading.Content = "";
            bookings.Aggregate("", (values, nextvalue) => values += bokningsBox.Items.Add
            ($"{bookings.IndexOf(nextvalue) + 1}, {nextvalue.date} {nextvalue.times} {nextvalue.name}" +
            $" bord: {nextvalue.table}" + "\n"));
        }
        public void disableButtons()
        {
            btn_Book.IsEnabled = false;
            btn_ReadFile.IsEnabled = false;
            btn_saveFile.IsEnabled = false;
            btn_ShowBookings.IsEnabled = false;
            btn_Serializing.IsEnabled = false;
            btn_Deserializing.IsEnabled = false;
        }
        public void enableButtons()
        {
            btn_Book.IsEnabled = true;
            btn_ReadFile.IsEnabled = true;
            btn_saveFile.IsEnabled = true;
            btn_ShowBookings.IsEnabled = true;
            btn_Serializing.IsEnabled = true;
            btn_Deserializing.IsEnabled = true;
        }

        private async void Serializing_Click(object sender, RoutedEventArgs e)
        {
            string fileName = "Testing.json";
            using FileStream createStream = File.Create(fileName);
            await JsonSerializer.SerializeAsync(createStream, bookings);
            
            await createStream.DisposeAsync();
            bokningsBox.Items.Clear();
            MessageBox.Show("Bokningar serialiserade till Json-format till fil: " + fileName);

            //(File.ReadAllText(fileName));
        }

        private void btn_Deserializing_Click(object sender, RoutedEventArgs e)
        {
            string fileName = "Testing.json";
            bokningsBox.Items.Clear();
            string jsonString = File.ReadAllText(fileName);
            List<Booking> temporary = JsonSerializer.Deserialize<List<Booking>>(jsonString)!;
            

            bookings = temporary;

            foreach (Booking book in bookings)
            {
                bokningsBox.Items.Add($"{book.date} {book.times} {book.table} {book.name}");
            }


        }
    }
}
            








