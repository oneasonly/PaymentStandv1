﻿using ExceptionManager;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace WPFApp
{
    public class ViewPagesManager
    {
        #region fields
        private List<Panel> pages = new List<Panel>();
        private int currentPageIndex = -1;
        private StackPanel tempPnl = new StackPanel() { VerticalAlignment = VerticalAlignment.Center };
        #endregion
        #region Public Methods
        #region Get Page
        public Panel NextPage()
        {
            //Ex.Log($"{nameof(ViewPagesManager)}.{nameof(NextPage)}(): index={currentPageIndex}; count={pages.Count};");
            if (IsNextAvaible)
            {
                currentPageIndex++;
            }
            return GetPage();
        }
        #endregion
        
        public Panel PrevPage()
        {
            //Ex.Log($"{nameof(ViewPagesManager)}.{nameof(PrevPage)}(): index={currentPageIndex}; count={pages.Count};");
            if (IsPrevAvaible)
            {
                currentPageIndex--;
            }
            return GetPage();
        }

        private Panel GetPage()
        {
            //var scrollView = new ScrollViewer() { Content = pages[currentPageIndex] };
            //scrollView.VerticalScrollBarVisibility = ScrollBarVisibility.Auto;
            //return scrollView;
            return pages[currentPageIndex];
        }

        public ViewPagesManager NewPage()
        {
            if (tempPnl.Children.Count > 0)
            {
                tempPnlClear();
                pages.Add(tempPnl);
            }
            CheckEmpty();
            //Ex.Log($"{nameof(ViewPagesManager)}.{nameof(NewPage)}(): pages={pages.Count}; tempPanel.Children={tempPnl.Children.Count}");
            Ex.Try(() =>
            {
                Ex.Log($"page[0]={(pages[0].Children[0] as ContentControl).Content};");
                Ex.Log($"page[1]={(pages[1].Children[0] as ContentControl).Content};");
            });
            return this;
        }
        public ViewPagesManager AddControl(FrameworkElement arg)
        {
            CheckEmpty();
            tempPnl.Children.Add(arg);
            //Ex.Log($"{nameof(ViewPagesManager)}.{nameof(AddControl)}(): pages={pages.Count}; tempPanel.Children={tempPnl?.Children?.Count}");
            return this;
        }

        private void CheckEmpty()
        {
            if (pages.Count <= 0)
            {
                pages.Add(tempPnl);
            }
        }

        public ViewPagesManager AddDataContext(object arg)
        {
            Ex.Log($"{nameof(ViewPagesManager)}.{nameof(AddDataContext)}(): pages={pages.Count}; tempPanel.Children={tempPnl?.Children?.Count}");
            tempPnl.DataContext = arg;
            return this;
        }
        public ViewPagesManager AddPage(Panel arg)
        {
            pages.Add(arg);
            Ex.Log($"{nameof(ViewPagesManager)}.{nameof(AddPage)}(): pages={pages.Count}; tempPanel.Children={tempPnl?.Children?.Count}");
            return this;
        }
        #endregion
        #region Properties
        public Panel this[int index] => pages[index];
        public bool IsNextAvaible => pages.Count-1 > currentPageIndex;
        public bool IsPrevAvaible => 0 < currentPageIndex;
        public int Count  => (tempPnl.Children.Count<=0) ? pages.Count-1 : pages.Count;
        public int CurrIndex => currentPageIndex;
        public Panel Page
            => (currentPageIndex < 0 || currentPageIndex >= pages.Count || pages.Count==0)
            ? null : pages[currentPageIndex];

        public Panel PreviosPage
            => (currentPageIndex < 1 || currentPageIndex >= pages.Count || pages.Count == 0)
            ? null : pages[currentPageIndex - 1];
        #endregion
        #region Private Methods
        private void tempPnlClear()
        {
            tempPnl = new StackPanel() { VerticalAlignment= VerticalAlignment.Center};
        }
        
        #endregion
    }
}
