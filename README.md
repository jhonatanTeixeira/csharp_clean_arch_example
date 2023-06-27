# S.O.L.I.D
1) Princípio da Responsabilidade Única (Single Responsibility Principle - SRP):
Uma classe deve ter apenas uma razão para mudar, ou seja, deve ter uma única responsabilidade.

```c#
public class Customer
{
    public string Name { get; set; }

    public void Save()
    {
        // Lógica para salvar o cliente no banco de dados
    }
}

```

2) Princípio do Aberto/Fechado (Open/Closed Principle - OCP):
Entidades de software devem estar abertas para extensão, mas fechadas para modificação.

```c#
public abstract class Shape
{
    public sealed double Area() {
        return Calculate();
    }

    public abstract double Calculate();
}

public class Rectangle : Shape
{
    public double Width { get; set; }
    public double Height { get; set; }

    public override double Calculate()
    {
        return Width * Height;
    }
}

public class Circle : Shape
{
    public double Radius { get; set; }

    public override double Calculate()
    {
        return Math.PI * Radius * Radius;
    }
}

```

3) Princípio da Substituição de Liskov (Liskov Substitution Principle - LSP):
As classes derivadas devem ser substituíveis por suas classes base.

```c#
public class ShapeService
{
    public double CalculateArea(Shape shape)
    {
        return shape.Area();
    }
}

```

4) Princípio da Segregação de Interface (Interface Segregation Principle - ISP):
Os clientes não devem ser forçados a depender de interfaces que não utilizam

```c#
public interface IInfoLogger
{
    void LogInfo(string message);
}

public interface IErrorLogger
{
    void LogInfo(string message);
}

public interface IErrorLogger
{
    void LogError(string message);
}

public class FileLogger : IInfoLogger
{
    public void LogInfo(string message)
    {
        // Lógica para registrar informações em um arquivo
    }
}

public class DatabaseLogger : IInfoLogger, IerrorLogger
{
    public void LogInfo(string message)
    {
        // Lógica para registrar informações em um banco de dados
    }

    public void LogError(string message)
    {
        // Lógica para registrar erros em um banco de dados
    }
}
```

5) Princípio da Inversão de Dependência (Dependency Inversion Principle - DIP):
Dependam de abstrações, não de implementações concretas.

```c#
public interface IMessageSender
{
    void SendMessage(string message);
}

public class EmailSender : IMessageSender
{
    public void SendMessage(string message)
    {
        // Lógica para enviar mensagem por e-mail
    }
}

public class SmsSender : IMessageSender
{
    public void SendMessage(string message)
    {
        // Lógica para enviar mensagem por SMS
    }
}

public class NotificationService
{
    private readonly IMessageSender _messageSender;

    public NotificationService(IMessageSender messageSender)
    {
        _messageSender = messageSender;
    }

    public void SendNotification(string message)
    {
        _messageSender.SendMessage(message);
    }
}

```

# Clean Code

1) Nomes Significativos: Escolha nomes claros e descritivos para variáveis, funções, classes e métodos. Nomes significativos ajudam a tornar o código mais legível e compreensível.

2) Funções Pequenas e Concisas: Crie funções que realizem uma única tarefa de forma concisa. Funções pequenas são mais fáceis de entender, testar e manter. Idealmente, as funções devem ter no máximo algumas linhas de código.

3) Comentários Significativos: Use comentários para explicar o propósito e a lógica complexa do código. No entanto, evite comentários óbvios ou desnecessários, pois eles podem se tornar desatualizados e confundir os desenvolvedores.

4) Evite a Duplicação de Código: Evite repetir o mesmo código em vários lugares. Em vez disso, extraia o código duplicado para funções ou classes reutilizáveis.

5) Estruturação e Organização: Organize o código em blocos lógicos e estruturados. Use espaçamento, indentação e formatação adequados para melhorar a legibilidade.

6) Testes Unitários: Escreva testes unitários para validar o comportamento esperado do código. Testes unitários fornecem confiança e segurança ao realizar alterações no código.

7) Princípio da Responsabilidade Única (SRP): Cada função, classe ou módulo deve ter uma única responsabilidade. Isso torna o código mais coeso, modular e fácil de entender.

8) Baixo Acoplamento e Alta Coesão: Minimize as dependências entre classes e módulos. Procure manter as classes focadas em uma única tarefa e com baixo acoplamento com outras classes.

9) Princípio Aberto/Fechado (OCP): O código deve ser aberto para extensão, mas fechado para modificação. Isso significa que você pode adicionar novos recursos sem modificar o código existente.

10) Siga as Convenções de Codificação: Siga as convenções de codificação recomendadas para a linguagem que está usando. Isso inclui a formatação do código, o uso de padrões de nomenclatura e a organização dos arquivos.

## Exemplos

Um serviço de produtos usando poucas tecnicas de clean code
```c#
public class ProductService
{
    private readonly IProductRepository _productRepository;
    private readonly IShippingService _shippingService;

    public ProductService(IProductRepository productRepository, IShippingService shippingService)
    {
        _productRepository = productRepository;
        _shippingService = shippingService;
    }

    public void ProcessOrder(OrderDto orderDto)
    {
        // Lógica de processamento do pedido

        // Verifica se há estoque suficiente para os produtos
        foreach (var item in orderDto.Items)
        {
            var product = _productRepository.GetProductById(item.ProductId);
            if (product == null || product.Stock < item.Quantity)
            {
                throw new InvalidOperationException($"O produto {item.ProductId} não possui estoque suficiente.");
            }
        }

        // Calcula o valor total do pedido
        decimal totalAmount = 0;
        foreach (var item in orderDto.Items)
        {
            var product = _productRepository.GetProductById(item.ProductId);
            decimal itemPrice = product.Price * item.Quantity;
            totalAmount += itemPrice;
        }

        // Atualiza o estoque dos produtos
        foreach (var item in orderDto.Items)
        {
            var product = _productRepository.GetProductById(item.ProductId);
            product.Stock -= item.Quantity;
            _productRepository.UpdateProduct(product);
        }

        // Chama o serviço de envio para realizar a entrega
        _shippingService.ShipOrder(orderDto.ShippingAddress, orderDto.Items);
    }
}
```

Um exemplo melhorado isolnado e modularizando a lógicas para reusabilidade e manutembilidade

```c#
public class StockChecker
{
    private readonly IProductRepository _productRepository;

    public StockChecker(IProductRepository productRepository)
    {
        _productRepository = productRepository;
    }

    public bool HasSufficientStock(OrderItemDto item)
    {
        var product = _productRepository.GetProductById(item.ProductId);
        return product != null && product.Stock >= item.Quantity;
    }
}

public class OrderDto
{
    public List<OrderItemDto> Items { get; set; }
    public AddressDto ShippingAddress { get; set; }

    public decimal CalculateTotalAmount(IProductRepository productRepository)
    {
        decimal totalAmount = 0;
        foreach (var item in Items)
        {
            var product = productRepository.GetProductById(item.ProductId);
            decimal itemPrice = product.Price * item.Quantity;
            totalAmount += itemPrice;
        }

        return totalAmount;
    }

    public void UpdateProductStock(IProductRepository productRepository)
    {
        foreach (var item in Items)
        {
            var product = productRepository.GetProductById(item.ProductId);
            product.Stock -= item.Quantity;
            productRepository.UpdateProduct(product);
        }
    }
}

public class ProductService
{
    private readonly IProductRepository _productRepository;
    private readonly IShippingService _shippingService;
    private readonly StockChecker _stockChecker;

    public ProductService(IProductRepository productRepository, IShippingService shippingService, StockChecker stockChecker)
    {
        _productRepository = productRepository;
        _shippingService = shippingService;
        _stockChecker = stockChecker;
    }

    public void ProcessOrder(OrderDto orderDto)
    {
        foreach (var item in orderDto.Items)
        {
            if (!_stockChecker.HasSufficientStock(item))
            {
                throw new InvalidOperationException($"O produto {item.ProductId} não possui estoque suficiente.");
            }
        }

        decimal totalAmount = orderDto.CalculateTotalAmount(_productRepository);

        orderDto.UpdateProductStock(_productRepository);

        _shippingService.ShipOrder(orderDto.ShippingAddress, orderDto.Items);
    }
}
```

## Um exemplo de nossos códigos

```c#
public static ModelsRN.Contato AtualizarContatoPedido(string unidadeNegocio, ModelsFront.Pedido dadosPedido, string emailRA = null)
        {
            bool ehB2B = (dadosPedido?.TipoPedido ?? "").Contains("B2B");
            string cpfCnpj = ehB2B ? dadosPedido?.Destinatario?.CpfCnpj : dadosPedido?.ClienteFaturamento?.CpfCnpj;

            // trava processamento de cadastro de contato, para evitar que registros duplicados sejam criados.
            // caso varias threads tentem criar o mesmo contato ao mesmo tempo.
            ContatosLF.AguardaEIncluiContatoAtivo(cpfCnpj, unidadeNegocio);

            //Consultar o contato exsistente, pela chave unidade negocio + cpf
            ModelsRN.Contato model = Contatos.ConsultarPorCpfCnpj(unidadeNegocio, cpfCnpj) ?? new ModelsRN.Contato();

            //Preenche a entidade de contato com os dados do pedido se for contato novo e data de atualizacao Menor que data edido
            if (!model.Id.HasValue || model.UpdatedTime < dadosPedido.DataPedido)
                PopularCamposContato(unidadeNegocio, dadosPedido, model, ehB2B);
                 
            try
            {
                if (model.Id.HasValue)
                    Contatos.Atualizar(model);
                else
                    model.Id = Contatos.Incluir(model).Id;
            }
            finally
            {
                // libera processamento de cadastro de contato, outras threads podem do mesmo contato poderão ser executadas.
                ContatosLF.RemoveContatoAtivo(ehB2B ? dadosPedido?.Destinatario?.CpfCnpj : dadosPedido?.ClienteFaturamento?.CpfCnpj, unidadeNegocio);
            }

            return model;
        }
```

# Clean Architecture

1) Separação de Responsabilidades: A arquitetura é dividida em camadas, cada uma com uma responsabilidade específica. As camadas mais comuns são: Interface do Usuário (UI), Aplicação, Domínio e Infraestrutura. Cada camada tem um papel claro e as dependências são direcionadas de fora para dentro.

2) Princípio da Inversão de Dependência (IoC/DI): Dependa de abstrações, não de implementações concretas. Inverta o controle das dependências, permitindo que as classes dependam de interfaces ou abstrações em vez de classes concretas. Isso promove o desacoplamento e facilita a substituição de implementações.

3) Princípio da Responsabilidade Única (SRP): Cada classe, módulo ou componente deve ter uma única responsabilidade. Isso facilita a manutenção e o teste, evitando classes grandes e complexas.

4) Princípio do Aberto/Fechado (OCP): As entidades de software devem estar abertas para extensão, mas fechadas para modificação. Isso significa que você pode adicionar novos recursos ou comportamentos sem modificar o código existente.

5) Testabilidade: A Clean Architecture promove a testabilidade através do uso de interfaces e dependências injetadas. Isso permite a criação de testes unitários facilmente isolados e testes de integração que não dependem de infraestrutura externa.

6) Domain-Driven Design (DDD): A Clean Architecture pode ser combinada com o DDD para focar no domínio do problema e garantir que a lógica de negócio esteja centralizada no núcleo do sistema.

7) Separation of Concerns (SoC): Separe diferentes aspectos da aplicação em componentes independentes para que cada um possa evoluir e ser mantido separadamente. Isso ajuda a evitar a mistura de lógica de negócio com detalhes de infraestrutura, por exemplo.

## O que contém em cada camada?

* Domain:
  * Entidades de domínio
  * Plain Objects
  * Interfaces dos repositorios
  * Lógicas de negócio pertinentes ao domínio

* Application:
  * Bibliotecas (libs)

* Infrastrucure:
  * Data mappers
  * Conexões
  * Implementações do domínio (domain)

* Presentation:
  * Frameworks
