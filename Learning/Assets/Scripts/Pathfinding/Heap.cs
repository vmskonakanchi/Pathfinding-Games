using System;
public class Heap<T> where T : IHeapItem<T>
{
    private T[] items;
    private int currentItemCount;
    public Heap(int maxheapSize)
    {
        items = new T[maxheapSize];
    }
    public void Add(T item)
    {
        item.HeapIndex = currentItemCount;
        items[currentItemCount] = item;
        SortUp(item);
        currentItemCount++;
    }

    void SortUp(T item)
    {
        int parentIndex = (item.HeapIndex - 1) / 2;
        while (true)
        {
            T parentItem = items[parentIndex];
            if (item.CompareTo(parentItem) > 0)
            {
                Swap(item, parentItem);
            }
            else
            {
                break;
            }
            parentIndex = (item.HeapIndex - 1) / 2;
        }
    }
    public void UpdateItem(T item)
    {
        SortUp(item);
    }
    public int Count
    {
        get => currentItemCount;
    }
    public bool Contains(T item)
    {
        return Equals(items[item.HeapIndex], item);
    }

    void Swap(T a, T b)
    {
        items[a.HeapIndex] = b;
        items[b.HeapIndex] = a;
        int indexA = a.HeapIndex;
        a.HeapIndex = b.HeapIndex;
        b.HeapIndex = indexA;
    }

    public T RemoveFirstItem()
    {
        T firstitem = items[0];
        currentItemCount--;
        items[0] = items[currentItemCount];
        items[0].HeapIndex = 0;
        SortDown(items[0]);
        return firstitem;
    }
    void SortDown(T item)
    {
        while (true)
        {
            int childindexLeft = (item.HeapIndex * 2) + 1;
            int childindexRight = (item.HeapIndex * 2) + 2;
            int swapIndex;
            if (childindexLeft < currentItemCount)
            {
                swapIndex = childindexLeft;
                if (childindexRight < currentItemCount)
                {
                    if (items[childindexLeft].CompareTo(items[childindexRight]) < 0)
                    {
                        swapIndex = childindexRight;
                    }
                }
                if (item.CompareTo(items[swapIndex]) < 0)
                {
                    Swap(item, items[swapIndex]);
                }
                else
                {
                    return;
                }
            }
            else
            {
                return;
            }
        }
    }
}

public interface IHeapItem<T> : IComparable<T>
{
    public int HeapIndex { get; set; }
}