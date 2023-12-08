namespace G16_20220915;

internal class PersonList : List<Person>
{
    public PersonList()
    {

    }
    public PersonList(Person person) : this()
    {
        person.ParentList = this;
        base.Add(person);
    }

    public int PersonalIDCheck(Person item)
    {
        foreach (Person person in this)
        {
            if (item.PersonalID == person.PersonalID)
            {
                return 1;
            }
            if (person.Children.Count > 0)
            {
                if (CheckChildren(person.Children, item) == 1)
                {
                    return 1;
                }
            }
        }
            if (item.Children.Count > 0)
            {
                if (CheckAllChildren(item.Children) == 1)
                {
                    return 1;
                }
            }
            else if (item.PersonalID == null)
            {
                throw new ArgumentNullException(nameof(item));
            }
        
        return 0;
    }

    private int CheckAllChildren(ICollection<Person> children)
    {
        foreach (Person child in children)
        {
          if (PersonalIDCheck(child) == 1)
           {
                return 1;
           }
          else          
          {               
               CheckAllChildren(child.Children);
          }           
        }
        return 0;
    }

    private int CheckChildren(ICollection<Person> children, Person item)
    {
        int found;
        foreach (Person child in children)
        {
            if (child.PersonalID == item.PersonalID)
            {
                found = 1;
                return found;                
            }
            else
            {
                found = CheckChildren(child.Children, item);
                if (found != 0)
                {
                    return found;
                }
            }
        }
        found = 0;
        return found;
    }

    new public void Add(Person item)
    {        
        try
        {
            if ((PersonalIDCheck(item) == 0))
            {
                item.ParentList = this;
                base.Add(item);
            }

            else
            {
            Console.WriteLine("ID you entered already exists in the list");
            Console.ReadKey();
            Console.Clear();
            }
        }
        catch (Exception exception)
        {
            Console.WriteLine($"Error: {exception.Message}");
            Console.Write("Press any key...");
            Console.ReadKey();
        }
    }

    new public void AddRange(IEnumerable<Person> collection)
    {
        foreach (Person item in collection)
        {
            if (PersonalIDCheck(item) == 0)
            {
                base.Add(item);
            }
        }
    }

    new public void Insert(int index, Person item)
    {
        if (PersonalIDCheck(item) == 0)
        {
            base.Insert(index, item);
        }
    }

    new public void InsertRange(int index, IEnumerable<Person> collection)
    {
        foreach (Person item in collection)
        {
            if (PersonalIDCheck(item) == 0)
            {
                base.Insert(index, item);
                index++;
            }
        }
    }

    public void Load(string filePath)
    {
        this.Clear();
        using (StreamReader reader = new StreamReader(new FileStream($@"{filePath}", FileMode.Open)))
        {          
            while (!reader.EndOfStream)
            {
                string[] line = reader.ReadLine().Split("|");
                Person newPerson = new Person(line[7]);
                newPerson.FirstName = line[3];
                newPerson.LastName = line[5];
                                
                if (line[1] == "")
                {                            
                  this.Add(newPerson);
                }
                if (line[1] != "")
                {
                  LoadChildren(newPerson, this, line[1]);
                }                  
            }
        }
    }    

	public void Save(string filePath)
	{
        using (StreamWriter writer = new StreamWriter(new FileStream($@"{filePath}", FileMode.Create)))
        { foreach (Person person in this)
            {
                writer.WriteLine($"This is parent||; {person}");
                if (person.Children.Count > 0)
                {
                    SaveChildren(person, writer);
                }
            }            
        }
	}

    private static void SaveChildren(Person person, StreamWriter writer)
    {        
        foreach (Person child in person.Children)
        {            
            writer.WriteLine($"This is a child of |{person.PersonalID}|; " + child);              
            SaveChildren(child, writer);
        }
    }

    private static void LoadChildren(Person child, ICollection<Person> people, String parentID)
    {        
        foreach (Person person in people)
        {
            if (person.PersonalID == parentID)
            {
                person.Children.Add(child);
            }
            else
            {
                foreach (Person person2 in people)
                {
                    LoadChildren(child, person2.Children, parentID);
                }
            }
        }        
    }   
}
