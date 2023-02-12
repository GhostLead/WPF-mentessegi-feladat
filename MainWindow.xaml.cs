using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Microsoft.Win32;
using System.Security.Policy;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Security.Cryptography;
using System.Xml.Linq;
using System.Windows.Interop;

namespace NevGenerator
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            txtNevSzam.Text = "0";
            stbRendezett.Visibility = Visibility.Hidden;

        }

        private ObservableCollection<String> csaladNevek = new ObservableCollection<string>();
        private ObservableCollection<String> utoNevek = new ObservableCollection<string>();


        private void btnBetoltCsNev_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();

            if (ofd.ShowDialog() == true)
            {
                foreach (var nev in File.ReadAllLines(ofd.FileName).ToList())
                {
                    csaladNevek.Add(nev);
                }
                lbCsaladnevek.ItemsSource = csaladNevek;

                lblCsaladnevekErtek.Content = csaladNevek.Count;

                slNevSzam.Maximum = csaladNevek.Count;
                lblNevSzamMaxErtek.Content = slNevSzam.Maximum;

            }
        }

        private void btnBetoltUNev_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();

            if (ofd.ShowDialog() == true)
            {
                foreach (var nev in File.ReadAllLines(ofd.FileName).ToList())
                {
                    utoNevek.Add(nev);
                }
                lbUtonevek.ItemsSource = utoNevek;

                lblUtonevekErtek.Content = utoNevek.Count;
            }
        }

        private void slNevSzam_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            txtNevSzam.Text = Math.Floor(slNevSzam.Value).ToString();
        }


        private void txtNevSzam_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (txtNevSzam.Text == "")
            {
                slNevSzam.Value = 0;
            }
            else
            {
                slNevSzam.Value = Math.Floor(Convert.ToDouble(txtNevSzam.Text));
            }

        }

        private void btnNevetGenaral_Click(object sender, RoutedEventArgs e)
        {

            if (lbCsaladnevek.Items.Count == 0 || lbUtonevek.Items.Count == 0)
            {
                MessageBox.Show("Nincs miből nevet generálni!");
            }
            else
            {
                Random rand = new Random();
                string csaladnev = "";
                string utonev = "";

                if (slNevSzam.Value != Convert.ToUInt32(txtNevSzam.Text))
                {
                    txtNevSzam.Text = slNevSzam.Value.ToString();
                }


                for (int i = 0; i < slNevSzam.Value; i++)
                {
                    int randomNumber = rand.Next(0, csaladNevek.Count);
                    csaladnev = csaladNevek[randomNumber];
                    csaladNevek.Remove(csaladnev);

                    if (rbtnOne.IsChecked == true)
                    {
                        randomNumber = rand.Next(0, utoNevek.Count);
                        utonev = utoNevek[randomNumber];
                        utoNevek.Remove(utonev);
                    }
                    else if (rbtnTwo.IsChecked == true)
                    {
                        randomNumber = rand.Next(0, utoNevek.Count);
                        utonev = utoNevek[randomNumber];
                        utoNevek.Remove(utonev);
                        randomNumber = rand.Next(0, utoNevek.Count);
                        utonev += " " + utoNevek[randomNumber];
                        utoNevek.RemoveAt(randomNumber);
                    }

                    string nev = csaladnev + " " + utonev;
                    lbNevek.Items.Add(nev);

                    lbNevek.Items.MoveCurrentToLast();
                    lbNevek.ScrollIntoView(lbNevek.Items.CurrentItem);

                }

                lblCsaladnevekErtek.Content = csaladNevek.Count;
                lblUtonevekErtek.Content = utoNevek.Count;
                lblNevSzamMaxErtek.Content = csaladNevek.Count;
                slNevSzam.Maximum = csaladNevek.Count;
                stbRendezett.Visibility = Visibility.Hidden;
            }



        }

        private void btnGeneraltNevekTorlese_Click(object sender, RoutedEventArgs e)
        {

            if (lbNevek.Items.Count == 0)
            {
                MessageBox.Show("Üres névsort szeretne törölni!");
            }
            else
            {
                foreach (var item in lbNevek.Items)
                {
                    string[] teljesNev = item.ToString().Split(" ");


                    if (teljesNev.Length == 3)
                    {

                        if (csaladNevek.Contains(teljesNev[0]))
                        {
                            continue;
                        }
                        else
                        {
                            csaladNevek.Add(teljesNev[0]);
                        }

                        if (utoNevek.Contains(teljesNev[1]))
                        {
                            continue;
                        }
                        else
                        {
                            utoNevek.Add(teljesNev[1]);
                        }
                        if (utoNevek.Contains(teljesNev[2]))
                        {
                            continue;
                        }
                        else
                        {
                            utoNevek.Add(teljesNev[2]);
                        }


                    }
                    else
                    {

                        if (csaladNevek.Contains(teljesNev[0]))
                        {
                            continue;
                        }
                        else
                        {
                            csaladNevek.Add(teljesNev[0]);
                        }

                        if (utoNevek.Contains(teljesNev[1]))
                        {
                            continue;
                        }
                        else
                        {
                            utoNevek.Add(teljesNev[1]);
                        }
                    }

                }
                lbNevek.Items.Clear();
                lblCsaladnevekErtek.Content = csaladNevek.Count;
                lblUtonevekErtek.Content = utoNevek.Count;
                lblNevSzamMaxErtek.Content = csaladNevek.Count;
                slNevSzam.Maximum = csaladNevek.Count;
                lbCsaladnevek.Items.MoveCurrentToLast();
                lbCsaladnevek.ScrollIntoView(lbCsaladnevek.Items.CurrentItem);
                lbUtonevek.Items.MoveCurrentToLast();
                lbUtonevek.ScrollIntoView(lbUtonevek.Items.CurrentItem);
                stbRendezett.Visibility = Visibility.Hidden;
            }


        }

        private void btnNevekRendezese_Click(object sender, RoutedEventArgs e)
        {

            if (lbNevek.Items.Count == 0)
            {
                MessageBox.Show("Üres névsort szeretne rendezni!");
            }
            else
            {
                lbNevek.Items.SortDescriptions.Add(new SortDescription("", ListSortDirection.Ascending));
                stbRendezett.Visibility = Visibility.Visible;
            }



        }

        private void btnNevekMentese_Click(object sender, RoutedEventArgs e)
        {

            SaveFileDialog sfd = new SaveFileDialog();
            sfd.AddExtension = true;
            sfd.DefaultExt = "txt";
            sfd.Filter = "Szöveges fájl (*.txt)|*.txt|CSV fájl (*.csv)|*.csv|Összes fájl (*.*)|*.*";
            sfd.Title = "Adja meg a névsor nevét!";

            if (lbNevek.Items.Count == 0)
            {
                MessageBox.Show("Üres névsort szeretne elmenteni!");
            }
            else
            {
                if (sfd.ShowDialog() == true)
                {
                    StreamWriter SaveFile = new StreamWriter(sfd.FileName);


                    if (sfd.FilterIndex == 2)
                    {
                        string csvNev = "";

                        foreach (var item in lbNevek.Items)
                        {
                            string[] teljesNev = item.ToString().Split(" ");
                            if (teljesNev.Length == 2)
                            {
                                csvNev = teljesNev[0] + ";" + teljesNev[1];
                            }
                            else if (teljesNev.Length == 3)
                            {
                                csvNev = teljesNev[0] + ";" + teljesNev[1] + ";" + teljesNev[2];
                            }


                            SaveFile.WriteLine(csvNev);
                        }
                        SaveFile.Close();

                    }
                    else if (sfd.FilterIndex == 1)
                    {
                        foreach (var item in lbNevek.Items)
                        {
                            SaveFile.WriteLine(item.ToString());
                        }
                        SaveFile.Close();
                    }

                    MessageBox.Show("Sikeres mentés!");

                }
            }



        }

        private void lbNevek_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            string selectedItem = "";

            foreach (var item in lbNevek.Items)
            {
                if (item == lbNevek.SelectedItem)
                {
                    selectedItem = item.ToString();
                }
                else
                {
                    continue;
                }

            }

            string[] teljesNev = selectedItem.Split(" ");


            if (teljesNev.Length == 3)
            {
 
            if (csaladNevek.Contains(teljesNev[0]))
                {

                }
                else
                {
                    csaladNevek.Add(teljesNev[0]);
                }

                if (utoNevek.Contains(teljesNev[1]))
                {

                }
                else
                {
                    utoNevek.Add(teljesNev[1]);
                }
                if (utoNevek.Contains(teljesNev[2]))
                {

                }
                else
                {
                    utoNevek.Add(teljesNev[2]);
                }
            }
            else if (teljesNev.Length == 2)
            {
                
                
                    if (csaladNevek.Contains(teljesNev[0]))
                    {
                        
                    }
                    else
                    {
                        csaladNevek.Add(teljesNev[0]);
                    }

                    if (utoNevek.Contains(teljesNev[1]))
                    {
                        
                    }
                    else
                    {
                        utoNevek.Add(teljesNev[1]);
                    }
                
            }
                lbCsaladnevek.Items.Refresh();
                lbUtonevek.Items.Refresh();
                lblCsaladnevekErtek.Content = csaladNevek.Count;
                lblUtonevekErtek.Content = utoNevek.Count;
                lblNevSzamMaxErtek.Content = csaladNevek.Count;
                slNevSzam.Maximum = csaladNevek.Count;
                lbCsaladnevek.Items.MoveCurrentToLast();
                lbCsaladnevek.ScrollIntoView(lbCsaladnevek.Items.CurrentItem);
                lbUtonevek.Items.MoveCurrentToLast();
                lbUtonevek.ScrollIntoView(lbUtonevek.Items.CurrentItem);
                stbRendezett.Visibility = Visibility.Hidden;
                lbNevek.Items.Remove(lbNevek.SelectedItem);

            }


        }
    
}
