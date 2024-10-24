using System;

public class CustomDictionary<TKey, TValue> where TKey : IComparable<TKey>
{
	private KeyValuePair<TKey, TValue>[] items;
	private int size;

	public CustomDictionary(int capacity = 10)
	{
		items = new KeyValuePair<TKey, TValue>[capacity];
		size = 0;
	}

	public void Add(TKey key, TValue value)
	{
		if (ContainsKey(key))
		{
			throw new ArgumentException("Key already exists.");
		}

		if (size == items.Length)
			Resize();

		items[size] = new KeyValuePair<TKey, TValue>(key, value);
		size++;
	}

	public TValue Get(TKey key)
	{
		int index = BinarySearch(key, 0, size - 1);
		if (index >= 0)
			return items[index].Value;
		throw new KeyNotFoundException("Key not found.");
	}

	public void Sort()
	{
		QuickSort(items, 0, size - 1);
	}

	private void QuickSort(KeyValuePair<TKey, TValue>[] array, int low, int high)
	{
		if (low < high)
		{
			int pivotIndex = Partition(array, low, high);
			QuickSort(array, low, pivotIndex - 1);
			QuickSort(array, pivotIndex + 1, high);
		}
	}

	private int Partition(KeyValuePair<TKey, TValue>[] array, int low, int high)
	{
		TKey pivot = array[high].Key;
		int i = low - 1;

		for (int j = low; j < high; j++)
		{
			if (array[j].Key.CompareTo(pivot) <= 0)
			{
				i++;
				Swap(array, i, j);
			}
		}
		Swap(array, i + 1, high);
		return i + 1;
	}

	private void Swap(KeyValuePair<TKey, TValue>[] array, int i, int j)
	{
		var temp = array[i];
		array[i] = array[j];
		array[j] = temp;
	}

	private int BinarySearch(TKey key, int low, int high)
	{
		while (low <= high)
		{
			int mid = low + (high - low) / 2;
			int comparison = key.CompareTo(items[mid].Key);

			if (comparison == 0)
				return mid;
			if (comparison < 0)
				high = mid - 1;
			else
				low = mid + 1;
		}
		return -1;
	}

	private bool ContainsKey(TKey key)
	{
		for (int i = 0; i < size; i++)
		{
			if (items[i].Key.CompareTo(key) == 0)
				return true;
		}
		return false;
	}

	private void Resize()
	{
		Array.Resize(ref items, items.Length * 2);
	}
}

public class Program
{
	public static void Main()
	{
		var dictionary = new CustomDictionary<string, int>();

		try
		{
			dictionary.Add("banana", 10);
			dictionary.Add("apple", 5);
			dictionary.Add("cherry", 20);

			Console.WriteLine("Before sorting:");
			PrintDictionary(dictionary);

			dictionary.Sort();

			Console.WriteLine("\nAfter sorting:");
			PrintDictionary(dictionary);

			Console.WriteLine($"\nValue for 'apple': {dictionary.Get("apple")}");
		}
		catch (Exception ex)
		{
			Console.WriteLine($"Error: {ex.Message}");
		}
	}

	private static void PrintDictionary(CustomDictionary<string, int> dictionary)
	{
		try
		{
			Console.WriteLine($"apple: {dictionary.Get("apple")}");
			Console.WriteLine($"banana: {dictionary.Get("banana")}");
			Console.WriteLine($"cherry: {dictionary.Get("cherry")}");
		}
		catch (Exception ex)
		{
			Console.WriteLine(ex.Message);
		}
	}
}
