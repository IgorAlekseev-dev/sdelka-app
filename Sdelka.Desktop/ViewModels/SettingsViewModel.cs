using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Sdelka.Application;
using Sdelka.Domain.Entities;

namespace Sdelka.Desktop.ViewModels;

public class SettingsViewModel : ViewModelBase
{
    private IDataAccess dataAccess;
    private User? selectedUser;

    public ObservableCollection<User> Users { get; set; } = new ObservableCollection<User>();

    public User? SelectedUser
    {
        get { return selectedUser; }
        set { SetProperty(ref selectedUser, value); }
    }

    public SettingsViewModel(IDataAccess dataAccess, User currentUser)
    {
        this.dataAccess = dataAccess;

        List<User> dbUsers = dataAccess.GetUsers();
        foreach (User u in dbUsers)
        {
            Users.Add(u);
            if (u.Id == currentUser.Id)
            {
                SelectedUser = u;
            }
        }
    }

    public void TopUpSelectedUser(decimal amount)
    {
        if (SelectedUser == null)
        {
            return;
        }

        SelectedUser.TopUpBalance(amount);
        dataAccess.UpdateUserBalance(SelectedUser.Id, SelectedUser.Balance);

        OnPropertyChanged(nameof(SelectedUser));
    }
}