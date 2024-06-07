using Avalonia.Controls;
using CommunityToolkit;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
namespace Avalgame.ViewModels
{
    public enum Page { Home, Game }
    public partial class MainWindowViewModel : ViewModelBase
    {
        private static MainWindowViewModel? instance;
        public static MainWindowViewModel Instance => instance!;
        public MainWindowViewModel()
        {
            instance = this;
            pages = new(){
                { Page.Home, new HomePageViewModel() },
                { Page.Game, new GamePageViewModel() } };
            ChangePage(Page.Home);
        }
        [ObservableProperty]
        private ViewModelBase? _curPage;
        private Dictionary<Page, ViewModelBase> pages;
        /// <summary>
        /// 切换页面的方法
        /// </summary>
        /// <param name="target">目标页面</param>
        public void ChangePage(Page target) => CurPage = pages[target];
    }
}
