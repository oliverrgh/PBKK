<<<<<<< HEAD
using System.Collections.ObjectModel;

namespace BookingKampus.Helpers;

public static class CollectionExtensions
{
    public static void ReplaceWith<T>(this ObservableCollection<T> target, IEnumerable<T> items)
    {
        target.Clear();
        foreach (var item in items) target.Add(item);
    }
}
=======
using System.Collections.ObjectModel;

namespace BookingKampus.Helpers;

public static class CollectionExtensions
{
    public static void ReplaceWith<T>(this ObservableCollection<T> target, IEnumerable<T> items)
    {
        target.Clear();
        foreach (var item in items) target.Add(item);
    }
}
>>>>>>> 483c16a2c6c6014653cb604fd45629248707663c
