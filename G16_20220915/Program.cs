using G16_20220915;


Person person1 = new Person("1234") { FirstName = "Iviki", LastName = "Matiashvili" };
PersonList people = new PersonList(person1);

Person person2 = new Person("1456") { FirstName = "Tata", LastName = "Vachnadze"};
people.Add(person2);
Person child1 = new Person("0010") { FirstName = "Ilusha", LastName = "Chanukvadze"};
Person child2 = new Person("0012") { FirstName = "Ikako", LastName = "Vachnadze" };
Person child3 = new Person("0011") { FirstName = "Saba", LastName = "Abuladze" };
Person child4 = new Person("0013");
Person child5 = new Person("0014");
child2.AddChild(child3);
child3.AddChild(child4);

person2.AddChild(child3);
person1.AddChild(child5);



child5.AddChild(child2);
person1.AddChild(child1);




//people.Save(@"C:\tata\tata.txt");



//people.Load(@"C:\tata\tata.txt");