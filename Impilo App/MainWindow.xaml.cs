﻿using System;
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

using Impilo_App.Views.Home;
using Impilo_App.Views.Client;
using Impilo_App.Views.Screening;
using Impilo_App.Views.ClinicData;
using Impilo_App.Views.FollowUpVisit;
using Impilo_App.Views.CHOW;
using Impilo_App.Views.Reports;
using Impilo_App.DataImport;

namespace Impilo_App
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            ListClients newPage = new ListClients();
            pageTransitionControl.ShowPage(newPage);
            cboDate.Content = DateTime.Now.ToString("dd MMMM yyyy");
            lbTime.Content = DateTime.Now.ToString("hh:mm:ss tt");
        }

        private void btnNewClient_Click(object sender, RoutedEventArgs e)
        {
             AddNewClient newPage  = new AddNewClient();
            pageTransitionControl.ShowPage(newPage);
        }

        private void btnScreening_Click(object sender, RoutedEventArgs e)
        {
            ScreeningHome home = new ScreeningHome();
            pageTransitionControl.ShowPage(home);
        }

        private void button_Copy2_Click(object sender, RoutedEventArgs e)
        {
            ClinicVisit clinic = new ClinicVisit();
            
            pageTransitionControl.ShowPage(clinic);
        }

        private void btnFollowUp_Click(object sender, RoutedEventArgs e)
        {
            FollowUp follow = new FollowUp();
            pageTransitionControl.ShowPage(follow);
        }

        private void AddChow_Click(object sender, RoutedEventArgs e)
        {
            AddChow chow = new AddChow();
            pageTransitionControl.ShowPage(chow);
        }

      

        private void ViewChow_Click(object sender, RoutedEventArgs e)
        {
            ViewChow vchow = new ViewChow();
            pageTransitionControl.ShowPage(vchow);
        }

        private void HomePage_Click_1(object sender, RoutedEventArgs e)
        {
            ListClients newPage = new ListClients();
            pageTransitionControl.ShowPage(newPage);
        }

        private void Format2_Click(object sender, RoutedEventArgs e)
        {
            Format2Report format2Report = new Format2Report();
            pageTransitionControl.ShowPage(format2Report);
        }

        private void ScreeningImport_Click(object sender, RoutedEventArgs e)
        {
            System.Windows.Forms.FolderBrowserDialog dlg = new System.Windows.Forms.FolderBrowserDialog();

            if (dlg.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                DataImport.ScreeningImport.Import(dlg.SelectedPath);
            }
        }

        private void FollowupImport_Click(object sender, RoutedEventArgs e)
        {
            System.Windows.Forms.FolderBrowserDialog dlg = new System.Windows.Forms.FolderBrowserDialog();

            if (dlg.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                //DataImport.FollowUpImport.Import(dlg.SelectedPath);
            }
        }

        private void ClinicImport_Click(object sender, RoutedEventArgs e)
        {
            System.Windows.Forms.FolderBrowserDialog dlg = new System.Windows.Forms.FolderBrowserDialog();

            if (dlg.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                //DataImport.ClinicImport.Import(dlg.SelectedPath);
            }
        }

      

      

    }
}
