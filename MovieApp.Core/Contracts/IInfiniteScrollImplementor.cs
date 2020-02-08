using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;

namespace MovieApp.Core.Contracts
{
    public interface IInfiniteScrollImplementor
    {
        ICommand LoadMoreDataCommand { get; set; }
    }
}
