# EDA Order System

Bu proje, Event-Driven Architecture (EDA) ile tasarlanmış bir sipariş sistemidir.

## Servisler

- **OrderService**: Sipariş oluşturma işlemlerini yönetir.
- **PaymentService**: Ödeme işlemlerini yönetir ve onaylar.
- **ShippingService**: Kargo/sevkiyat işlemlerini yönetir.
- **RabbitMQ**: Servisler arası iletişimi sağlayan mesaj kuyruk sistemi.

## Docker ile Başlatma

Projeyi Docker ile başlatmak için aşağıdaki adımları izleyin:

1. Projeyi klonlayın.
2. Ana dizinde aşağıdaki komutu çalıştırın:

```bash
docker-compose up -d
```

Bu komut, tüm servisleri Docker konteynerleri olarak başlatacaktır.

## Servis Portları

- **OrderService**: http://localhost:5001
- **PaymentService**: http://localhost:5002
- **ShippingService**: http://localhost:5003
- **RabbitMQ Management UI**: http://localhost:15672 (Kullanıcı adı: guest, Şifre: guest)

## Sipariş Oluşturma

Bir sipariş oluşturmak için aşağıdaki HTTP POST isteğini kullanabilirsiniz:

```
POST http://localhost:5001/create-order
Content-Type: application/json

{
  "orderId": "1",
  "customerId": "123",
  "amount": 100.50,
  "items": [
    {
      "productId": "P1",
      "quantity": 2,
      "price": 50.25
    }
  ]
}
```

Bu istek, RabbitMQ aracılığıyla diğer servisleri tetikleyecektir. 