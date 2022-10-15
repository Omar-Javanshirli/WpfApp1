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

namespace WpfApp1.ViewModel
{
    public class AppViewModel:BaseViewModel
    {
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
            ScreenShot();
        }

        public void Undo()
        {
            UndoCommand = new RelayCommand((e) =>
              {
                  var momento = new CareTaker(imagePath);
              });
        }

        public void ScreenShot()
        {
            ScreenShootCommand = new RelayCommand((e) =>
            {
                try
                {
                    //Creating a new Bitmap object
                    Bitmap captureBitmap = new Bitmap(1024, 768, System.Drawing.Imaging.PixelFormat.Format32bppArgb);
                    //Bitmap captureBitmap = new Bitmap(int width, int height, PixelFormat);
                    //Creating a Rectangle object which will
                    //capture our Current Screen
                    Rectangle captureRectangle = Screen.AllScreens[0].Bounds;
                    //Creating a New Graphics Object
                    Graphics captureGraphics = Graphics.FromImage(captureBitmap);
                    //Copying Image from The Screen
                    captureGraphics.CopyFromScreen(captureRectangle.Left, captureRectangle.Top, 0, 0, captureRectangle.Size);
                    //Saving the Image File (I am here Saving it in My E drive).
                    captureBitmap.Save(@"C:\Users\stepguest\Desktop\index\Capture.jpg", ImageFormat.Jpeg);
                    //Displaying the Successfull Result

                    ImagePath = @"C:\Users\stepguest\Desktop\index\Capture.jpg";
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            });
        
        }

    }
}
