Принцип Fail Fast дозволяє швидше знаходити баги та спрощує налагодження.
Наступний код виконує цей принцип і працює так, що програма негайно зупиняє виконання якщо виникає помилка, 
замість того щоб продовжувати роботу в некоректному стані.
```c#
public void RemoveProduct(string productName, int quantity)
        {
            var product = GetProduct(productName);
            ValidateProduct(product, quantity);
            product.Quantity -= quantity;
        }

        private void ValidateProduct(Warehouse product, int quantity)
        {
            if (product == null)
                throw new InvalidOperationException("Товар не знайдено на складі.");
            if (product.Quantity < quantity)
                throw new InvalidOperationException("Недостатньо товарів на складі.");
        }
```
 

Принцип KISS закликає до простоти в написанні коду, уникаючи зайвої складності. 
Дотримання KISS у коді  в якому кожен метод виконує одну певну дію.
```c#
pulic void ReducePrice(double amount)
{
    cheackAmount(amount);
    price = Math.Max(0, price - amount);
}

public void cheackAmount(double amount)
{
    if (amount < 0)
        throw new ArgumentException("Число повинне бути додатнім.", nameof(amount));
}
```
 

Принцип DRY (Don't Repeat Yourself) говорить, що код не повинен містити 
дублювання, кожен фрагмент логіки повинен існувати в єдиному місці.

В даному прикладі два методи які використовують спільний метод FindProduct для знаходженя продукту і не дублюють в собі код.
```c#
public void RegisterArrival(Warehouse product, int quantity)
{
    var existingProduct = FindProduct(product.Name);
    if (existingProduct != null)
    {
        existingProduct.Quantity += quantity;
        existingProduct.LastDeliveryDate = DateTime.Now;
    }
    else
    {
        product.Quantity = quantity;
        product.LastDeliveryDate = DateTime.Now;
        inventory.Add(product);
    }
}

public void RegisterShipment(string productName, int quantity)
{
    var product = FindProduct(productName);
    ValidProduct(product, quantity);

    product.Quantity -= quantity;
}
private Warehouse FindProduct(string productName)
{
    return inventory.Find(p => p.Name == productName);
}
```

Принцип SRP (Single Responsibility Principle) кожен клас повинен
виконувати одну конкретну відповідальність.
Клас WarehouseRepository - операції зі складом
Клас Reporting - формування звіту
Money – обробка грошей
Product – представлення товару.

Принцип OCP (Open/Closed Principle) це код який повинен бути відкритий для розширення, але закритий для модифікації.
Щоб додати нову поведінку, ми не змінюємо існуючий код, а розширюємо його через успадкування або інтерфейсию.
WarehouseRepository можна розширювати, наприклад,додати нові методи без зміни основної логіки класу.

WarehouseRepository залежить від інтерфейсу IWarehouseRepository
Ми можемо створити нову реалізацію без зміни існуючого коду
 

Принцип ISP (Interface Segregation Principle) – принцип розділення інтерфейсів

IWarehouseRepository містить лише ті методи, які безпосередньо пов’язані зі складом, і не змушує реалізацію мати непотрібні методи.
```c#
 public interface IWarehouseRepository
    {
        void AddProduct(Warehouse product, int quantity);
        void RemoveProduct(string productName, int quantity);
        Warehouse GetProduct(string productName);
        List<Warehouse> GetAllProducts();
    }
```
DIP (Dependency Inversion Principle) – принцип інверсії залежностей

Reporting залежить від IWarehouseRepository, а не від конкретної реалізації WarehouseRepository, 
що дозволяє легко замінювати реалізацію без зміни коду Reporting.
