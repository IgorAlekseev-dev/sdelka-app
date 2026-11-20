using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Sdelka.Application;
using Sdelka.Domain.Entities;

namespace Sdelka.Desktop.ViewModels;

public class MainViewModel : ViewModelBase
{
    private IDataAccess? dataAccess;
    private OrderController? orderController;
    private List<Product> allProducts = new List<Product>();
    private Guid? selectedCategoryId = null;

    private string searchText = "";
    private User currentUser = new User();

    public ObservableCollection<Category> Categories { get; set; } = new ObservableCollection<Category>();
    public ObservableCollection<Product> Products { get; set; } = new ObservableCollection<Product>();

    public string SearchText
    {
        get { return searchText; }
        set
        {
            if (SetProperty(ref searchText, value))
            {
                ApplyFilter();
            }
        }
    }

    public User CurrentUser
    {
        get { return currentUser; }
        set { SetProperty(ref currentUser, value); }
    }

    public MainViewModel()
    {
    }

    public MainViewModel(IDataAccess dataAccess, OrderController orderController)
    {
        this.dataAccess = dataAccess;
        this.orderController = orderController;
        LoadData();
    }

    public void LoadData()
    {
        if (dataAccess == null)
        {
            return;
        }

        List<Category> dbCategories = dataAccess.GetCategories();
        Categories.Clear();
        foreach (Category c in dbCategories)
        {
            Categories.Add(c);
        }

        allProducts = dataAccess.GetProducts();

        if (currentUser.Id == Guid.Empty)
        {
            List<User> users = dataAccess.GetUsers();
            if (users.Count > 0)
            {
                CurrentUser = users[0];
            }
        }
        else
        {
            User? freshUser = dataAccess.GetUserById(currentUser.Id);
            if (freshUser != null)
            {
                CurrentUser = freshUser;
            }
        }

        ApplyFilter();
    }

    public void SelectCategory(Category? category)
    {
        if (category == null)
        {
            selectedCategoryId = null;
        }
        else
        {
            selectedCategoryId = category.Id;
        }
        ApplyFilter();
    }

    public void ApplyFilter()
    {
        Products.Clear();

        foreach (Product p in allProducts)
        {
            if (selectedCategoryId != null && p.CategoryId != selectedCategoryId.Value)
            {
                continue;
            }

            if (string.IsNullOrWhiteSpace(searchText) == false)
            {
                if (p.Title.Contains(searchText, StringComparison.OrdinalIgnoreCase) == false)
                {
                    continue;
                }
            }

            Products.Add(p);
        }
    }

    public IDataAccess? GetDataAccess()
    {
        return dataAccess;
    }

    public OrderController? GetOrderController()
    {
        return orderController;
    }
}