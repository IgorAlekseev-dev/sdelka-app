using Avalonia.Controls;
using Avalonia.Interactivity;
using Sdelka.Desktop.ViewModels;
using Sdelka.Domain.Entities;

namespace Sdelka.Desktop.Views;

public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();
    }

    private void OnAllCategoriesClick(object? sender, RoutedEventArgs e)
    {
        MainViewModel? vm = this.DataContext as MainViewModel;
        if (vm != null)
        {
            vm.SelectCategory(null);
        }
    }

    private void OnCategoryClick(object? sender, RoutedEventArgs e)
    {
        Button? btn = sender as Button;
        if (btn == null)
        {
            return;
        }

        Category? category = btn.DataContext as Category;
        MainViewModel? vm = this.DataContext as MainViewModel;
        if (vm != null && category != null)
        {
            vm.SelectCategory(category);
        }
    }

    private void OnSearchClick(object? sender, RoutedEventArgs e)
    {
        MainViewModel? vm = this.DataContext as MainViewModel;
        if (vm != null)
        {
            vm.ApplyFilter();
        }
    }
}