using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace JobBoard
{
    class SwitchPage
    {

        internal static Screen CurrentPage;
        private static UserControl currentControl;
        private static UserControl prevControl;

        public static void Next(UserControl page)
        {
            if (currentControl == null)
            {
                currentControl = page;
                CurrentPage.Navigate(currentControl);
            }
            else
            {
                prevControl = currentControl;
                currentControl = page;
                CurrentPage.Navigate(currentControl);
            }
        }

        public static void Back()
        {
            if(prevControl != null)
            {
                UserControl temp = currentControl;
                currentControl = prevControl;
                prevControl = temp;
                CurrentPage.Navigate(prevControl);
            }
        }


    }
}
