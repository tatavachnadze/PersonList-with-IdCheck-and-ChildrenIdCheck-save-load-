namespace G16_20221010;

internal class PersonList : List<Person>
{
	new public void Add(Person item)
	{
		item.ParentList = this;
		base.Add(item);
	}
}