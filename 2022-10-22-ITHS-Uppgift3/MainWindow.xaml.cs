﻿using System;
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

namespace _2022_10_22_ITHS_Uppgift3
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public int[] bord { get; set; }
        public string[] tider { get; set; }
        List<Bokning> bokningar = new List<Bokning>();
        public MainWindow()
        {
            InitializeComponent();

            bord = new int[] { 1, 2, 3, 4, 5 };
            tider = new string[] { "16:00-19:00", "19:00-22:00" };
            bokningar.Add(new Bokning("2022-10-16", tider[0], "Joel", bord[0]));
            bokningar.Add(new Bokning("2022-10-16", tider[1], "Joel", bord[1]));
            bokningar.Add(new Bokning("2022-10-17", tider[1], "Joel", bord[2]));
            bokningar.Aggregate("", (values, nextvalue) => values += bokningsBox.Items.Add($"{bokningar.IndexOf(nextvalue) + 1}, {nextvalue.datum} {nextvalue.tider} {nextvalue.name} bord: {nextvalue.table}" + "\n"));
            DataContext = this;

        }

        private void btn_visaBokningar_Click(object sender, RoutedEventArgs e)
        {
            visaAllaBokningar();
            //bokningsBox.Items.Clear();
            //bokningar.Aggregate("", (values, nextvalue) => values += bokningsBox.Items.Add($"{bokningar.IndexOf(nextvalue) + 1}, {nextvalue.datum} {nextvalue.tider} {nextvalue.name} bord: {nextvalue.table}" + "\n"));

        }

        private void btn_boka_Click(object sender, RoutedEventArgs e)
        {

            bokaBord();
            //    bool fieldMissing = false;
            //    bool tableAvailable = true;
            //    if (String.IsNullOrEmpty(datepick.Text.ToString()) || String.IsNullOrEmpty(tidCombo.Text.ToString()) || String.IsNullOrEmpty(nameBox.Text.ToString()) || String.IsNullOrEmpty(bordCombo.Text.ToString()))
            //    {
            //        fieldMissing = true;
            //    }

            //    foreach (var bokning in bokningar)
            //    {

            //        if (datepick.Text.ToString() == bokning.datum && tidCombo.Text.ToString() == bokning.tider && bordCombo.Text.ToString() == bokning.table.ToString())
            //        {
            //            tableAvailable = false;
            //        }
            //    }

            //    if (fieldMissing)
            //    {

            //        MessageBox.Show("Du skrev inte in alla fält, gör ett nytt försök");

            //    }
            //    else
            //    {
            //        if (tableAvailable)
            //        {
            //            bokningar.Add(new Bokning(datepick.Text.ToString(), tidCombo.Text.ToString(), nameBox.Text.ToString(), Int32.Parse(bordCombo.Text)));
            //            MessageBox.Show("Ditt bort är bokat");
            //            datepick.Text = "";
            //            tidCombo.Text = "";
            //            bordCombo.Text = "";
            //            nameBox.Text = "";
            //            bokningsBox.Items.Clear();
            //        }
            //        else
            //        {
            //            MessageBox.Show("Tiden är redan upptagen försök att göra en ny bokning på en annan tid");
            //            bokningsBox.Items.Clear();
            //        }
            //    }
        }

        private void btn_avboka_Click(object sender, RoutedEventArgs e)
        {
            avbokaBord();
            //if (bokningsBox.Items.Count > 0)
            //{
            //    int val = bokningsBox.SelectedIndex;
            //    bokningar.RemoveAt(val);
            //    bokningsBox.Items.RemoveAt(bokningsBox.SelectedIndex);
            //}
            //else
            //{
            //    MessageBox.Show("Det finns inga bokningar");
            //}
        }

        private void btn_saveFile_Click(object sender, RoutedEventArgs e)
        {
            saveToFile();
            //SaveFileDialog dlg = new SaveFileDialog();
            //dlg.Filter = "(*.txt) | *.txt";

            //if (dlg.ShowDialog() == true)
            //{
            //    StreamWriter sw = new StreamWriter(dlg.FileName);

            //    for (int i = 0; i < bokningsBox.Items.Count; i++)
            //    {
            //        sw.WriteLine(bokningsBox.Items[i].ToString());
            //    }
            //    sw.Close();
            //}
        }

        private void btn_readFile_Click(object sender, RoutedEventArgs e)
        {
            readFromFile();
            //bokningsBox.Items.Clear();
            //OpenFileDialog dlg = new OpenFileDialog();

            //dlg.Filter = "Text Files(.txt) | *.txt";


            //var result1 = dlg.ShowDialog();
            //if (result1 == true)
            //{
            //    var lines = File.ReadAllLines(dlg.FileName);
            //    foreach (var line in lines)
            //    {
            //        bokningsBox.Items.Add(line);
            //    }
            //}
        }

        public void bokaBord()
        {
            bool fieldMissing = false;
            bool tableAvailable = true;
            if (String.IsNullOrEmpty(datepick.Text.ToString()) || String.IsNullOrEmpty(tidCombo.Text.ToString()) || String.IsNullOrEmpty(nameBox.Text.ToString()) || String.IsNullOrEmpty(bordCombo.Text.ToString()))
            {
                fieldMissing = true;
            }

            foreach (var bokning in bokningar)
            {

                if (datepick.Text.ToString() == bokning.datum && tidCombo.Text.ToString() == bokning.tider && bordCombo.Text.ToString() == bokning.table.ToString())
                {
                    tableAvailable = false;
                }
            }

            if (fieldMissing)
            {

                MessageBox.Show("Du skrev inte in alla fält, gör ett nytt försök");

            }
            else
            {
                if (tableAvailable)
                {
                    bokningar.Add(new Bokning(datepick.Text.ToString(), tidCombo.Text.ToString(), nameBox.Text.ToString(), Int32.Parse(bordCombo.Text)));
                    MessageBox.Show("Ditt bort är bokat");
                    datepick.Text = "";
                    tidCombo.Text = "";
                    bordCombo.Text = "";
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
        public void avbokaBord()
        {
            if (bokningsBox.Items.Count > 0)
            {
                int val = bokningsBox.SelectedIndex;
                bokningar.RemoveAt(val);
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

                foreach(var bokning in bokningar)
                {
                    sw.WriteLine(bokning.datum + " " + bokning.tider + " " + bokning.table + " " + bokning.name);
                }
                //for (int i = 0; i < bokningsBox.Items.Count; i++)
                //{
                //    sw.WriteLine(bokningsBox.Items[i].ToString());
                //}
                sw.Close();
            }
        }
        public void readFromFile()
        {
            bokningsBox.Items.Clear();
            OpenFileDialog dlg = new OpenFileDialog();

            dlg.Filter = "Text Files(.txt) | *.txt";


            var result1 = dlg.ShowDialog();
            if (result1 == true)
            {
                var lines = File.ReadAllLines(dlg.FileName);
                foreach (var line in lines)
                {
                    bokningsBox.Items.Add(line);
                }
            }
        }
        public void visaAllaBokningar()
        {
            bokningsBox.Items.Clear();
            bokningar.Aggregate("", (values, nextvalue) => values += bokningsBox.Items.Add
            ($"{bokningar.IndexOf(nextvalue) + 1}, {nextvalue.datum} {nextvalue.tider} {nextvalue.name}" +
            $" bord: {nextvalue.table}" + "\n"));
        }
    }

}


