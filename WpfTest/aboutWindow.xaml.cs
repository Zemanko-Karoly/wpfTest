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
using System.Windows.Shapes;

namespace WpfTest
{
    /// <summary>
    /// Interaction logic for aboutWindow.xaml
    /// </summary>
    public partial class aboutWindow : Window
    {
        public aboutWindow()
        {
            InitializeComponent();
            Layout();
            Title = "Névjegy";
            Width = 300;
            Height = 200;
            ResizeMode = ResizeMode.NoResize;
        }

        private void Layout()
        {


            //create wrap for content
            WrapPanel wrap = new WrapPanel();
            wrap.Orientation = Orientation.Vertical;
            wrap.HorizontalAlignment = HorizontalAlignment.Center;
            wrap.VerticalAlignment = VerticalAlignment.Center;

            Content = wrap;


            //hello world ans settings
            Label world = new Label() { Content = "Hello World!" };
            world.FontSize = 22;
            world.FontWeight = FontWeights.Bold;


            //ok button
            Button ok = new Button();
            ok.Name = "btn1";
            ok.Click += btn1_Click;
            ok.Content = "OK";
            ok.Height = 40;
            ok.Width = 100;
            ok.Margin = new Thickness(10);


            //add to wrap
            wrap.Children.Add(world);
            wrap.Children.Add(ok);
        }

        //ok button click event
        private void btn1_Click(object sender, EventArgs e)
        {
            //close the window this button is on
            Close();
        }
    }
}
