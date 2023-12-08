namespace G16_20220915;

internal class Person
{
	public Person(string personalID)
	{
		PersonalID = personalID ?? throw new ArgumentNullException(nameof(personalID));		
    }

	private List<Person> _children = new List<Person>();	
	public string? PersonalID { get; private init; }
	public string? FirstName { get; set; }
	public string? LastName { get; set; }
	public PersonList ParentList { get; set; }
	public Person?	Parent { get; set; }

	public ICollection<Person> Children => _children.AsReadOnly();

    public override string ToString()
    {
		return $"First Name :|{FirstName}|Last Name: |{LastName}|Personal ID: |{PersonalID}";
    }

    public void AddChild(Person child)
    {
        var person = this;

        if (person.Parent == null && person.ParentList == null)
        {
            if (person.PersonalID == child.PersonalID)
            {
                Console.WriteLine("ID exists");
            }
            if (person.Children.Count > 0)
            {
                CheckChildsChildren(person.Children, child);
            }
            else
            {
                _children.Add(child);
                return;
            }
        }
      
        while (person.ParentList == null)
        {
            person = this.Parent;
        }
 
        if (person.ParentList.Count > 0)
        {
            if (person.ParentList.PersonalIDCheck(child) == 0)
            {
                child.Parent = this;
                _children.Add(child);
            }
            else
            {
                Console.WriteLine("ID you entered already exists in the list");
                Console.ReadKey();
                Console.Clear();
            }
        }
    }
    
    private int CheckChildsChildren(ICollection<Person> children, Person child)
    {
        foreach (Person c in children)
        {
            if (c.PersonalID == child.PersonalID)
            {
                return 1;
            }
            if (c.Children.Count > 0)
            {
                CheckChildsChildren(c.Children, child);
            }
        }
        return 0;
    }
}

