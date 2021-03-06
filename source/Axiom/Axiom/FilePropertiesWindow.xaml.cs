﻿/* ----------------------------------------------------------------------
Axiom UI
Copyright (C) 2017 Matt McManis
http://github.com/MattMcManis/Axiom
http://axiomui.github.io
axiom.interface@gmail.com

This program is free software: you can redistribute it and/or modify
it under the terms of the GNU General Public License as published by
the Free Software Foundation, either version 3 of the License, or
(at your option) any later version.

This program is distributed in the hope that it will be useful,
but WITHOUT ANY WARRANTY; without even the implied warranty of
MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE. See the
GNU General Public License for more details.

You should have received a copy of the GNU General Public License
along with this program.If not, see <http://www.gnu.org/licenses/>. 
---------------------------------------------------------------------- */

using System.Windows;
using System.Windows.Documents;
// Disable XML Comment warnings
#pragma warning disable 1591

namespace Axiom
{
    /// <summary>
    /// Interaction logic for Console.xaml
    /// </summary>
    public partial class FilePropertiesWindow : Window
    {
        private MainWindow mainwindow;

        public FilePropertiesWindow(MainWindow mainwindow)
        {
            InitializeComponent();

            // Set Width/Height to prevent Tablets maximizing
            //this.Width = 420;
            //this.Height = 400;
            this.MinWidth = 200;
            this.MinHeight = 200;

            this.mainwindow = mainwindow;
        }

        /// <summary>
        ///    Window Loaded
        /// </summary>
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            // Display FFprobe File Properties

            Paragraph propertiesParagraph = new Paragraph(); //RichTextBox
            this.rtbFileProperties.Document = new FlowDocument(propertiesParagraph); // start
            this.rtbFileProperties.BeginChange(); // begin change

            // Clear Rich Text Box on Start
            propertiesParagraph.Inlines.Clear();

            // Write All File Properties to Rich Text Box
            //propertiesParagraph.Inlines.Add(new Run(FFprobe.inputFileProperties) { Foreground = Log.ConsoleDefault });
            FFprobe.argsFileProperties = " -i" + " " + "\"" + mainwindow.tbxInput.Text + "\"" + " -v quiet -print_format ini -show_format -show_streams";
            FFprobe.inputFileProperties = FFprobe.InputFileInfo(mainwindow, FFprobe.argsFileProperties);

            propertiesParagraph.Inlines.Add(new Run(FFprobe.inputFileProperties) { Foreground = Log.ConsoleDefault });

            this.rtbFileProperties.EndChange(); // end change
        }


        /// <summary>
        /// Close
        /// </summary>
        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            e.Cancel = true;
            this.Close();
        }


        /// <summary>
        /// Expand Button
        /// </summary>
        private void buttonExpand_Click(object sender, RoutedEventArgs e)
        {
            // If less than 600px Height
            if (this.Height <= 650)
            {
                this.Width = 650;
                this.Height = 600;

                double screenWidth = SystemParameters.PrimaryScreenWidth;
                double screenHeight = SystemParameters.PrimaryScreenHeight;
                double windowWidth = this.Width;
                double windowHeight = this.Height;
                this.Left = (screenWidth / 2) - (windowWidth / 2);
                this.Top = (screenHeight / 2) - (windowHeight / 2);
            }
        }
    }
}