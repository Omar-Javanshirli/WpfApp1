using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using WpfApp1.Command;
using WpfApp1.Momento;
using Image = System.Windows.Controls.Image;
namespace WpfApp1.ViewModel
{
    public class AppViewModel:BaseViewModel
    {
        public Originator originator { get; set; }
        public CareTaker careTaker { get; set; }
        public RelayCommand ScreenShootCommand { get; set; }
        public RelayCommand UndoCommand { get; set; }
        public RelayCommand RendoCommand { get; set; }
        private string imagePath;

        public string ImagePath
        {
            get { return  imagePath; }
            set {  imagePath = value; OnPropertyChanged(); }
        }

        public AppViewModel()
        {
            originator = new Originator(string.Empty);
            careTaker = new CareTaker(originator);
            ScreenShot();
            UndoBtn();
            RedoBtn();
        }

        public void Undo()
        {
            UndoCommand = new RelayCommand((e) =>
              {
                  //var momento = new CareTaker(imagePath);
              });
        }

        public void ScreenShot()
        {
            ScreenShootCommand = new RelayCommand((e) =>
            {
                String filename = "ScreenCapture-" + DateTime.Now.ToString("ddMMyyyy-hhmmss") + ".png";
                Bitmap bm = new Bitmap(Screen.PrimaryScreen.Bounds.Width, Screen.PrimaryScreen.Bounds.Height);
                Graphics g = Graphics.FromImage(bm);
                g.CopyFromScreen(0, 0, 0, 0, bm.Size);
                filename = Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + filename;
                bm.Save(filename);

                Image finalImage = new Image();
                BitmapImage logo = new BitmapImage();
                logo.BeginInit();
                logo.UriSource = new Uri(filename);
                logo.EndInit();
                finalImage.Source = logo;
                ImagePath = logo.ToString();
                originator.DoSomething(filename);
                careTaker.BackUp();
            });
        
        }

        public void RedoBtn()
        {
            RendoCommand = new RelayCommand((e) =>
            {
                careTaker.Redo();
                try
                {
                    Image finalImage = new Image();
                    BitmapImage logo = new BitmapImage();
                    logo.BeginInit();
                    logo.UriSource = new Uri(careTaker._mementos[careTaker.Index].GetState());
                    logo.EndInit();
                    finalImage.Source = logo;
                    ImagePath= logo.ToString();
                }
                catch (Exception)
                {

                }
            });
        }

        public void UndoBtn()
        {
            UndoCommand = new RelayCommand((e) =>
            {
                careTaker.Undo();

                try
                {
                    Image finalImage = new Image();
                    BitmapImage logo = new BitmapImage();
                    logo.BeginInit();
                    logo.UriSource = new Uri(careTaker._mementos[careTaker.Index].GetState());
                    logo.EndInit();
                    finalImage.Source = logo;
                    ImagePath = logo.ToString();
                }
                catch (Exception)
                {

                }
            });
        }
    }
}
