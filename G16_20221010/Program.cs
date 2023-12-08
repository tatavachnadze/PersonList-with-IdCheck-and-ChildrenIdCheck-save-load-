using G16_20221010;

PersonList personList = new PersonList();
Person person1 = new Person();
Person person2 = new Person();

person1.AddChild(person2);

((List<Person>)person1.Children).Add(person2);