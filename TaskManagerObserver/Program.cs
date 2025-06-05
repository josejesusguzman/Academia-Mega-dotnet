using Domain;
using Intraestructure;
using Subscribers;

var bus = new EventBus();
var repo = new TaskRepository(bus);

bus.Subscribe(new EmailNotifier());
bus.Subscribe(new SmsNotifier());
bus.Subscribe(new ConsoleUI());

// LA PRUEBA

var task1 = repo.Add(new TaskItem { Title = "Aprender Patron observer" });
var task2 = repo.Add(new TaskItem { Title = "Conectar PubSub a todo el proyecto" });

task1.Complete();
repo.Update(task1);

repo.Delete(task2.id);