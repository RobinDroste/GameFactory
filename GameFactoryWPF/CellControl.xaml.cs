﻿using ClassLibrary;
using System;
using System.Windows;
using System.Windows.Controls;

namespace GameFactoryWPF
{
    /// <summary>
    /// Interaction logic for CellControl.xaml
    /// </summary>
    public partial class CellControl : UserControl
    {
        public static readonly DependencyProperty CellContentProperty =
            DependencyProperty.Register("CellContent", typeof(string), typeof(CellControl));

        public event EventHandler<CellClickedEventArgs> CellClicked;


        public int Row { get; set; }
        public int Column { get; set; }
        public bool IsClicked { get; set; } = false;

        public object CellContent
        {
            get { return (string)GetValue(CellContentProperty); }
            set { CellButton.Content = value; }
        }
        public string CellColor
        {
            get { return CellButton.Background.ToString(); }
            set { CellButton.Background = (System.Windows.Media.Brush)new System.Windows.Media.BrushConverter().ConvertFromString(value); }
        }

        public CellControl()
        {
            InitializeComponent();
            CellButton.Click += CellButton_Click;
        }

        private void CellButton_Click(object sender, RoutedEventArgs e)
        {
            CellClicked?.Invoke(this, new CellClickedEventArgs(Row, Column));
            IsClicked = true;
        }

    }

}
