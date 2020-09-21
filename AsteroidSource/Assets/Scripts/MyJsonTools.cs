using SimpleJSON;
using System.Collections.Generic;
/// <summary>
/// Мои тулсы для SimpleJSON 
/// </summary>
public static class MyJsonTools
{
	/// <summary>
	/// Возвращает значение без критической ошибки
	/// </summary>
	public static int ParseIntField(JSONNode node, string fieldName)
	{
		int ret = 0;

		try	{ret = int.Parse(node[fieldName]);}
		catch { }

		return ret;
	}

	/// <summary>
	/// Конвертирует List в JsonNode строку.
	/// </summary>
	/// <param name="_list">Лист значений</param>
	public static string ConvertListToJStr(List<string> _list, bool isCheckDouble = true)
	{
		//Защита от дублирования
		List<string> list = new List<string>();
		foreach(string value in _list)
		{
			if(isCheckDouble)
			{
				if(!list.Contains(value))
					list.Add(value);
			}
			else
				list.Add(value);
		}

        JSONNode node = JSONNode.Parse("{ }");
        for (int i = 0; i < list.Count; i++)
            node.Add(i.ToString(), list[i]);

		return node.ToString();
	}
}
