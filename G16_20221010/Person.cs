namespace G16_20221010;

internal class Person
{
	private List<Person> _children = new List<Person>();

	public string PersonalID { get; set; }
	public string FirstName { get; set; }
	public string LastName { get; set; }
	public PersonList ParentList { get; set; }
	public Person Parent { get; set; }

	public IEnumerable<Person> Children => _children.AsReadOnly();
	
	public void AddChild(Person person)
	{
		person.Parent = this;
		person.ParentList = this.ParentList;
		_children.Add(person);
	}

	public void RemoveChild(Person person)
	{
		person.Parent = null;
		_children.Remove(person);
	}
}