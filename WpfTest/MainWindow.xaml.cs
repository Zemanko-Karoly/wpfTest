using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Threading;
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
using Microsoft.Win32;

namespace WpfTest
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        DispatcherTimer timer;
        Label time;

        public MainWindow()
        {
            InitializeComponent();
            mainLayout();
            //window settings
            Title = "WPF tesztfeladat";
            WindowState = WindowState.Maximized;
            ResizeMode = ResizeMode.CanResizeWithGrip;
            Width = 1000;
            Height = 600;

        }

        void mainLayout()
        {
            //creating grid
            var grid = new Grid();
            Content = grid;
            //grid.ShowGridLines = true;

            //header
            grid.RowDefinitions.Add(new RowDefinition() { Height = new GridLength(60) });

            //middle row
            grid.RowDefinitions.Add(new RowDefinition() { Height = new GridLength(1, GridUnitType.Star) });

            //footer
            grid.RowDefinitions.Add(new RowDefinition() { Height = new GridLength(50) });

            //left side
            grid.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(3, GridUnitType.Star) });

            //right side
            grid.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(7, GridUnitType.Star) });


            var footer = new Label();


            //create WrapPanels
            //wrap buttons in header
            var wrap = new WrapPanel();
            wrap.Orientation = Orientation.Horizontal;
            wrap.HorizontalAlignment = HorizontalAlignment.Left;
            wrap.VerticalAlignment = VerticalAlignment.Top;

            //wrap time and canvas
            var wrap2 = new WrapPanel();
            wrap2.Orientation = Orientation.Vertical;
            wrap2.Height = 600;
            wrap2.HorizontalAlignment = HorizontalAlignment.Center;
            wrap2.VerticalAlignment = VerticalAlignment.Stretch;

            //timer
            timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromSeconds(1);
            timer.Tick += Timer_Tick;
            timer.Start();

            time = new Label();
            time.Margin = new Thickness(10);

            //refresh time
            void Timer_Tick(object sender, EventArgs e)
            {
                time.Content = DateTime.Now.ToString();

            }

            //create buttons
            //choose image button
            Button image = new Button();
            image.Name = "btn1";
            image.Click += btn1_Click;
            image.Content = "Kép kiválasztása";
            image.Height = 40;
            image.Width = 100;
            image.Background = Brushes.LightGreen;
            image.Margin = new Thickness(10);

            //about button
            Button about = new Button();
            about.Name = "btn2";
            about.Click += btn2_Click;
            about.Margin = new Thickness(10);
            about.Content = "Névjegy";
            about.Height = 40;
            about.Width = 100;
            about.Background = Brushes.LightGreen;


            //place elements
            grid.Children.Add(footer);
            wrap.Children.Add(image);
            wrap.Children.Add(about);
            wrap2.Children.Add(time);
            grid.Children.Add(wrap);
            grid.Children.Add(wrap2);
            Grid.SetRow(wrap, 0);
            Grid.SetColumnSpan(wrap, 2);
            Grid.SetRow(wrap2, 1);
            Grid.SetRow(footer, 2);
            Grid.SetColumnSpan(footer, 2);




            //choose image button clik event
            void btn1_Click(object sender, EventArgs e)
            {

                Image photo = new Image();
                photo.Stretch = Stretch.Uniform;
                grid.Children.Add(photo);

                //open dialog to choose picture
                OpenFileDialog loadImage = new OpenFileDialog();
                loadImage.Title = "Kép kiválasztása";

                //limit search to image formats
                loadImage.Filter = "JPG Files (*.jpg)|*.jpg|BMP Files (*.bmp)|*.bmp| PNG Files (*.png)|*.png";

                //if choosen
                if (loadImage.ShowDialog() == true)
                {
                    //load image
                    string fileName = loadImage.FileName;
                    BitmapImage bi = new BitmapImage();
                    bi.BeginInit();
                    bi.UriSource = new Uri(fileName, UriKind.RelativeOrAbsolute);
                    bi.EndInit();
                    photo.Source = bi;
                    Grid.SetRow(photo, 1);
                    Grid.SetColumn(photo, 1);
                    footer.Content = fileName;

                }

                //create elements to draw
                Canvas canv = new Canvas();
                Ellipse smiley = new Ellipse();
                Ellipse eyeLeft = new Ellipse();
                Ellipse eyeRight = new Ellipse();

                //create mouth geometry
                Path smile = new Path();
                PathGeometry mouth = new PathGeometry();
                PathFigureCollection figures = new PathFigureCollection();
                PathFigure mouthFigure = new PathFigure();
                mouthFigure.StartPoint = new Point(50, 80);
                ArcSegment line = new ArcSegment(new Point(120, 80), new Size(25, 15), 0, false, SweepDirection.Counterclockwise, true);
                mouthFigure.Segments.Add(line);
                figures.Add(mouthFigure);
                mouth.Figures = figures;
                smile.Data = mouth;

                //head ellipse settings
                smiley.Fill = Brushes.Yellow;
                smiley.Stroke = Brushes.DarkOrange;
                smiley.StrokeThickness = 5;
                smiley.Width = 200;
                smiley.Height = 200;

                //left eye settings
                eyeLeft.Fill = Brushes.White;
                eyeLeft.Stroke = Brushes.Black;
                eyeLeft.StrokeThickness = 1;
                eyeLeft.Width = 50;
                eyeLeft.Height = 50;

                //right eye settings
                eyeRight.Fill = Brushes.White;
                eyeRight.Stroke = Brushes.Black;
                eyeRight.StrokeThickness = 1;
                eyeRight.Width = 50;
                eyeRight.Height = 50;

                //mouth settings
                smile.Stroke = Brushes.Black;
                smile.StrokeThickness = 3;



                //add to canvas
                canv.Children.Add(smiley);
                canv.Children.Add(eyeLeft);
                canv.Children.Add(eyeRight);
                canv.Children.Add(smile);

                //add canvas to wrappanel
                wrap2.Children.Add(canv);


                //placeing on canvas
                Canvas.SetLeft(eyeLeft, 10);
                Canvas.SetTop(eyeLeft, 245);

                Canvas.SetLeft(eyeRight, 90);
                Canvas.SetTop(eyeRight, 245);

                Canvas.SetLeft(smiley, -25);
                Canvas.SetTop(smiley, 200);

                Canvas.SetLeft(smile, -10);
                Canvas.SetTop(smile, 250);

            }


            //about button click event
            void btn2_Click(object sender, EventArgs e)
            {
                aboutWindow win2 = new aboutWindow();
                win2.Show();
            }

        }



    }
}
